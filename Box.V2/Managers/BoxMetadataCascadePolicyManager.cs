using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Extensions;
using Box.V2.Models;
using Box.V2.Services;
using Newtonsoft.Json.Linq;

namespace Box.V2.Managers
{
    /// <summary>
    /// The class managing the Box API's Metadata Cascade Policies endpoint.
    /// </summary>
    public class BoxMetadataCascadePolicyManager : BoxResourceManager, IBoxMetadataCascadePolicyManager
    {
        /// <summary>
        /// Create a new BoxMetadataCascadePolicy object.
        /// </summary>
        /// <param name="config"></param>
        /// <param name="service"></param>
        /// <param name="converter"></param>
        /// <param name="auth"></param>
        /// <param name="asUser"></param>
        /// <param name="suppressNotifications"></param>
        public BoxMetadataCascadePolicyManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
            : base(config, service, converter, auth, asUser, suppressNotifications) { }

        /// <summary>
        /// Creates a metadata cascade policy on a folder and the sub folder items. 
        /// </summary>
        /// <param name="folderId">The id of the folder to assign the cascade policy to.</param>
        /// <param name="scope">The scope of the metadata cascade policy.</param>
        /// <param name="templateKey">The template key of the metadata cascade policy.</param>
        /// <returns>The metadata cascade policy if successfully created.</returns>
        public async Task<BoxMetadataCascadePolicy> CreateCascadePolicyAsync(string folderId, string scope, string templateKey)
        {
            folderId.ThrowIfNullOrWhiteSpace("folderId");
            scope.ThrowIfNullOrWhiteSpace("scope");
            templateKey.ThrowIfNullOrWhiteSpace("templateKey");

            dynamic jsonObject = new JObject();
            jsonObject.folder_id = folderId;
            jsonObject.scope = scope;
            jsonObject.templateKey = templateKey;

            string jsonString = jsonObject.ToString();

            BoxRequest request = new BoxRequest(_config.MetadataCascadePolicyUri)
                .Method(RequestMethod.Post)
                .Payload(jsonString);

            IBoxResponse<BoxMetadataCascadePolicy> response = await ToResponseAsync<BoxMetadataCascadePolicy>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Retrieves a metadata cascade policy by policy Id. 
        /// </summary>
        /// <param name="policyId">The metadata cascade policy Id to retrieve.</param>
        /// <param name="fields">Optional fields to retrieve on metadata cascade policy.</param>
        /// <returns>The metadata cascade policy retrieved by Id.</returns>
        public async Task<BoxMetadataCascadePolicy> GetCascadePolicyAsync(string policyId, IEnumerable<string> fields = null)
        {
            policyId.ThrowIfNullOrWhiteSpace("policyId");

            BoxRequest request = new BoxRequest(_config.MetadataCascadePolicyUri, policyId)
                .Param(ParamFields, fields);

            IBoxResponse<BoxMetadataCascadePolicy> response = await ToResponseAsync<BoxMetadataCascadePolicy>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        public async Task<BoxCollectionMarkerBased<BoxMetadataCascadePolicy>> GetAllMetadataCascadePoliciesAsync(string folderId, string ownerEnterpriseId = null, int limit = 100, string nextMarker = null, IEnumerable<string> fields = null, bool autopaginate = false)
        {
            folderId.ThrowIfNullOrWhiteSpace("folderId");

            BoxRequest request = new BoxRequest(_config.MetadataCascadePolicyUri)
                .Method(RequestMethod.Get)
                .Param(ParamFields, fields)
                .Param("folder_id", folderId)
                .Param("owner_enterprise_id", ownerEnterpriseId)
                .Param("limit", limit.ToString())
                .Param("marker", nextMarker);

            if (autopaginate)
            {
                return await AutoPaginateMarker<BoxMetadataCascadePolicy>(request, limit).ConfigureAwait(false);
            }
            else
            {
                IBoxResponse<BoxCollectionMarkerBased<BoxMetadataCascadePolicy>> response =
                    await ToResponseAsync<BoxCollectionMarkerBased<BoxMetadataCascadePolicy>>(request).ConfigureAwait(false);

                return response.ResponseObject;
            }
        }

        /// <summary>
        /// If a policy already exists on the specified folder, this will apply that new policy to the folder and the sub-folder items. 
        /// </summary>
        /// <param name="policyId">The policy Id to force apply.</param>
        /// <param name="conflictResolution">The desired behavior if a conflict exists. Set to either "none" or "overwrite".</param>
        public async Task<bool> ForceApplyCascadePolicyAsync(string policyId, string conflictResolution)
        {
            policyId.ThrowIfNullOrWhiteSpace("policyId");
            conflictResolution.ThrowIfNullOrWhiteSpace("conflictResolution");

            dynamic jsonObject = new JObject();
            jsonObject.conflict_resolution = conflictResolution;

            string jsonString = jsonObject.ToString();

            BoxRequest request = new BoxRequest(_config.MetadataCascadePolicyUri, string.Format(Constants.MetadataCascadePoliciesForceApplyPathString, policyId))
                .Method(RequestMethod.Post)
                .Payload(jsonString);

            IBoxResponse<BoxMetadataCascadePolicy> response = await ToResponseAsync<BoxMetadataCascadePolicy>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }

        /// <summary>
        /// Deletes a metadata cascade policy. 
        /// </summary>
        /// <param name="policyId">The id of the metadata cascade policy to delete.</param>
        /// <returns>True if metadata cascade policy was deleted successfully.</returns>
        public async Task<bool> DeleteCascadePolicyAsync(string policyId)
        {
            policyId.ThrowIfNullOrWhiteSpace("policyId");

            BoxRequest request = new BoxRequest(_config.MetadataCascadePolicyUri, policyId)
                .Method(RequestMethod.Delete);

            IBoxResponse<BoxMetadataCascadePolicy> response = await ToResponseAsync<BoxMetadataCascadePolicy>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }
    }
}
