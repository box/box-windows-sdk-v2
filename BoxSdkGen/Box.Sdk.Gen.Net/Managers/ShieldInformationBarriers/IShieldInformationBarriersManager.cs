using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IShieldInformationBarriersManager {
        /// <summary>
    /// Get shield information barrier based on provided ID.
    /// </summary>
    /// <param name="shieldInformationBarrierId">
    /// The ID of the shield information barrier.
    /// Example: "1910967"
    /// </param>
    /// <param name="headers">
    /// Headers of getShieldInformationBarrierById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShieldInformationBarrier> GetShieldInformationBarrierByIdAsync(string shieldInformationBarrierId, GetShieldInformationBarrierByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Change status of shield information barrier with the specified ID.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of updateShieldInformationBarrierStatus method
    /// </param>
    /// <param name="headers">
    /// Headers of updateShieldInformationBarrierStatus method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShieldInformationBarrier> UpdateShieldInformationBarrierStatusAsync(UpdateShieldInformationBarrierStatusRequestBody requestBody, UpdateShieldInformationBarrierStatusHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieves a list of shield information barrier objects
    /// for the enterprise of JWT.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getShieldInformationBarriers method
    /// </param>
    /// <param name="headers">
    /// Headers of getShieldInformationBarriers method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShieldInformationBarriers> GetShieldInformationBarriersAsync(GetShieldInformationBarriersQueryParams? queryParams = default, GetShieldInformationBarriersHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates a shield information barrier to
    /// separate individuals/groups within the same
    /// firm and prevents confidential information passing between them.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createShieldInformationBarrier method
    /// </param>
    /// <param name="headers">
    /// Headers of createShieldInformationBarrier method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShieldInformationBarrier> CreateShieldInformationBarrierAsync(CreateShieldInformationBarrierRequestBody requestBody, CreateShieldInformationBarrierHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}