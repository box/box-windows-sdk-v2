using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Extensions;
using Box.V2.Models;
using Box.V2.Services;
using Box.V2.Utility;
#if NET462
using Microsoft.Win32;
using System.Security;
#endif
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Box.V2.Managers
{
    /// <summary>
    /// The base class for all of the Box resource managers
    /// </summary>
    public abstract class BoxResourceManager
    {
        protected const string ParamFields = "fields";
        protected const string HeaderBoxUA = "X-Box-UA";

        protected IBoxConfig _config;
        protected IBoxService _service;
        protected IBoxConverter _converter;
        protected IAuthRepository _auth;
        protected string _asUser;
        protected string _boxUA;
        protected bool? _suppressNotifications;

        /// <summary>
        /// Instantiates the base class for the Box resource managers
        /// </summary>
        /// <param name="config"></param>
        /// <param name="service"></param>
        /// <param name="converter"></param>
        /// <param name="auth"></param>
        public BoxResourceManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser, bool? suppressNotifications)
        {
            _config = config;
            _service = service;
            _converter = converter;
            _auth = auth;
            _asUser = asUser;
            _suppressNotifications = suppressNotifications;
        }

        protected IBoxRequest AddDefaultHeaders(IBoxRequest request)
        {
            request
                .Header(Constants.RequestParameters.UserAgent, _config.UserAgent)
                .Header(HeaderBoxUA, GetBoxUAHeader())
                .Header(Constants.RequestParameters.AcceptEncoding, _config.AcceptEncoding.ToString());

            if (!string.IsNullOrWhiteSpace(_asUser))
                request.Header(Constants.RequestParameters.AsUser, _asUser);

            if (_suppressNotifications.HasValue && _suppressNotifications.Value)
                request.Header(Constants.RequestParameters.BoxNotifications, "off");

            return request;
        }

        protected async Task<IBoxResponse<T>> ToResponseAsync<T>(IBoxRequest request, bool queueRequest = false,
            IBoxConverter converter = null)
            where T : class
        {
            AddDefaultHeaders(request);
            await AddAuthorizationAsync(request).ConfigureAwait(false);
            var response = await ExecuteRequest<T>(request, queueRequest).ConfigureAwait(false);

            return converter != null ? response.ParseResults(converter) : response.ParseResults(_converter);
        }

        private async Task<IBoxResponse<T>> ExecuteRequest<T>(IBoxRequest request, bool queueRequest)
            where T : class
        {
            var response = queueRequest ?
                await _service.EnqueueAsync<T>(request).ConfigureAwait(false) :
                await _service.ToResponseAsync<T>(request).ConfigureAwait(false);

            if (response.Status == ResponseStatus.Unauthorized)
            {
                // Refresh the access token if the status is "Unauthorized" (HTTP Status Code 401: Unauthorized)
                // This will only be attempted once as refresh tokens are single use
                response = await RetryExpiredTokenRequest<T>(request).ConfigureAwait(false);
            }

            return response;
        }

        /// <summary>
        /// Retry the request once if the first try was due to an expired token
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        protected async Task<IBoxResponse<T>> RetryExpiredTokenRequest<T>(IBoxRequest request)
            where T : class
        {
            OAuthSession newSession = await _auth.RefreshAccessTokenAsync(request.Authorization).ConfigureAwait(false);
            AddDefaultHeaders(request);
            await AddAuthorizationAsync(request, newSession.AccessToken).ConfigureAwait(false);
            return await _service.ToResponseAsync<T>(request).ConfigureAwait(false);
        }

        protected async Task AddAuthorizationAsync(IBoxRequest request, string accessToken = null)
        {
            var auth = accessToken ??
                (_auth.Session ?? await _auth.RefreshAccessTokenAsync(null).ConfigureAwait(false)).AccessToken;

            var authString = string.Format(CultureInfo.InvariantCulture, Constants.V2AuthString, auth);

            request.Authorization = auth;
            request.Header(Constants.AuthHeaderKey, authString);
        }

        /// <summary>
        /// Used to fetch all results using pagination based on limit and offset
        /// </summary>
        /// <typeparam name="T">The type of BoxCollection item to expect.</typeparam>
        /// <param name="request">The pre-configured BoxRequest object.</param>
        /// <param name="limit">The limit specific to the endpoint.</param>
        /// <returns></returns>
        protected async Task<BoxCollection<T>> AutoPaginateLimitOffset<T>(BoxRequest request, int limit) where T : class, new()
        {
            var allItemsCollection = new BoxCollection<T>
            {
                Entries = new List<T>()
            };

            var offset = 0;
            bool keepGoing;
            do
            {
                var response = await ToResponseAsync<BoxCollection<T>>(request).ConfigureAwait(false);
                var newItems = response.ResponseObject;
                allItemsCollection.Entries.AddRange(newItems.Entries ?? new List<T>());
                allItemsCollection.Order = newItems.Order;

                offset += limit;
                request.Param("offset", offset.ToString());

                keepGoing = newItems.Entries.Count >= limit;

            } while (keepGoing);

            allItemsCollection.TotalCount = allItemsCollection.Entries.Count;

            return allItemsCollection;
        }

        /// <summary>
        /// Used to fetch all results using pagination based on next_marker
        /// </summary>
        /// <typeparam name="T">The type of BoxCollectionMarkerBased item to expect.</typeparam>
        /// <param name="request">The pre-configured BoxRequest object.</param>
        /// <param name="limit">The limit specific to the endpoint.</param>
        /// <returns></returns>
        protected async Task<BoxCollectionMarkerBased<T>> AutoPaginateMarker<T>(BoxRequest request, int limit) where T : BoxEntity, new()
        {
            var allItemsCollection = new BoxCollectionMarkerBased<T>
            {
                Entries = new List<T>()
            };

            bool keepGoing;
            do
            {
                var response = await ToResponseAsync<BoxCollectionMarkerBased<T>>(request).ConfigureAwait(false);
                var newItems = response.ResponseObject;
                allItemsCollection.Entries.AddRange(newItems.Entries ?? new List<T>());
                allItemsCollection.Order = newItems.Order;

                request.Param("marker", newItems.NextMarker);

                keepGoing = !string.IsNullOrWhiteSpace(newItems.NextMarker);

            } while (keepGoing);

            return allItemsCollection;
        }

        /// <summary>
        /// Used to fetch all results using pagination based on next_marker, V2 is for sort order is an object.
        /// </summary>
        /// <typeparam name="T">The type of BoxCollectionMarkerBased item to expect.</typeparam>
        /// <param name="request">The pre-configured BoxRequest object.</param>
        /// <param name="limit">The limit specific to the endpoint.</param>
        /// <returns></returns>
        protected async Task<BoxCollectionMarkerBasedV2<T>> AutoPaginateMarkerV2<T>(BoxRequest request, int limit) where T : BoxEntity, new()
        {
            var allItemsCollection = new BoxCollectionMarkerBasedV2<T>
            {
                Entries = new List<T>()
            };

            bool keepGoing;
            do
            {
                var response = await ToResponseAsync<BoxCollectionMarkerBasedV2<T>>(request).ConfigureAwait(false);
                var newItems = response.ResponseObject;
                allItemsCollection.Entries.AddRange(newItems.Entries ?? new List<T>());
                allItemsCollection.Order = newItems.Order;

                request.Param("marker", newItems.NextMarker);

                keepGoing = !string.IsNullOrWhiteSpace(newItems.NextMarker);

            } while (keepGoing);

            return allItemsCollection;
        }

        /// <summary>
        /// Used to fetch all results using pagination for metadata queries
        /// </summary>
        /// <typeparam name="T">The type of BoxCollectionMarkerBased item to expect.</typeparam>
        /// <param name="request">The pre-configured BoxRequest object.</param>
        /// <returns></returns>
        protected async Task<BoxCollectionMarkerBased<T>> AutoPaginateMarkerMetadataQuery<T>(BoxRequest request) where T : BoxMetadataQueryItem, new()
        {
            var allItemsCollection = new BoxCollectionMarkerBased<T>
            {
                Entries = new List<T>()
            };

            bool keepGoing;
            do
            {
                var response = await ToResponseAsync<BoxCollectionMarkerBased<T>>(request).ConfigureAwait(false);
                var newItems = response.ResponseObject;
                allItemsCollection.Entries.AddRange(newItems.Entries);
                allItemsCollection.Order = newItems.Order;

                dynamic body = JObject.Parse(request.Payload);
                body.marker = newItems.NextMarker;
                request.Payload = body.ToString();

                keepGoing = !string.IsNullOrWhiteSpace(newItems.NextMarker);

            } while (keepGoing);

            return allItemsCollection;
        }

        /// <summary>
        /// Used to fetch all results using pagination for metadata queries. V2 of this method expects a fields parameter to be passed in and returns a collection of BoxItem objects.
        /// </summary>
        /// <typeparam name="T">The type of BoxCollectionMarkerBased item to expect.</typeparam>
        /// <param name="request">The pre-configured BoxRequest object.</param>
        /// <returns></returns>
        protected async Task<BoxCollectionMarkerBased<T>> AutoPaginateMarkerMetadataQueryV2<T>(BoxRequest request) where T : BoxItem, new()
        {
            var allItemsCollection = new BoxCollectionMarkerBased<T>
            {
                Entries = new List<T>()
            };

            bool keepGoing;
            do
            {
                var response = await ToResponseAsync<BoxCollectionMarkerBased<T>>(request).ConfigureAwait(false);
                var newItems = response.ResponseObject;
                allItemsCollection.Entries.AddRange(newItems.Entries ?? new List<T>());
                allItemsCollection.Order = newItems.Order;

                dynamic body = JObject.Parse(request.Payload);
                body.marker = newItems.NextMarker;
                request.Payload = body.ToString();

                keepGoing = !string.IsNullOrWhiteSpace(newItems.NextMarker);

            } while (keepGoing);

            return allItemsCollection;
        }

        protected string GetBoxUAHeader()
        {
            if (_boxUA == null)
            {
                _boxUA = "agent=box-windows-sdk/" + AssemblyInfo.NuGetVersion + ";" + GetEnvNameAndVersion();
            }
            return _boxUA;
        }

        private string GetEnvNameAndVersion()
        {
#if NET462
            const string Subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";

            RegistryKey ndpKey;
            try
            {
                ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(Subkey);
            }
            catch (Exception)
            {
                return "";
            }

            using (ndpKey)
            {
                if (ndpKey != null && ndpKey.GetValue("Release") != null)
                {
                    var frameworkVersion = CheckFor462PlusVersion((int)ndpKey.GetValue("Release"));
                    return frameworkVersion != null ? "env=.NET Framework/" + frameworkVersion : "";
                }
                else
                {
                    return "";
                }
            }
#elif NETSTANDARD2_0
            var coreVersion = GetNetCoreVersion();
            return coreVersion != null ? "env=.NET Core/" + coreVersion : "";
#else
            FAIL THE BUILD
            return "";
#endif
        }

        // Checking the version using >= will enable forward compatibility.
        private string CheckFor462PlusVersion(int releaseKey)
        {
            if (releaseKey >= 533320)
                return "4.8.1+";

            if (releaseKey >= 528040)
                return "4.8";

            if (releaseKey >= 461808)
                return "4.7.2";

            if (releaseKey >= 461308)
                return "4.7.1";

            if (releaseKey >= 460798)
                return "4.7";

            if (releaseKey >= 394802)
                return "4.6.2";

            // This code should never execute. A non-null release key should mean
            // that 4.6.2 or later is installed.
            return null;
        }

        private string GetNetCoreVersion()
        {
            try
            {
                var assembly = typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly;
                if (assembly?.CodeBase != null)
                {
                    var assemblyPath = assembly.CodeBase.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
                    var netCoreAppIndex = Array.IndexOf(assemblyPath, "Microsoft.NETCore.App");
                    return netCoreAppIndex > 0 && netCoreAppIndex < assemblyPath.Length - 2 ?
                        assemblyPath[netCoreAppIndex + 1] :
                        null;
                }
            }
            catch
            {
                return null;
            }

#if NETSTANDARD2_0
            var frameworkVersion = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription;
            return Regex.Match(frameworkVersion, @"\d+(\.\d+)+").Value;
#else
            return null;
#endif
        }
    }
}
