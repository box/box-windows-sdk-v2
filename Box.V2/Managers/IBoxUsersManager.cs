using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Box.V2.Models;

namespace Box.V2.Managers
{
    /// <summary>
    /// The manager that represents all of the user endpoints
    /// </summary>
    public interface IBoxUsersManager
    {
        /// <summary>
        /// Retrieves information about the user who is currently logged in i.e. the user for whom this auth token was generated.
        /// </summary>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>Returns a single complete user object.</returns>
        Task<BoxUser> GetCurrentUserInformationAsync(IEnumerable<string> fields = null);

        /// <summary>
        /// Used to provision a new user in an enterprise. This method only works for enterprise admins.
        /// </summary>
        /// <param name="userRequest">BoxUserRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>Returns the user object for the newly created user.</returns>
        Task<BoxUser> CreateEnterpriseUserAsync(BoxUserRequest userRequest, IEnumerable<string> fields = null);

        /// <summary>
        /// Used to edit the settings and information about a user. This method only works for enterprise admins. To roll a user out 
        /// of the enterprise (and convert them to a standalone free user), update the special enterprise attribute to be null.
        /// </summary>
        /// <param name="userRequest">BoxUserRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>Returns the user object for the updated user. Errors may be thrown when the fields are invalid or this API call is made from a non-admin account.</returns>
        Task<BoxUser> UpdateUserInformationAsync(BoxUserRequest userRequest, IEnumerable<string> fields = null);

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
        Task<BoxCollection<BoxUser>> GetEnterpriseUsersAsync(string filterTerm = null,
            uint offset = 0,
            uint limit = 100,
            IEnumerable<string> fields = null,
            string userType = null,
            string externalAppUserId = null,
            bool autoPaginate = false);

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
        Task<BoxCollectionMarkerBased<BoxUser>> GetEnterpriseUsersWithMarkerAsync(string marker = null,
                                                                          string filterTerm = null,
                                                                          uint limit = 100,
                                                                          IEnumerable<string> fields = null,
                                                                          string externalAppUserId = null,
                                                                          bool autoPaginate = false);

        /// <summary>
        /// Deletes a user in an enterprise account.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        /// <param name="notify">Determines if the destination user should receive email notification of the transfer.</param>
        /// <param name="force">Whether or not the user should be deleted even if this user still own files.</param>
        /// <returns>Null, if user is deleted.</returns>
        Task<BoxUser> DeleteEnterpriseUserAsync(string userId, bool notify, bool force);

        /// <summary>
        /// Invites an existing user to join an Enterprise. The existing user cannot be part of another Enterprise and must already have a Box account.
        /// Once invited, the user will receive an email and prompt to accept the invitation within the Box web application. 
        /// This method requires the "Manage An Enterprise" scope for the enterprise, which can be enabled within your developer console.
        /// </summary>
        /// <param name="userInviteRequest">BoxUserInviteRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>A new invite object will be returned if successful.</returns>
        Task<BoxUserInvite> InviteUserToEnterpriseAsync(BoxUserInviteRequest userInviteRequest, IEnumerable<string> fields = null);

        /// <summary>
        /// Returns information about an existing user invitation.
        /// </summary>
        /// <param name="inviteId">The ID associated with the user invitiation.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The complete user invite information.</returns>
        Task<BoxUserInvite> GetUserInviteAsync(string inviteId, IEnumerable<string> fields = null);

        /// <summary>
        /// Used to convert one of the user’s confirmed email aliases into the user’s primary login.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        /// <param name="login">The email alias to become the primary email.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>If the user_id is valid and the email address is a confirmed email alias, the updated user object will be returned.</returns>
        Task<BoxUser> ChangeUsersLoginAsync(string userId, string login, IEnumerable<string> fields = null);

        /// <summary>
        /// Removes an email alias from a user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="emailAliasId">The email alias identifier.</param>
        /// <returns>True, if the user has permission to remove this email alias.</returns>
        Task<bool> DeleteEmailAliasAsync(string userId, string emailAliasId);

        /// <summary>
        /// Retrieves information about a user in the enterprise. Requires enterprise administration authorization.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>Returns the complete user object.</returns>
        Task<BoxUser> GetUserInformationAsync(string userId, IEnumerable<string> fields = null);

        /// <summary>
        /// Retrieves all email aliases for this user. The collection of email aliases does not include the primary login for the user.
        /// </summary>
        /// <param name="userId">The user ID (required).</param>
        /// <returns>If the userId is valid a collection of email aliases will be returned.</returns>
        Task<BoxCollection<BoxEmailAlias>> GetEmailAliasesAsync(string userId);

        /// <summary>
        /// Adds a new email alias to the given user’s account.
        /// </summary>
        /// <param name="userId">The user ID (required).</param>
        /// <param name="email">The email address to add to the account as an alias (required).</param>
        /// <returns>Returns the newly created email_alias object. Errors will be thrown if the user_id is not valid or the particular user’s email alias cannot be modified.</returns>
        Task<BoxEmailAlias> AddEmailAliasAsync(string userId, string email);

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
        Task<BoxFolder> MoveUserFolderAsync(string userId, string ownedByUserId, string folderId = "0", bool notify = false, TimeSpan? timeout = null);

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
        Task<BoxCollection<BoxGroupMembership>> GetMembershipsForUserAsync(string userId, uint offset = 0, uint limit = 100, bool autoPaginate = false);

        /// <summary>
        /// Retrieves a user's avatar image.
        /// </summary>
        /// <param name="userId">The Id of the user.</param>
        /// <returns>A stream of the bytes for the user's avatar image.</returns>
        Task<Stream> GetUserAvatar(string userId);

        /// <summary>
        /// Adds or updates a user avatar. Supported formats are JPG, JPEG and PNG. Maximum allowed file size is 1MB and 1024x1024 pixels resolution.
        /// </summary>
        /// <param name="userId">The Id of the user.</param>
        /// <param name="stream">FileStream with avatar image.</param>
        /// <returns>Response containing avatar Urls.</returns>
        Task<BoxUploadAvatarResponse> AddOrUpdateUserAvatarAsync(string userId, FileStream stream);

        /// <summary>
        /// Adds or updates a user avatar. Supported formats are JPG, JPEG and PNG. Maximum allowed file size is 1MB and 1024x1024 pixels resolution.
        /// </summary>
        /// <param name="userId">The Id of the user.</param>
        /// <param name="stream">Stream with avatar image.</param>
        /// <param name="fileName">Filename of the avatar image.</param>
        /// <returns>Response containing avatar Urls.</returns>
        Task<BoxUploadAvatarResponse> AddOrUpdateUserAvatarAsync(string userId, Stream stream, string fileName);

        /// <summary>
        /// Deletes a user's avatar image.
        /// </summary>
        /// <param name="userId">Removes an existing user avatar. You cannot reverse this operation.</param>
        /// <returns>True if deletion success.</returns>
        Task<bool> DeleteUserAvatarAsync(string userId);
    }
}
