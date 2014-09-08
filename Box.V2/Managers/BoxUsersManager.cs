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
        public BoxUsersManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth)
            : base(config, service, converter, auth) { }

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
                .Payload(_converter.Serialize(userRequest));

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
        /// Get all email aliases for a user
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <returns>A collection of email aliases</returns>
        public async Task<BoxCollection<BoxEmailAlias>> GetEmailAliasesAsync(string userId)
        {
            userId.ThrowIfNullOrWhiteSpace("userId");

            var request = new BoxRequest(_config.UserEndpointUri, string.Format(Constants.EmailAliasesPathString, userId));
            var response = await ToResponseAsync<BoxCollection<BoxEmailAlias>>(request).ConfigureAwait(false);
            return response.ResponseObject;
        }

        /// <summary>
        /// Add a new email alias for a user
        /// </summary>
        /// <param name="emailAliasRequest">The ID of the user to alias and the email address to add</param>
        /// <returns>A new email alias</returns>
        public async Task<BoxEmailAlias> AddEmailAliasAsync(BoxEmailAliasRequest emailAliasRequest)
        {
            emailAliasRequest.Email.ThrowIfNullOrWhiteSpace("email");
            emailAliasRequest.User.ThrowIfNull("user")
                .Id.ThrowIfNullOrWhiteSpace("user.Id");

            var request = new BoxRequest(_config.UserEndpointUri, string.Format(Constants.EmailAliasesPathString, emailAliasRequest.User.Id))
                .Method(RequestMethod.Post)
                .Payload(_converter.Serialize(emailAliasRequest));

            var response = await ToResponseAsync<BoxEmailAlias>(request).ConfigureAwait(false);
            return response.ResponseObject;
        }
    }
}
