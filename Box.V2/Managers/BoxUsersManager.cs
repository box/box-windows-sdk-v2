using Box.V2.Auth;
using Box.V2.Contracts;
using Box.V2.Models;
using Box.V2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Managers
{
    public class BoxUsersManager : BoxResourceManager
    {
        public BoxUsersManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth)
            : base(config, service, converter, auth) { }

        /// <summary>
        /// Retrieves information about the user who is currently logged in i.e. the user for whom this auth token was generated.
        /// </summary>
        /// <returns></returns>
        public async Task<BoxUser> GetCurrentUserInformationAsync()
        {
            BoxRequest request = new BoxRequest(_config.UserEndpointUri, "me")
                .Authorize(_auth.Session.AccessToken);

            IBoxResponse<BoxUser> response = await ToResponseAsync<BoxUser>(request);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to edit the settings and information about a user. This method only works for enterprise admins. To roll a user out 
        /// of the enterprise (and convert them to a standalone free user), update the special enterprise attribute to be null
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        public async Task<BoxUser> UpdateUserInformationAsync(string id, BoxUserRequest userRequest)
        {
            BoxRequest request = new BoxRequest(_config.UserEndpointUri, id)
                .Payload(_converter.Serialize(userRequest))
                .Authorize(_auth.Session.AccessToken);

            IBoxResponse<BoxUser> response = await ToResponseAsync<BoxUser>(request);

            return response.ResponseObject;
        }

    }
}
