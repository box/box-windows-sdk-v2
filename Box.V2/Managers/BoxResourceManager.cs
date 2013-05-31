using Box.V2.Auth;
using Box.V2.Contracts;
using Box.V2.Exceptions;
using Box.V2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Managers
{
    public abstract class BoxResourceManager
    {
        protected IBoxConfig _config;
        protected IBoxService _service;
        protected IBoxConverter _converter;
        protected IAuthRepository _auth;

        public BoxResourceManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth)
        {
            _config = config;
            _service = service;
            _converter = converter; 
            _auth = auth;
        }

        protected IBoxRequest AddDefaultHeaders(IBoxRequest request)
        {
            request.Header("User-Agent", _config.UserAgent ?? string.Empty);
                //.Param("device_id", _config.DeviceId ?? string.Empty)
                //.Param("device_name", _config.DeviceName ?? string.Empty);

            return request;
        }

        protected void CheckPrerequisite(params string[] values)
        {
            foreach (var v in values)
            {
                if (string.IsNullOrWhiteSpace(v.ToString()))
                    throw new ArgumentException("Value for a required field cannot be null or empty");
            }
        }

        protected async Task<IBoxResponse<T>> RetryExpiredTokenRequest<T>(IBoxRequest request)
            where T : class
        {
            OAuthSession newSession = await _auth.RefreshAccessTokenAsync(request.Authorization);
            request.Authorize(newSession.AccessToken);
            return await _service.ToResponseAsync<T>(request);
        }

        protected async Task<IBoxResponse<T>> ToResponseAsync<T>(IBoxRequest request, bool queueRequest = false)
            where T : class
        {
            var response = queueRequest ? 
                await _service.EnqueueAsync<T>(request) :
                await _service.ToResponseAsync<T>(request);

            if (response.Status == ResponseStatus.Unauthorized)
                response = await RetryExpiredTokenRequest<T>(request);

            return response.ParseResults(_converter);
        }

    }
}
