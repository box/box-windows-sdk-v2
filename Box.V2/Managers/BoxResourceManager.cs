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

        protected IBoxRequest AddAuthentication(IBoxRequest request)
        {
            request
                .Header("Authorization", string.Format("Bearer {0}", _auth.Session.AccessToken));
                //.Header("Accept", "*/*");
                //.Header("User-Agent", _config.UserAgent ?? string.Empty);
                //.Param("device_id", _config.DeviceId ?? string.Empty)
                //.Param("device_name", _config.DeviceName ?? string.Empty);

            return request;
        }
    }
}
