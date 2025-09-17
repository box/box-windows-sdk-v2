using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IShieldInformationBarrierSegmentsManager {
        /// <summary>
    /// Retrieves shield information barrier segment based on provided ID..
    /// </summary>
    /// <param name="shieldInformationBarrierSegmentId">
    /// The ID of the shield information barrier segment.
    /// Example: "3423"
    /// </param>
    /// <param name="headers">
    /// Headers of getShieldInformationBarrierSegmentById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShieldInformationBarrierSegment> GetShieldInformationBarrierSegmentByIdAsync(string shieldInformationBarrierSegmentId, GetShieldInformationBarrierSegmentByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Deletes the shield information barrier segment
    /// based on provided ID.
    /// </summary>
    /// <param name="shieldInformationBarrierSegmentId">
    /// The ID of the shield information barrier segment.
    /// Example: "3423"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteShieldInformationBarrierSegmentById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteShieldInformationBarrierSegmentByIdAsync(string shieldInformationBarrierSegmentId, DeleteShieldInformationBarrierSegmentByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates the shield information barrier segment based on provided ID..
    /// </summary>
    /// <param name="shieldInformationBarrierSegmentId">
    /// The ID of the shield information barrier segment.
    /// Example: "3423"
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateShieldInformationBarrierSegmentById method
    /// </param>
    /// <param name="headers">
    /// Headers of updateShieldInformationBarrierSegmentById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShieldInformationBarrierSegment> UpdateShieldInformationBarrierSegmentByIdAsync(string shieldInformationBarrierSegmentId, UpdateShieldInformationBarrierSegmentByIdRequestBody? requestBody = default, UpdateShieldInformationBarrierSegmentByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieves a list of shield information barrier segment objects
    /// for the specified Information Barrier ID.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getShieldInformationBarrierSegments method
    /// </param>
    /// <param name="headers">
    /// Headers of getShieldInformationBarrierSegments method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShieldInformationBarrierSegments> GetShieldInformationBarrierSegmentsAsync(GetShieldInformationBarrierSegmentsQueryParams queryParams, GetShieldInformationBarrierSegmentsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates a shield information barrier segment.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createShieldInformationBarrierSegment method
    /// </param>
    /// <param name="headers">
    /// Headers of createShieldInformationBarrierSegment method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShieldInformationBarrierSegment> CreateShieldInformationBarrierSegmentAsync(CreateShieldInformationBarrierSegmentRequestBody requestBody, CreateShieldInformationBarrierSegmentHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}