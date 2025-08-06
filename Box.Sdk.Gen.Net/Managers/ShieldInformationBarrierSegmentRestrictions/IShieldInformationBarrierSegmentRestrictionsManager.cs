using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IShieldInformationBarrierSegmentRestrictionsManager {
        /// <summary>
    /// Retrieves a shield information barrier segment
    /// restriction based on provided ID.
    /// </summary>
    /// <param name="shieldInformationBarrierSegmentRestrictionId">
    /// The ID of the shield information barrier segment Restriction.
    /// Example: "4563"
    /// </param>
    /// <param name="headers">
    /// Headers of getShieldInformationBarrierSegmentRestrictionById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShieldInformationBarrierSegmentRestriction> GetShieldInformationBarrierSegmentRestrictionByIdAsync(string shieldInformationBarrierSegmentRestrictionId, GetShieldInformationBarrierSegmentRestrictionByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Delete shield information barrier segment restriction
    /// based on provided ID.
    /// </summary>
    /// <param name="shieldInformationBarrierSegmentRestrictionId">
    /// The ID of the shield information barrier segment Restriction.
    /// Example: "4563"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteShieldInformationBarrierSegmentRestrictionById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteShieldInformationBarrierSegmentRestrictionByIdAsync(string shieldInformationBarrierSegmentRestrictionId, DeleteShieldInformationBarrierSegmentRestrictionByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Lists shield information barrier segment restrictions
    /// based on provided segment ID.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getShieldInformationBarrierSegmentRestrictions method
    /// </param>
    /// <param name="headers">
    /// Headers of getShieldInformationBarrierSegmentRestrictions method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShieldInformationBarrierSegmentRestrictions> GetShieldInformationBarrierSegmentRestrictionsAsync(GetShieldInformationBarrierSegmentRestrictionsQueryParams queryParams, GetShieldInformationBarrierSegmentRestrictionsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates a shield information barrier
    /// segment restriction object.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createShieldInformationBarrierSegmentRestriction method
    /// </param>
    /// <param name="headers">
    /// Headers of createShieldInformationBarrierSegmentRestriction method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShieldInformationBarrierSegmentRestriction> CreateShieldInformationBarrierSegmentRestrictionAsync(CreateShieldInformationBarrierSegmentRestrictionRequestBody requestBody, CreateShieldInformationBarrierSegmentRestrictionHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}