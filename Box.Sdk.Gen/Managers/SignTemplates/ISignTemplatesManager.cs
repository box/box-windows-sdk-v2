using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface ISignTemplatesManager {
        /// <summary>
    /// Gets Box Sign templates created by a user.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getSignTemplates method
    /// </param>
    /// <param name="headers">
    /// Headers of getSignTemplates method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<SignTemplates> GetSignTemplatesAsync(GetSignTemplatesQueryParams? queryParams = default, GetSignTemplatesHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Fetches details of a specific Box Sign template.
    /// </summary>
    /// <param name="templateId">
    /// The ID of a Box Sign template.
    /// Example: "123075213-7d117509-8f05-42e4-a5ef-5190a319d41d"
    /// </param>
    /// <param name="headers">
    /// Headers of getSignTemplateById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<SignTemplate> GetSignTemplateByIdAsync(string templateId, GetSignTemplateByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}