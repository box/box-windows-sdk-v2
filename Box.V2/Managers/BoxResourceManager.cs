using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Extensions;
using Box.V2.Models;
using Box.V2.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Managers
{
    /// <summary>
    /// The base class for all of the Box resource managers
    /// </summary>
    public abstract class BoxResourceManager
    {
        protected const string ParamFields = "fields";

        protected IBoxConfig _config;
        protected IBoxService _service;
        protected IBoxConverter _converter;
        protected IAuthRepository _auth;
        protected string _asUser;
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
                .Header(Constants.RequestParameters.AcceptEncoding, _config.AcceptEncoding.ToString());
            
            if (!String.IsNullOrWhiteSpace(_asUser))
                request.Header(Constants.RequestParameters.AsUser, _asUser);

            if (_suppressNotifications.HasValue && _suppressNotifications.Value)
                request.Header(Constants.RequestParameters.BoxNotifications, "off");

            return request;
        }

        protected async Task<IBoxResponse<T>> ToResponseAsync<T>(IBoxRequest request, bool queueRequest = false)
            where T : class
        {
            AddDefaultHeaders(request);
            AddAuthorization(request);
            var response = await ExecuteRequest<T>(request, queueRequest).ConfigureAwait(false);

            return response.ParseResults(_converter);
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
            AddAuthorization(request, newSession.AccessToken);
            return await _service.ToResponseAsync<T>(request).ConfigureAwait(false);
        }

        protected void AddAuthorization(IBoxRequest request, string accessToken = null)
        {
            var auth = accessToken ?? _auth.Session.AccessToken;

            string authString = _auth.Session.AuthVersion == AuthVersion.V1 ? 
                string.Format(CultureInfo.InvariantCulture, Constants.V1AuthString, _config.ClientId, auth) : 
                string.Format(CultureInfo.InvariantCulture, Constants.V2AuthString, auth);

            StringBuilder sb = new StringBuilder(authString);

            // Appending device_id is required for accounts that have device pinning enabled on V1 auth
            if (_auth.Session.AuthVersion == AuthVersion.V1)
            { 
                sb.Append(string.IsNullOrWhiteSpace(_config.DeviceId) ? 
                    string.Empty : 
                    string.Format("&device_id={0}", _config.DeviceId));
                sb.Append(string.IsNullOrWhiteSpace(_config.DeviceName) ? 
                    string.Empty : 
                    string.Format("&device_name={0}", _config.DeviceName));
            }

            request.Authorization = auth;
            request.Header(Constants.AuthHeaderKey, sb.ToString());
        }

        /// <summary>
        /// Used to fetch all results using pagination based on limit and offset
        /// </summary>
        /// <typeparam name="T">The type of BoxCollection item to expect.</typeparam>
        /// <param name="request">The pre-configured BoxRequest object.</param>
        /// <param name="limit">The limit specific to the endpoint.</param>
        /// <returns></returns>
        protected async Task<BoxCollection<T>> AutoPaginateLimitOffset<T>(BoxRequest request, int limit) where T : BoxEntity, new()
        {
            var allItemsCollection = new BoxCollection<T>();
            allItemsCollection.Entries = new List<T>();

            int offset = 0;
            bool keepGoing;
            do
            {
                IBoxResponse<BoxCollection<T>> response = await ToResponseAsync<BoxCollection<T>>(request).ConfigureAwait(false);
                var newItems = response.ResponseObject;
                allItemsCollection.Entries.AddRange(newItems.Entries);
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
            var allItemsCollection = new BoxCollectionMarkerBased<T>();
            allItemsCollection.Entries = new List<T>();

            bool keepGoing;
            do
            {
                IBoxResponse<BoxCollectionMarkerBased<T>> response = await ToResponseAsync<BoxCollectionMarkerBased<T>>(request).ConfigureAwait(false);
                var newItems = response.ResponseObject;
                allItemsCollection.Entries.AddRange(newItems.Entries);
                allItemsCollection.Order = newItems.Order;

                request.Param("marker", newItems.NextMarker);

                keepGoing = !string.IsNullOrWhiteSpace(newItems.NextMarker);

            } while (keepGoing);

            return allItemsCollection;
        }

    }
}
