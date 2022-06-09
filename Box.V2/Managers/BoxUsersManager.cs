using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Extensions;
using Box.V2.Models;
using Box.V2.Models.Request;
using Box.V2.Services;
using Box.V2.Utility;

namespace Box.V2.Managers
{
    /// <summary>
    /// The manager that represents all of the user endpoints
    /// </summary>
    public class BoxUsersManager : BoxResourceManager, IBoxUsersManager
    {
        public BoxUsersManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
            : base(config, service, converter, auth, asUser, suppressNotifications) { }

        /// <summary>
        /// Retrieves information about the user who is currently logged in i.e. the user for whom this auth token was generated.
        /// </summary>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>Returns a single complete user object.</returns>
        public async Task<BoxUser> GetCurrentUserInformationAsync(IEnumerable<string> fields = null)
        {
            BoxRequest request = new BoxRequest(_config.UserEndpointUri, "me")
                .Param(ParamFields, fields);

            IBoxResponse<BoxUser> response = await ToResponseAsync<BoxUser>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to provision a new user in an enterprise. This method only works for enterprise admins.
        /// </summary>
        /// <param name="userRequest">BoxUserRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>Returns the user object for the newly created user.</returns>
        public async Task<BoxUser> CreateEnterpriseUserAsync(BoxUserRequest userRequest, IEnumerable<string> fields = null)
        {
            userRequest.ThrowIfNull("userRequest");
            userRequest.Name.ThrowIfNullOrWhiteSpace("userRequest.Name");

            BoxRequest request = new BoxRequest(_config.UserEndpointUri, "")
                .Param(ParamFields, fields)
                .Payload(_converter.Serialize(userRequest))
                .Method(RequestMethod.Post);

            IBoxResponse<BoxUser> response = await ToResponseAsync<BoxUser>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to edit the settings and information about a user. This method only works for enterprise admins. To roll a user out 
        /// of the enterprise (and convert them to a standalone free user), update the special enterprise attribute to be null.
        /// </summary>
        /// <param name="userRequest">BoxUserRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>Returns the user object for the updated user. Errors may be thrown when the fields are invalid or this API call is made from a non-admin account.</returns>
        public async Task<BoxUser> UpdateUserInformationAsync(BoxUserRequest userRequest, IEnumerable<string> fields = null)
        {
            userRequest.ThrowIfNull("userRequest")
                .Id.ThrowIfNullOrWhiteSpace("userRequest.Id");
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
        /// <param name="filterTerm">Filter the results to only users starting with this value in either the name or the login.</param>
        /// <param name="offset">The record at which to start. (default: 0)</param>
        /// <param name="limit">The number of records to return. (min: 1; default: 100; max: 1000)</param>
        /// <param name="fields">The fields to populate for each returned user.</param>
        /// <param name="userType">The type of user to search for. Valid values are all, external or managed. If nothing is provided, the default behavior will be managed only</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all users; defaults to false.</param>
        /// <param name="externalAppUserId">The external app user id.</param>
        /// <returns>A BoxCollection of BoxUsers matching the provided filter criteria.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when limit outside the range 0&lt;limit&lt;=1000</exception>
        public async Task<BoxCollection<BoxUser>> GetEnterpriseUsersAsync(string filterTerm = null,
                                                                          uint offset = 0,
                                                                          uint limit = 100,
                                                                          IEnumerable<string> fields = null,
                                                                          string userType = null,
                                                                          string externalAppUserId = null,
                                                                          bool autoPaginate = false)
        {
            if (limit == 0 || limit > 1000)
                throw new ArgumentOutOfRangeException("limit", "limit must be within the range 1 <= limit <= 1000");

            BoxRequest request = new BoxRequest(_config.UserEndpointUri)
                .Param("filter_term", filterTerm)
                .Param("offset", offset.ToString())
                .Param("limit", limit.ToString())
                .Param("user_type", userType)
                .Param("external_app_user_id", externalAppUserId)
                .Param(ParamFields, fields);

            if (autoPaginate)
            {
                return await AutoPaginateLimitOffset<BoxUser>(request, (int)limit).ConfigureAwait(false);
            }
            else
            {
                IBoxResponse<BoxCollection<BoxUser>> response = await ToResponseAsync<BoxCollection<BoxUser>>(request).ConfigureAwait(false);
                return response.ResponseObject;
            }
        }

        /// <summary>
        /// Get information about users in an enterprise. This method only works for enterprise admins.
        /// </summary>
        /// <param name="filterTerm">Filter the results to only users starting with this value in either the name or the login.</param>
        /// <param name="marker">Position to return results from.</param>
        /// <param name="limit">The number of records to return. (min: 1; default: 100; max: 1000)</param>
        /// <param name="fields">The fields to populate for each returned user.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all users; defaults to false.</param>
        /// <param name="externalAppUserId">The external app user id.</param>
        /// <returns>A BoxCollection of BoxUsers matching the provided filter criteria.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when limit outside the range 0&lt;limit&lt;=1000</exception>
        public async Task<BoxCollectionMarkerBased<BoxUser>> GetEnterpriseUsersWithMarkerAsync(string marker = null,
                                                                          string filterTerm = null,
                                                                          uint limit = 100,
                                                                          IEnumerable<string> fields = null,
                                                                          string externalAppUserId = null,
                                                                          bool autoPaginate = false)
        {
            if (limit == 0 || limit > 1000)
                throw new ArgumentOutOfRangeException("limit", "limit must be within the range 1 <= limit <= 1000");

            BoxRequest request = new BoxRequest(_config.UserEndpointUri)
                .Param("filter_term", filterTerm)
                .Param("usemarker", "true")
                .Param("marker", marker)
                .Param("limit", limit.ToString())
                .Param("user_type", "")
                .Param("external_app_user_id", externalAppUserId)
                .Param(ParamFields, fields);

            if (autoPaginate)
            {
                return await AutoPaginateMarker<BoxUser>(request, (int)limit).ConfigureAwait(false);
            }
            else
            {
                IBoxResponse<BoxCollectionMarkerBased<BoxUser>> response = await ToResponseAsync<BoxCollectionMarkerBased<BoxUser>>(request).ConfigureAwait(false);
                return response.ResponseObject;
            }
        }

        /// <summary>
        /// Deletes a user in an enterprise account.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        /// <param name="notify">Determines if the destination user should receive email notification of the transfer.</param>
        /// <param name="force">Whether or not the user should be deleted even if this user still own files.</param>
        /// <returns>Null, if user is deleted.</returns>
        public async Task<BoxUser> DeleteEnterpriseUserAsync(string userId, bool notify, bool force)
        {
            BoxRequest request = new BoxRequest(_config.UserEndpointUri, userId)
                .Param("notify", notify.ToString())
                .Param("force", force.ToString())
                .Method(RequestMethod.Delete);

            IBoxResponse<BoxUser> response = await ToResponseAsync<BoxUser>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Invites an existing user to join an Enterprise. The existing user cannot be part of another Enterprise and must already have a Box account.
        /// Once invited, the user will receive an email and prompt to accept the invitation within the Box web application. 
        /// This method requires the "Manage An Enterprise" scope for the enterprise, which can be enabled within your developer console.
        /// </summary>
        /// <param name="userInviteRequest">BoxUserInviteRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>A new invite object will be returned if successful.</returns>
        public async Task<BoxUserInvite> InviteUserToEnterpriseAsync(BoxUserInviteRequest userInviteRequest, IEnumerable<string> fields = null)
        {
            userInviteRequest.ThrowIfNull("userInviteRequest")
                .Enterprise.ThrowIfNull("Enterprise").Id.ThrowIfNullOrWhiteSpace("userInviteRequest.Enterprise.Id");
            userInviteRequest.ActionableBy.ThrowIfNull("userInviteRequest.ActionableBy")
               .Login.ThrowIfNullOrWhiteSpace("userInviteRequest.ActionableBy.Login");

            BoxRequest request = new BoxRequest(_config.InviteEndpointUri)
            .Param(ParamFields, fields)
            .Payload(_converter.Serialize(userInviteRequest))
            .Method(RequestMethod.Post);

            IBoxResponse<BoxUserInvite> response = await ToResponseAsync<BoxUserInvite>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Returns information about an existing user invitation.
        /// </summary>
        /// <param name="inviteId">The ID associated with the user invitiation.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The complete user invite information.</returns>
        public async Task<BoxUserInvite> GetUserInviteAsync(string inviteId, IEnumerable<string> fields = null)
        {
            BoxRequest request = new BoxRequest(_config.InviteEndpointUri, inviteId)
            .Param(ParamFields, fields);

            IBoxResponse<BoxUserInvite> response = await ToResponseAsync<BoxUserInvite>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to convert one of the user’s confirmed email aliases into the user’s primary login.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        /// <param name="login">The email alias to become the primary email.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>If the user_id is valid and the email address is a confirmed email alias, the updated user object will be returned.</returns>
        public async Task<BoxUser> ChangeUsersLoginAsync(string userId, string login, IEnumerable<string> fields = null)
        {
            userId.ThrowIfNullOrWhiteSpace("userId");
            login.ThrowIfNullOrWhiteSpace("login");

            BoxRequest request = new BoxRequest(_config.UserEndpointUri, userId)
            .Param(ParamFields, fields)
            .Payload(_converter.Serialize(new BoxUserRequest() { Login = login }))
            .Method(RequestMethod.Put);

            IBoxResponse<BoxUser> response = await ToResponseAsync<BoxUser>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Removes an email alias from a user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="emailAliasId">The email alias identifier.</param>
        /// <returns>True, if the user has permission to remove this email alias.</returns>
        public async Task<bool> DeleteEmailAliasAsync(string userId, string emailAliasId)
        {
            BoxRequest request = new BoxRequest(_config.UserEndpointUri, string.Format(Constants.DeleteEmailAliasPathString, userId, emailAliasId))
                .Method(RequestMethod.Delete);

            IBoxResponse<BoxUser> response = await ToResponseAsync<BoxUser>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }

        /// <summary>
        /// Retrieves information about a user in the enterprise with the specified fields. Requires enterprise administration authorization.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>Returns the default representation of the user object.</returns>
        public async Task<BoxUser> GetUserInformationAsync(string userId, IEnumerable<string> fields = null)
        {
            BoxRequest request = new BoxRequest(_config.UserEndpointUri, userId)
            .Param(ParamFields, fields);

            IBoxResponse<BoxUser> response = await ToResponseAsync<BoxUser>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Retrieves all email aliases for this user. The collection of email aliases does not include the primary login for the user.
        /// </summary>
        /// <param name="userId">The user ID (required).</param>
        /// <returns>If the userId is valid a collection of email aliases will be returned.</returns>
        public async Task<BoxCollection<BoxEmailAlias>> GetEmailAliasesAsync(string userId)
        {
            userId.ThrowIfNullOrWhiteSpace("userId");

            BoxRequest request = new BoxRequest(_config.UserEndpointUri, string.Format(Constants.UserEmailAliasesPathString, userId)).
                Method(RequestMethod.Get);

            IBoxResponse<BoxCollection<BoxEmailAlias>> response = await ToResponseAsync<BoxCollection<BoxEmailAlias>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Adds a new email alias to the given user’s account.
        /// </summary>
        /// <param name="userId">The user ID (required).</param>
        /// <param name="email">The email address to add to the account as an alias (required).</param>
        /// <returns>Returns the newly created email_alias object. Errors will be thrown if the user_id is not valid or the particular user’s email alias cannot be modified.</returns>
        public async Task<BoxEmailAlias> AddEmailAliasAsync(string userId, string email)
        {
            userId.ThrowIfNullOrWhiteSpace("userId");
            email.ThrowIfNullOrWhiteSpace("emailAlias");

            BoxRequest request = new BoxRequest(_config.UserEndpointUri, string.Format(Constants.UserEmailAliasesPathString, userId)).
                Method(RequestMethod.Post)
                .Payload(_converter.Serialize(new BoxEmailAliasRequest() { Email = email }));

            IBoxResponse<BoxEmailAlias> response = await ToResponseAsync<BoxEmailAlias>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Moves all of the owned content from within one user’s folder into a new folder in another user’s account. 
        /// You can move folders across users as long as the you have administrative permissions and the ‘source’ user owns the folders. 
        /// To move everything from the root folder, use “0” which always represents the root folder of a Box account.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        /// <param name="ownedByUserId">The ID of the user who the folder will be transferred to.</param>
        /// <param name="folderId">Currently only moving of the root folder (0) is supported.</param>
        /// <param name="notify">Determines if the destination user should receive email notification of the transfer.</param>
        /// <param name="timeout">Optional timeout for response.</param>
        /// <returns>Returns the information for the newly created destination folder. An error is thrown if you do not have the necessary permissions to move the folder.</returns>
        public async Task<BoxFolder> MoveUserFolderAsync(string userId, string ownedByUserId, string folderId = "0", bool notify = false, TimeSpan? timeout = null)
        {
            userId.ThrowIfNullOrWhiteSpace("userId");
            ownedByUserId.ThrowIfNullOrWhiteSpace("ownedByUserId");
            folderId.ThrowIfNullOrWhiteSpace("folderId");

            BoxRequest request = new BoxRequest(_config.UserEndpointUri, string.Format(Constants.MoveUserFolderPathString, userId, folderId)) { Timeout = timeout }
                .Param("notify", notify.ToString())
                .Payload(_converter.Serialize(new BoxMoveUserFolderRequest()
                {
                    OwnedBy = new BoxUserRequest()
                    {
                        Id = ownedByUserId
                    }
                }))
                .Method(RequestMethod.Put);

            IBoxResponse<BoxFolder> response = await ToResponseAsync<BoxFolder>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Retrieves all of the group memberships for a given user. 
        /// Note this is only available to group admins. 
        /// To retrieve group memberships for the user making the API request, use the users/me/memberships endpoint.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        /// <param name="offset">The item at which to begin the response.</param>
        /// <param name="limit">Default is 100. Max is 1000.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all group memberships; defaults to false.</param>
        /// <returns>A collection of group membership objects will be returned upon success.</returns>
        public async Task<BoxCollection<BoxGroupMembership>> GetMembershipsForUserAsync(string userId, uint offset = 0, uint limit = 100, bool autoPaginate = false)
        {
            userId.ThrowIfNullOrWhiteSpace("userId");

            BoxRequest request = new BoxRequest(_config.UserEndpointUri, string.Format(Constants.GroupMembershipForUserPathString, userId))
                .Param("offset", offset.ToString())
                .Param("limit", limit.ToString())
                .Method(RequestMethod.Get);

            if (autoPaginate)
            {
                return await AutoPaginateLimitOffset<BoxGroupMembership>(request, (int)limit).ConfigureAwait(false);
            }
            else
            {
                IBoxResponse<BoxCollection<BoxGroupMembership>> response = await ToResponseAsync<BoxCollection<BoxGroupMembership>>(request).ConfigureAwait(false);
                return response.ResponseObject;
            }
        }

        /// <summary>
        /// Retrieves a user's avatar image.
        /// </summary>
        /// <param name="userId">The Id of the user.</param>
        /// <returns>A stream of the bytes for the user's avatar image.</returns>
        public async Task<Stream> GetUserAvatar(string userId)
        {
            var request = new BoxRequest(_config.UserEndpointUri, userId + "/avatar")
                   .Method(RequestMethod.Get);

            IBoxResponse<Stream> response = await ToResponseAsync<Stream>(request).ConfigureAwait(false);
            return response.ResponseObject;
        }

        /// <summary>
        /// Adds or updates a user avatar. Supported formats are JPG, JPEG and PNG. Maximum allowed file size is 1MB and 1024x1024 pixels resolution.
        /// </summary>
        /// <param name="userId">The Id of the user.</param>
        /// <param name="stream">FileStream with avatar image.</param>
        /// <returns>Response containing avatar Urls.</returns>
        public async Task<BoxUploadAvatarResponse> AddOrUpdateUserAvatarAsync(string userId, FileStream stream)
        {
            return await AddOrUpdateUserAvatarAsync(userId, stream, Path.GetFileName(stream.Name)).ConfigureAwait(false);
        }

        /// <summary>
        /// Adds or updates a user avatar. Supported formats are JPG, JPEG and PNG. Maximum allowed file size is 1MB and 1024x1024 pixels resolution.
        /// </summary>
        /// <param name="userId">The Id of the user.</param>
        /// <param name="stream">Stream with avatar image.</param>
        /// <param name="fileName">Filename of the avatar image.</param>
        /// <returns>Response containing avatar Urls.</returns>
        public async Task<BoxUploadAvatarResponse> AddOrUpdateUserAvatarAsync(string userId, Stream stream, string fileName)
        {
            BoxMultiPartRequest request = new BoxMultiPartRequest(_config.UserEndpointUri, userId + "/avatar")
                .FormPart(new BoxFileFormPart()
                {
                    Name = "pic",
                    Value = stream,
                    FileName = fileName,
                    ContentType = ContentTypeMapper.GetContentTypeFromFilename(fileName)
                });

            IBoxResponse<BoxUploadAvatarResponse> response = await ToResponseAsync<BoxUploadAvatarResponse>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Deletes a user's avatar image.
        /// </summary>
        /// <param name="userId">Removes an existing user avatar. You cannot reverse this operation.</param>
        /// <returns>True if deletion success.</returns>
        public async Task<bool> DeleteUserAvatarAsync(string userId)
        {
            var request = new BoxRequest(_config.UserEndpointUri, userId + "/avatar")
                   .Method(RequestMethod.Delete);

            IBoxResponse<BoxEntity> response = await ToResponseAsync<BoxEntity>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }
    }
}
