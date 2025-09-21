using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public interface IShieldListsManager {
        /// <summary>
    /// Retrieves all shield lists in the enterprise.
    /// </summary>
    /// <param name="headers">
    /// Headers of getShieldListsV2025R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShieldListsV2025R0> GetShieldListsV2025R0Async(GetShieldListsV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates a shield list.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createShieldListV2025R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of createShieldListV2025R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShieldListV2025R0> CreateShieldListV2025R0Async(ShieldListsCreateV2025R0 requestBody, CreateShieldListV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieves a single shield list by its ID.
    /// </summary>
    /// <param name="shieldListId">
    /// The unique identifier that represents a shield list.
    /// The ID for any Shield List can be determined by the response from the endpoint
    /// fetching all shield lists for the enterprise.
    /// Example: "90fb0e17-c332-40ed-b4f9-fa8908fbbb24 "
    /// </param>
    /// <param name="headers">
    /// Headers of getShieldListByIdV2025R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShieldListV2025R0> GetShieldListByIdV2025R0Async(string shieldListId, GetShieldListByIdV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Delete a single shield list by its ID.
    /// </summary>
    /// <param name="shieldListId">
    /// The unique identifier that represents a shield list.
    /// The ID for any Shield List can be determined by the response from the endpoint
    /// fetching all shield lists for the enterprise.
    /// Example: "90fb0e17-c332-40ed-b4f9-fa8908fbbb24 "
    /// </param>
    /// <param name="headers">
    /// Headers of deleteShieldListByIdV2025R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteShieldListByIdV2025R0Async(string shieldListId, DeleteShieldListByIdV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates a shield list.
    /// </summary>
    /// <param name="shieldListId">
    /// The unique identifier that represents a shield list.
    /// The ID for any Shield List can be determined by the response from the endpoint
    /// fetching all shield lists for the enterprise.
    /// Example: "90fb0e17-c332-40ed-b4f9-fa8908fbbb24 "
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateShieldListByIdV2025R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of updateShieldListByIdV2025R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShieldListV2025R0> UpdateShieldListByIdV2025R0Async(string shieldListId, ShieldListsUpdateV2025R0 requestBody, UpdateShieldListByIdV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}