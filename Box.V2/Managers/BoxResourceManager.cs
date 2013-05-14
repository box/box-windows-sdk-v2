using Box.V2.Auth;
using Box.V2.Contracts;
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
        protected IBoxConfig _boxConfig;

        public BoxResourceManager(IBoxConfig boxConfig, IAuthRepository auth)
        {
            _boxConfig = boxConfig;
            _auth = auth;
        }

        protected IBoxRequest AddAuthentication(IBoxRequest request)
        {
            request
                .Header("Authorization", string.Format("{0} api_key={1}&auth_token={2}", "BoxAuth", _boxConfig.ConsumerKey, _auth.Session.AccessToken))
                .Header("User-Agent", _boxConfig.UserAgent)
                .Param("device_id", _boxConfig.DeviceId)
                .Param("device_name", _boxConfig.DeviceName);

            return request;
        }

    }
}
