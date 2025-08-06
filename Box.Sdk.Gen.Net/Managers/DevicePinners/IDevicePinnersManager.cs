using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IDevicePinnersManager {
        /// <summary>
    /// Retrieves information about an individual device pin.
    /// </summary>
    /// <param name="devicePinnerId">
    /// The ID of the device pin.
    /// Example: "2324234"
    /// </param>
    /// <param name="headers">
    /// Headers of getDevicePinnerById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<DevicePinner> GetDevicePinnerByIdAsync(string devicePinnerId, GetDevicePinnerByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Deletes an individual device pin.
    /// </summary>
    /// <param name="devicePinnerId">
    /// The ID of the device pin.
    /// Example: "2324234"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteDevicePinnerById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteDevicePinnerByIdAsync(string devicePinnerId, DeleteDevicePinnerByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieves all the device pins within an enterprise.
    /// 
    /// The user must have admin privileges, and the application
    /// needs the "manage enterprise" scope to make this call.
    /// </summary>
    /// <param name="enterpriseId">
    /// The ID of the enterprise.
    /// Example: "3442311"
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of getEnterpriseDevicePinners method
    /// </param>
    /// <param name="headers">
    /// Headers of getEnterpriseDevicePinners method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<DevicePinners> GetEnterpriseDevicePinnersAsync(string enterpriseId, GetEnterpriseDevicePinnersQueryParams? queryParams = default, GetEnterpriseDevicePinnersHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}