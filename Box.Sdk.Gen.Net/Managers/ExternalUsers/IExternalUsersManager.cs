using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public interface IExternalUsersManager {
        /// <summary>
    /// Delete external users from current user enterprise. This will remove each
    /// external user from all invited collaborations within the current enterprise.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createExternalUserSubmitDeleteJobV2025R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of createExternalUserSubmitDeleteJobV2025R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ExternalUsersSubmitDeleteJobResponseV2025R0> CreateExternalUserSubmitDeleteJobV2025R0Async(ExternalUsersSubmitDeleteJobRequestV2025R0 requestBody, CreateExternalUserSubmitDeleteJobV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}