using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Extensions;
using Box.V2.Converter;
using Box.V2.Models;
using Box.V2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Managers
{
    /// <summary>
    /// Allow create, update, get, delete legal hold and legal hold assignment.
    /// </summary>
    public class BoxLegalHoldPoliciesManager : BoxResourceManager
    {

        public BoxLegalHoldPoliciesManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
            : base(config, service, converter, auth, asUser, suppressNotifications) { }

        /// <summary>
        /// Get details of a single Legal Hold Policy.
        /// </summary>
        /// <param name="legalHoldId">Id of the legal hold policy.</param>
        /// <returns>If the Id is valid, information about the Legal Hold Policy is returned.</returns>
        public async Task<BoxLegalHoldPolicy> GetLegalHoldPolicyAsync(string legalHoldId)
        {
            legalHoldId.ThrowIfNullOrWhiteSpace("legalHoldId");

            BoxRequest request = new BoxRequest(_config.LegalHoldPoliciesEndpointUri, legalHoldId)
                .Method(RequestMethod.Get);

            IBoxResponse<BoxLegalHoldPolicy> response = await ToResponseAsync<BoxLegalHoldPolicy>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Get a list of Legal Hold Policies that belong to your Enterprise.
        /// </summary>
        /// <param name="policyName">Case insensitive prefix-match filter on Policy name.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <param name="limit">Limit result size to this number. Defaults to 100, maximum is 1,000.</param>
        /// <param name="marker">Take from "next_marker" column of a prior call to get the next page.</param>
        /// <returns>Returns the list of Legal Hold Policies in your Enterprise that match the filter parameters (if passed in). By default, will only return only 'type', 'id', and 'policy_name', but you can specify more by using the 'fields' parameter.</returns>
        public async Task<BoxCollection<BoxLegalHoldPolicy>> GetListLegalHoldPoliciesAsync(string policyName, string fields = null, int? limit = null, string marker = null)
        {
            BoxRequest request = new BoxRequest(_config.LegalHoldPoliciesEndpointUri)
                .Method(RequestMethod.Get)
                .Param("policy_name", policyName)
                .Param("fields", fields)
                .Param("limit", limit == null ? null : limit.ToString())
                .Param("marker", marker);

            IBoxResponse<BoxCollection<BoxLegalHoldPolicy>> response = await ToResponseAsync<BoxCollection<BoxLegalHoldPolicy>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Create a new Legal Hold Policy. Optional date filter may be passed. 
        /// If Policy has a date filter, any Custodian assignments will apply only to file versions created or uploaded inside of the date range. 
        /// (Other assignment types, such as folders and files, will ignore the date filter).
        /// </summary>
        /// <param name="createRequest">Legal Hold Policy request
        /// createRequest.PolicyName (required) - Name of Legal Hold Policy. Max characters 254,
        /// createRequest.Description - Description of Legal Hold Policy. Max characters 500.
        /// </param>
        /// <returns>For a successful request, returns information about the Legal Hold Policy created. 
        /// If the Policy Name is in use for your enterprise, will return null.
        /// </returns>
        public async Task<BoxLegalHoldPolicy> CreateLegalHoldPolicyAsync(BoxLegalHoldPolicyRequest createRequest)
        {
            createRequest.ThrowIfNull("createRequest")
                .PolicyName.ThrowIfNull("createRequest.PolicyName");
            createRequest.ReleaseNotes = null;

            BoxRequest request = new BoxRequest(_config.LegalHoldPoliciesEndpointUri)
                .Method(RequestMethod.Post)
                .Payload(_converter.Serialize(createRequest));

            IBoxResponse<BoxLegalHoldPolicy> response = await ToResponseAsync<BoxLegalHoldPolicy>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Update existing Legal Hold Policy. Only name and description can be modified.
        /// </summary>
        /// <param name="legalHoldPolicyId">Id of the legal hold policy</param>
        /// <param name="updateRequest">Legal Hold Policy update request
        /// updateRequest.PolicyName - Name of Legal Hold Policy. Max characters 254,
        /// updateRequest.Description - Description of Legal Hold Policy. Max characters 500.
        /// updateRequest.ReleaseNotes - Notes around why the policy was released. Optional property with a 500 character limit.
        /// </param>
        /// <returns>Returns information about the Legal Hold Policy updated.</returns>
        public async Task<BoxLegalHoldPolicy> UpdateLegalHoldPolicyAsync(string legalHoldPolicyId, BoxLegalHoldPolicyRequest updateRequest)
        {
            legalHoldPolicyId.ThrowIfNull("legalHoldPolicyId");
            updateRequest.ThrowIfNull("updateRequest");
            updateRequest.FilterStartedAt = null;
            updateRequest.FilterEndedAt = null;

            BoxRequest request = new BoxRequest(_config.LegalHoldPoliciesEndpointUri, legalHoldPolicyId)
                .Method(RequestMethod.Put)
                .Payload(_converter.Serialize(updateRequest));

            IBoxResponse<BoxLegalHoldPolicy> response = await ToResponseAsync<BoxLegalHoldPolicy>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Sends request to delete an existing Legal Hold Policy. Note that this is an asynchronous process - the Policy will not be fully deleted yet when the response comes back.
        /// </summary>
        /// <param name="legalHoldPolicyId">Id of the legal hold policy.</param>
        /// <returns>Returns True if the request to delete the Policy was accepted.</returns>
        public async Task<bool> DeleteLegalHoldPolicyAsync(string legalHoldPolicyId)
        {
            legalHoldPolicyId.ThrowIfNull("legalHoldPolicyId");

            BoxRequest request = new BoxRequest(_config.LegalHoldPoliciesEndpointUri, legalHoldPolicyId)
               .Method(RequestMethod.Delete);

            IBoxResponse<BoxLegalHoldPolicy> response = await ToResponseAsync<BoxLegalHoldPolicy>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }

    }
}
