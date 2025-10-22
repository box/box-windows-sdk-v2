using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public interface IEnterpriseConfigurationsManager {
        /// <summary>
    /// Retrieves the configuration for an enterprise.
    /// </summary>
    /// <param name="enterpriseId">
    /// The ID of the enterprise.
    /// Example: "3442311"
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of getEnterpriseConfigurationByIdV2025R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of getEnterpriseConfigurationByIdV2025R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<EnterpriseConfigurationV2025R0> GetEnterpriseConfigurationByIdV2025R0Async(string enterpriseId, GetEnterpriseConfigurationByIdV2025R0QueryParams queryParams, GetEnterpriseConfigurationByIdV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}