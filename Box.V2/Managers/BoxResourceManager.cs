using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Extensions;
using Box.V2.Services;
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

        /// <summary>
        /// Instantiates the base class for the Box resource managers
        /// </summary>
        /// <param name="config"></param>
        /// <param name="service"></param>
        /// <param name="converter"></param>
        /// <param name="auth"></param>
        public BoxResourceManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth)
        {
            _config = config;
            _service = service;
            _converter = converter; 
            _auth = auth;
        }

        protected IBoxRequest AddDefaultHeaders(IBoxRequest request)
        {
            request
                .Header("User-Agent", _config.UserAgent)
                .Header("Accept-Encoding", _config.AcceptEncoding.ToString());

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

            switch (response.Status)
            {
                // Refresh the access token if the status is "Unauthorized" (HTTP Status Code 401: Unauthorized)
                // This will only be attempted once as refresh tokens are single use
                case ResponseStatus.Unauthorized:
                    response = await RetryExpiredTokenRequest<T>(request).ConfigureAwait(false);
                    break;
                // Continue to retry the request if the status is "Pending" (HTTP Status Code 202: Approved)
                // this will occur if a preview/thumbnail is not ready yet
                case ResponseStatus.Pending:
                    response = await ExecuteRequest<T>(request, queueRequest).ConfigureAwait(false);
                    break;
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
            
            // Device ID is required for accounts that have device pinning enabled
            sb.Append(string.IsNullOrWhiteSpace(_config.DeviceId) ? 
                string.Empty : 
                string.Format("&device_id={0}", _config.DeviceId));
            sb.Append(string.IsNullOrWhiteSpace(_config.DeviceName) ? 
                string.Empty : 
                string.Format("&device_name={0}", _config.DeviceName));

            request.Header(Constants.AuthHeaderKey, sb.ToString());
        }


    }
}
