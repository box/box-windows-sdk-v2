using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IUsersManager {
        /// <summary>
    /// Returns a list of all users for the Enterprise along with their `user_id`,
    /// `public_name`, and `login`.
    /// 
    /// The application and the authenticated user need to
    /// have the permission to look up users in the entire
    /// enterprise.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getUsers method
    /// </param>
    /// <param name="headers">
    /// Headers of getUsers method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Users> GetUsersAsync(GetUsersQueryParams? queryParams = default, GetUsersHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates a new managed user in an enterprise. This endpoint
    /// is only available to users and applications with the right
    /// admin permissions.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createUser method
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of createUser method
    /// </param>
    /// <param name="headers">
    /// Headers of createUser method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<UserFull> CreateUserAsync(CreateUserRequestBody requestBody, CreateUserQueryParams? queryParams = default, CreateUserHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieves information about the user who is currently authenticated.
    /// 
    /// In the case of a client-side authenticated OAuth 2.0 application
    /// this will be the user who authorized the app.
    /// 
    /// In the case of a JWT, server-side authenticated application
    /// this will be the service account that belongs to the application
    /// by default.
    /// 
    /// Use the `As-User` header to change who this API call is made on behalf of.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getUserMe method
    /// </param>
    /// <param name="headers">
    /// Headers of getUserMe method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<UserFull> GetUserMeAsync(GetUserMeQueryParams? queryParams = default, GetUserMeHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieves information about a user in the enterprise.
    /// 
    /// The application and the authenticated user need to
    /// have the permission to look up users in the entire
    /// enterprise.
    /// 
    /// This endpoint also returns a limited set of information
    /// for external users who are collaborated on content
    /// owned by the enterprise for authenticated users with the
    /// right scopes. In this case, disallowed fields will return
    /// null instead.
    /// </summary>
    /// <param name="userId">
    /// The ID of the user.
    /// Example: "12345"
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of getUserById method
    /// </param>
    /// <param name="headers">
    /// Headers of getUserById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<UserFull> GetUserByIdAsync(string userId, GetUserByIdQueryParams? queryParams = default, GetUserByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates a managed or app user in an enterprise. This endpoint
    /// is only available to users and applications with the right
    /// admin permissions.
    /// </summary>
    /// <param name="userId">
    /// The ID of the user.
    /// Example: "12345"
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateUserById method
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of updateUserById method
    /// </param>
    /// <param name="headers">
    /// Headers of updateUserById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<UserFull> UpdateUserByIdAsync(string userId, UpdateUserByIdRequestBody? requestBody = default, UpdateUserByIdQueryParams? queryParams = default, UpdateUserByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Deletes a user. By default this will fail if the user
    /// still owns any content. Move their owned content first
    /// before proceeding, or use the `force` field to delete
    /// the user and their files.
    /// </summary>
    /// <param name="userId">
    /// The ID of the user.
    /// Example: "12345"
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of deleteUserById method
    /// </param>
    /// <param name="headers">
    /// Headers of deleteUserById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteUserByIdAsync(string userId, DeleteUserByIdQueryParams? queryParams = default, DeleteUserByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}