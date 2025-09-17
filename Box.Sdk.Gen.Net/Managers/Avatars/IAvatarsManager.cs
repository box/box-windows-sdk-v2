using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IAvatarsManager {
        /// <summary>
    /// Retrieves an image of a the user's avatar.
    /// </summary>
    /// <param name="userId">
    /// The ID of the user.
    /// Example: "12345"
    /// </param>
    /// <param name="headers">
    /// Headers of getUserAvatar method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<System.IO.Stream> GetUserAvatarAsync(string userId, GetUserAvatarHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Adds or updates a user avatar.
    /// </summary>
    /// <param name="userId">
    /// The ID of the user.
    /// Example: "12345"
    /// </param>
    /// <param name="requestBody">
    /// Request body of createUserAvatar method
    /// </param>
    /// <param name="headers">
    /// Headers of createUserAvatar method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<UserAvatar> CreateUserAvatarAsync(string userId, CreateUserAvatarRequestBody requestBody, CreateUserAvatarHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Removes an existing user avatar.
    /// You cannot reverse this operation.
    /// </summary>
    /// <param name="userId">
    /// The ID of the user.
    /// Example: "12345"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteUserAvatar method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteUserAvatarAsync(string userId, DeleteUserAvatarHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}