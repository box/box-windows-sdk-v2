using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Services;
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
            var response = await ExecuteRequest<T>(request, queueRequest);

            return response.ParseResults(_converter);
        }

        private async Task<IBoxResponse<T>> ExecuteRequest<T>(IBoxRequest request, bool queueRequest)
            where T : class
        {
            var response = queueRequest ? 
                await _service.EnqueueAsync<T>(request) :
                await _service.ToResponseAsync<T>(request);

            switch (response.Status)
            {
                // Refresh the access token if the status is "Unauthorized" (HTTP Status Code 401: Unauthorized)
                // This will only be attempted once as refresh tokens are single use
                case ResponseStatus.Unauthorized:
                    response = await RetryExpiredTokenRequest<T>(request);
                    break;
                // Continue to retry the request if the status is "Pending" (HTTP Status Code 202: Approved)
                // this will occur if a preview/thumbnail is not ready yet
                case ResponseStatus.Pending:
                    response = await ExecuteRequest<T>(request, queueRequest);
                    break;
            }

            return response;
        }

        protected async Task<IBoxResponse<T>> RetryExpiredTokenRequest<T>(IBoxRequest request)
            where T : class
        {
            OAuthSession newSession = await _auth.RefreshAccessTokenAsync(request.Authorization);
            request.Authorize(newSession.AccessToken);
            return await _service.ToResponseAsync<T>(request);
        }


        protected void AddAuthorization(IBoxRequest request, string accessToken)
        {
            StringBuilder sb = new StringBuilder(string.Format("Bearer {0}", accessToken));
            
            // Device ID is required for accounts that have device pinning enabled
            sb.Append(string.IsNullOrWhiteSpace(_config.DeviceId) ? 
                string.Empty : 
                string.Format("&device_id={0}", _config.DeviceId));
            sb.Append(string.IsNullOrWhiteSpace(_config.DeviceName) ? 
                string.Empty : 
                string.Format("&device_name={0}", _config.DeviceName));

            request.Header("Authorization", sb.ToString());
        }

    }
}
