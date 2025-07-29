using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IShieldInformationBarrierReportsManager {
        /// <summary>
    /// Lists shield information barrier reports.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getShieldInformationBarrierReports method
    /// </param>
    /// <param name="headers">
    /// Headers of getShieldInformationBarrierReports method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShieldInformationBarrierReports> GetShieldInformationBarrierReportsAsync(GetShieldInformationBarrierReportsQueryParams queryParams, GetShieldInformationBarrierReportsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates a shield information barrier report for a given barrier.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createShieldInformationBarrierReport method
    /// </param>
    /// <param name="headers">
    /// Headers of createShieldInformationBarrierReport method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShieldInformationBarrierReport> CreateShieldInformationBarrierReportAsync(ShieldInformationBarrierReference requestBody, CreateShieldInformationBarrierReportHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieves a shield information barrier report by its ID.
    /// </summary>
    /// <param name="shieldInformationBarrierReportId">
    /// The ID of the shield information barrier Report.
    /// Example: "3423"
    /// </param>
    /// <param name="headers">
    /// Headers of getShieldInformationBarrierReportById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShieldInformationBarrierReport> GetShieldInformationBarrierReportByIdAsync(string shieldInformationBarrierReportId, GetShieldInformationBarrierReportByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}