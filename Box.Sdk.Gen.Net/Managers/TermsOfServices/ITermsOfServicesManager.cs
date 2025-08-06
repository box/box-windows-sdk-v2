using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface ITermsOfServicesManager {
        /// <summary>
    /// Returns the current terms of service text and settings
    /// for the enterprise.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getTermsOfService method
    /// </param>
    /// <param name="headers">
    /// Headers of getTermsOfService method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<TermsOfServices> GetTermsOfServiceAsync(GetTermsOfServiceQueryParams? queryParams = default, GetTermsOfServiceHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates a terms of service for a given enterprise
    /// and type of user.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createTermsOfService method
    /// </param>
    /// <param name="headers">
    /// Headers of createTermsOfService method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<TermsOfService> CreateTermsOfServiceAsync(CreateTermsOfServiceRequestBody requestBody, CreateTermsOfServiceHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Fetches a specific terms of service.
    /// </summary>
    /// <param name="termsOfServiceId">
    /// The ID of the terms of service.
    /// Example: "324234"
    /// </param>
    /// <param name="headers">
    /// Headers of getTermsOfServiceById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<TermsOfService> GetTermsOfServiceByIdAsync(string termsOfServiceId, GetTermsOfServiceByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates a specific terms of service.
    /// </summary>
    /// <param name="termsOfServiceId">
    /// The ID of the terms of service.
    /// Example: "324234"
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateTermsOfServiceById method
    /// </param>
    /// <param name="headers">
    /// Headers of updateTermsOfServiceById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<TermsOfService> UpdateTermsOfServiceByIdAsync(string termsOfServiceId, UpdateTermsOfServiceByIdRequestBody requestBody, UpdateTermsOfServiceByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}