using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IEmailAliasesManager {
        /// <summary>
    /// Retrieves all email aliases for a user. The collection
    /// does not include the primary login for the user.
    /// </summary>
    /// <param name="userId">
    /// The ID of the user.
    /// Example: "12345"
    /// </param>
    /// <param name="headers">
    /// Headers of getUserEmailAliases method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<EmailAliases> GetUserEmailAliasesAsync(string userId, GetUserEmailAliasesHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Adds a new email alias to a user account..
    /// </summary>
    /// <param name="userId">
    /// The ID of the user.
    /// Example: "12345"
    /// </param>
    /// <param name="requestBody">
    /// Request body of createUserEmailAlias method
    /// </param>
    /// <param name="headers">
    /// Headers of createUserEmailAlias method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<EmailAlias> CreateUserEmailAliasAsync(string userId, CreateUserEmailAliasRequestBody requestBody, CreateUserEmailAliasHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Removes an email alias from a user.
    /// </summary>
    /// <param name="userId">
    /// The ID of the user.
    /// Example: "12345"
    /// </param>
    /// <param name="emailAliasId">
    /// The ID of the email alias.
    /// Example: "23432"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteUserEmailAliasById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteUserEmailAliasByIdAsync(string userId, string emailAliasId, DeleteUserEmailAliasByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}