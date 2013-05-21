using Box.V2.Auth;
using Box.V2.Contracts;
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
        protected IAuthRepository _auth;
        protected IBoxService _service;
        protected IBoxConfig _config;

        public BoxResourceManager(IBoxConfig config, IBoxService service, IAuthRepository auth)
        {
            _config = config;
            _service = service;
            _auth = auth;
        }

        protected IBoxRequest AddAuthentication(IBoxRequest request)
        {
            request
                .Header("Authorization", string.Format("Bearer {0}", _auth.Session.AccessToken))
                .Header("User-Agent", _config.UserAgent ?? string.Empty)
                .Param("device_id", _config.DeviceId ?? string.Empty)
                .Param("device_name", _config.DeviceName ?? string.Empty);

            return request;
        }


        //"{0} api_key={1}&auth_token={2}", "BoxAuth", _config.ConsumerKey
    }
}
