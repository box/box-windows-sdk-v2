using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IShieldInformationBarrierSegmentMembersManager {
        /// <summary>
    /// Retrieves a shield information barrier
    /// segment member by its ID.
    /// </summary>
    /// <param name="shieldInformationBarrierSegmentMemberId">
    /// The ID of the shield information barrier segment Member.
    /// Example: "7815"
    /// </param>
    /// <param name="headers">
    /// Headers of getShieldInformationBarrierSegmentMemberById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShieldInformationBarrierSegmentMember> GetShieldInformationBarrierSegmentMemberByIdAsync(string shieldInformationBarrierSegmentMemberId, GetShieldInformationBarrierSegmentMemberByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Deletes a shield information barrier
    /// segment member based on provided ID.
    /// </summary>
    /// <param name="shieldInformationBarrierSegmentMemberId">
    /// The ID of the shield information barrier segment Member.
    /// Example: "7815"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteShieldInformationBarrierSegmentMemberById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteShieldInformationBarrierSegmentMemberByIdAsync(string shieldInformationBarrierSegmentMemberId, DeleteShieldInformationBarrierSegmentMemberByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Lists shield information barrier segment members
    /// based on provided segment IDs.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getShieldInformationBarrierSegmentMembers method
    /// </param>
    /// <param name="headers">
    /// Headers of getShieldInformationBarrierSegmentMembers method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShieldInformationBarrierSegmentMembers> GetShieldInformationBarrierSegmentMembersAsync(GetShieldInformationBarrierSegmentMembersQueryParams queryParams, GetShieldInformationBarrierSegmentMembersHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates a new shield information barrier segment member.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createShieldInformationBarrierSegmentMember method
    /// </param>
    /// <param name="headers">
    /// Headers of createShieldInformationBarrierSegmentMember method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ShieldInformationBarrierSegmentMember> CreateShieldInformationBarrierSegmentMemberAsync(CreateShieldInformationBarrierSegmentMemberRequestBody requestBody, CreateShieldInformationBarrierSegmentMemberHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}