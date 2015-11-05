using System;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Extensions;
using Box.V2.Models;
using Box.V2.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Box.V2.Managers
{
    /// <summary>
    /// The manager that represents all of the user endpoints
    /// </summary>
    public class BoxUsersManager : BoxResourceManager
    {
        public BoxUsersManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null)
            : base(config, service, converter, auth, asUser) { }

        /// <summary>
        /// Retrieves information about the user who is currently logged in i.e. the user for whom this auth token was generated.
        /// </summary>
        /// <returns></returns>
        public async Task<BoxUser> GetCurrentUserInformationAsync(List<string> fields = null)
        {
            BoxRequest request = new BoxRequest(_config.UserEndpointUri, "me")
                .Param(ParamFields, fields);

            IBoxResponse<BoxUser> response = await ToResponseAsync<BoxUser>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Create a new Box Enterprise user.
        /// </summary>
        /// <returns></returns>
        public async Task<BoxUser> CreateEnterpriseUserAsync(BoxUserRequest userRequest, List<string> fields = null)
        {
            BoxRequest request = new BoxRequest(_config.UserEndpointUri, userRequest.Id)
                .Param(ParamFields, fields)
                .Payload(_converter.Serialize(userRequest))
                .Method(RequestMethod.Post);

            IBoxResponse<BoxUser> response = await ToResponseAsync<BoxUser>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to edit the settings and information about a user. This method only works for enterprise admins. To roll a user out 
        /// of the enterprise (and convert them to a standalone free user), update the special enterprise attribute to be null
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        public async Task<BoxUser> UpdateUserInformationAsync(BoxUserRequest userRequest, List<string> fields = null)
        {
            BoxRequest request = new BoxRequest(_config.UserEndpointUri, userRequest.Id)
                .Param(ParamFields, fields)
                .Payload(_converter.Serialize(userRequest))
                .Method(RequestMethod.Put);

            IBoxResponse<BoxUser> response = await ToResponseAsync<BoxUser>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }


        /// <summary>
        /// Get information about users in an enterprise. This method only works for enterprise admins.
        /// </summary>
        /// <param name="filterTerm">Filter the results to only users starting with this value in either the name or the login</param>
        /// <param name="offset">The record at which to start. (default: 0)</param>
        /// <param name="limit">The number of records to return. (min: 1; default: 100; max: 1000)</param>
        /// <param name="fields">The fields to populate for each returned user</param>
        /// <returns>A BoxCollection of BoxUsers matching the provided filter criteria</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when limit outside the range 0&lt;limit&lt;=1000</exception>
        public async Task<BoxCollection<BoxUser>> GetEnterpriseUsersAsync(string filterTerm = null, uint offset= 0, uint limit = 100, List<string> fields = null)
        {
            if (limit == 0 || limit > 1000) throw new ArgumentOutOfRangeException("limit", "limit must be within the range 1 <= limit <= 1000");

            BoxRequest request = new BoxRequest(_config.UserEndpointUri)
                .Param("filter_term", filterTerm)
                .Param("offset", offset.ToString())
                .Param("limit", limit.ToString())
                .Param(ParamFields, fields);

            IBoxResponse<BoxCollection<BoxUser>> response = await ToResponseAsync<BoxCollection<BoxUser>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Deletes an enterprise user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="notify">Determines if the destination user should receive email notification of the transfer.</param>
        /// <param name="force">Whether or not the user should be deleted even if this user still own files.</param>
        /// <returns></returns>
        public async Task<BoxUser> DeleteEnterpriseUserAsync(string userId, bool notify, bool force)
        {
            BoxRequest request = new BoxRequest(_config.UserEndpointUri, userId)
                .Param("notify", notify.ToString())
                .Param("force", force.ToString())
                .Method(RequestMethod.Delete);

            IBoxResponse<BoxUser> response = await ToResponseAsync<BoxUser>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }
    }
}
