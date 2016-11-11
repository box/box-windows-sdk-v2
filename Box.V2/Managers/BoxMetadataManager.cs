using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Extensions;
using Box.V2.Models;
using Box.V2.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Box.V2.Managers
{
    /// <summary>
    /// Metadata allows users and applications to define and store custom data associated with their files/folders
    /// </summary>
    public class BoxMetadataManager : BoxResourceManager
    {
        public BoxMetadataManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
            : base(config, service, converter, auth, asUser, suppressNotifications) { }

        /// <summary>
        /// Used to retrieve the metadata template instance for a corresponding Box file.
        /// </summary>
        /// <param name="fileId">Id of file</param>
        /// <param name="scope">Scope name. Currently, the only scopes supported are enterprise and global</param>
        /// <param name="template">Metadata template name</param>
        /// <returns>A Dictionary of key:value pairs representing the metadata.</returns>
        public async Task<Dictionary<string, object>> GetFileMetadataAsync(string fileId, string scope, string template)
        {
            return await GetMetadata(_config.FilesEndpointUri, fileId, scope, template);
        }

        /// <summary>
        /// Used to retrieve the metadata template instance for a corresponding Box folder.
        /// </summary>
        /// <param name="folderId">Id of folder</param>
        /// <param name="scope">Scope name. Currently, only the enterprise scope is supported</param>
        /// <param name="template">Metadata template name</param>
        /// <returns>A Dictionary of key:value pairs representing the metadata.</returns>
        public async Task<Dictionary<string, object>> GetFolderMetadataAsync(string folderId, string scope, string template)
        {
            return await GetMetadata(_config.FoldersEndpointUri, folderId, scope, template);
        }

        /// <summary>
        /// Used to create the metadata template instance for a corresponding Box file. When creating metadata, only values that adhere to the metadata template schema will be accepted.
        /// </summary>
        /// <param name="fileId">Id of file</param>
        /// <param name="metadata">Metadata to create</param>
        /// <param name="scope">Scope name. Currently, the only scopes support are enterprise and global</param>
        /// <param name="template">Metadata template name</param>
        /// <returns>A Dictionary of key:value pairs representing the metadata.</returns>
        public async Task<Dictionary<string, object>> CreateFileMetadataAsync(string fileId, Dictionary<string, object> metadata, string scope, string template)
        {
            return await CreateMetadata(_config.FilesEndpointUri, fileId, metadata, scope, template);
        }

        /// <summary>
        /// Used to create the metadata template instance for a corresponding Box folder. When creating metadata, only values that adhere to the metadata template schema will be accepted.
        /// </summary>
        /// <param name="folderId">Id of folder</param>
        /// <param name="metadata">Metadata to create</param>
        /// <param name="scope">Scope name. Currently, only the enterprise scope is supported</param>
        /// <param name="template">Metadata template name</param>
        /// <returns>A Dictionary of key:value pairs representing the metadata.</returns>
        public async Task<Dictionary<string, object>> CreateFolderMetadataAsync(string folderId, Dictionary<string, object> metadata, string scope, string template)
        {
            return await CreateMetadata(_config.FoldersEndpointUri, folderId, metadata, scope, template);
        }

        /// <summary>
        /// Used to update the template instance. The request body must follow the JSON-Patch specification, which is represented as a JSON array of operation objects (see examples for more details). Updates can be either add, replace, remove , test, move, or copy. The template instance can only be updated if the template instance already exists. When editing metadata, only values that adhere to the metadata template schema will be accepted.  The update is applied atomically. If any errors occur during the application of the update operations, the metadata instance remains unchanged.
        /// </summary>
        /// <param name="fileId">Id of file</param>
        /// <param name="updates">Metadata updates to apply</param>
        /// <param name="scope">Scope name. Currently, the only scopes support are enterprise and global</param>
        /// <param name="template">Metadata template name</param>
        /// <returns>A Dictionary of key:value pairs representing the metadata.</returns>
        public async Task<Dictionary<string, object>> UpdateFileMetadataAsync(string fileId, List<BoxMetadataUpdate> updates, string scope, string template)
        {
            return await UpdateMetadata(_config.FilesEndpointUri, fileId, updates, scope, template);
        }

        /// <summary>
        /// Used to update the template instance. Updates can be either add, replace, remove , or test. The template instance can only be updated if the template instance already exists. When editing metadata, only values that adhere to the metadata template schema will be accepted.
        /// </summary>
        /// <param name="folderId">Id of folder</param>
        /// <param name="updates">Metadata updates to apply</param>
        /// <param name="scope">Scope name. Currently, only the enterprise scope is supported</param>
        /// <param name="template">Metadata template name</param>
        /// <returns>A Dictionary of key:value pairs representing the metadata.</returns>
        public async Task<Dictionary<string, object>> UpdateFolderMetadataAsync(string folderId, List<BoxMetadataUpdate> updates, string scope, string template)
        {
            return await UpdateMetadata(_config.FoldersEndpointUri, folderId, updates, scope, template);
        }

        /// <summary>
        /// Used to delete the template instance. To delete custom key:value pairs within a template instance, you should refer to the updating metadata section.
        /// </summary>
        /// <param name="fileId">Id of file</param>
        /// <param name="scope">Scope name. Currently, the only scopes support are enterprise and global</param>
        /// <param name="template">Metadata template name</param>
        /// <returns>True if successful, false otherwise.</returns>
        public async Task<bool> DeleteFileMetadataAsync(string fileId, string scope, string template)
        {
            return await DeleteMetadata(_config.FilesEndpointUri, fileId, scope, template);
        }

        /// <summary>
        /// Used to delete the template instance. To delete custom key:value pairs within a template instance, you should refer to the updating metadata section.
        /// </summary>
        /// <param name="folderId">Id of folder</param>
        /// <param name="scope">Scope name. Currently, only the enterprise scope is supported</param>
        /// <param name="template">Metadata template name</param>
        /// <returns>True if successful, false otherwise.</returns>
        public async Task<bool> DeleteFolderMetadataAsync(string folderId, string scope, string template)
        {
            return await DeleteMetadata(_config.FoldersEndpointUri, folderId, scope, template);
        }

        /// <summary>
        /// Used to retrieve the schema for a given metadata template.
        /// </summary>
        /// <param name="scope">Scope name. Currently, the only scopes supported are enterprise and global</param>
        /// <param name="template">Metadata template name</param>
        /// <returns>Returns the schema for the specified metadata template.</returns>
        public async Task<BoxMetadataTemplate> GetMetadataTemplate(string scope, string template)
        {
            BoxRequest request = new BoxRequest(_config.MetadataTemplatesUri, string.Format(Constants.MetadataTemplatesPathString, scope, template));
            IBoxResponse<BoxMetadataTemplate> response = await ToResponseAsync<BoxMetadataTemplate>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to create a new metadata template with the specified schema.
        /// </summary>
        /// <param name="template">BoxMetadataTemplate object</param>
        /// <returns>The schema representing the metadata template created.</returns>
        public async Task<BoxMetadataTemplate> CreateMetadataTemplate(BoxMetadataTemplate template)
        {
            BoxRequest request = new BoxRequest(_config.CreateMetadataTemplateUri)
                .Method(RequestMethod.Post)
                .Payload(_converter.Serialize(template));

            request.ContentType = Constants.RequestParameters.ContentTypeJson;

            IBoxResponse<BoxMetadataTemplate> response = await ToResponseAsync<BoxMetadataTemplate>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to update the schema of an existing template.
        /// </summary>
        /// <param name="metadataTemplateUpdate">BoxMetadataTemplateUpdate object</param>
        /// <param name="scope">Scope name. Currently, the only scopes supported are enterprise and global</param>
        /// <param name="template">Metadata template name</param>
        /// <returns></returns>
        public async Task<BoxMetadataTemplate> UpdateMetadataTemplate(List<BoxMetadataTemplateUpdate> metadataTemplateUpdate, string scope, string template)
        {
            BoxRequest request = new BoxRequest(_config.MetadataTemplatesUri, string.Format(Constants.MetadataTemplatesPathString, scope, template))
                .Method(RequestMethod.Put)
                .Payload(_converter.Serialize(metadataTemplateUpdate));

            IBoxResponse<BoxMetadataTemplate> response = await ToResponseAsync<BoxMetadataTemplate>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to retrieve all metadata associated with a given file
        /// </summary>
        /// <param name="fileId">Id of file</param>
        /// <returns>Collection of metadata instances associated with the file.</returns>
        public async Task<BoxMetadataTemplateCollection<Dictionary<string, object>>> GetAllFileMetadataTemplatesAsync(string fileId)
        {
            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.AllFileMetadataPathString, fileId));
            IBoxResponse<BoxMetadataTemplateCollection<Dictionary<string, object>>> response = await ToResponseAsync<BoxMetadataTemplateCollection<Dictionary<string, object>>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to retrieve all metadata associated with a given folder
        /// </summary>
        /// <param name="folderId">Id of folder</param>
        /// <returns>Collection of metadata instances associated with the file.</returns>
        public async Task<BoxMetadataTemplateCollection<Dictionary<string, object>>> GetAllFolderMetadataTemplatesAsync(string folderId)
        {
            BoxRequest request = new BoxRequest(_config.FoldersEndpointUri, string.Format(Constants.AllFolderMetadataPathString, folderId));
            IBoxResponse<BoxMetadataTemplateCollection<Dictionary<string, object>>> response = await ToResponseAsync<BoxMetadataTemplateCollection<Dictionary<string, object>>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to retrieve all metadata templates within a user's enterprise. Currently only the enterprise scope is supported.
        /// </summary>
        /// <param name="scope">Scope name. Currently, the only scopes support are enterprise and global</param>
        /// <returns>Collection of enterprise metadata instances associated with the file.</returns>
        public async Task<BoxEnterpriseMetadataTemplateCollection<BoxMetadataTemplate>> GetEnterpriseMetadataAsync(string scope = "enterprise")
        {
            BoxRequest request = new BoxRequest(_config.MetadataTemplatesUri, string.Format(Constants.EnterpriseMetadataTemplatesPathString, scope));
            IBoxResponse<BoxEnterpriseMetadataTemplateCollection<BoxMetadataTemplate>> response = await ToResponseAsync<BoxEnterpriseMetadataTemplateCollection<BoxMetadataTemplate>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }



        //************************************
        //Private methods
        //************************************

        private async Task<Dictionary<string, object>> UpdateMetadata(Uri hostUri, string id, List<BoxMetadataUpdate> updates, string scope, string template)
        {
            foreach (BoxMetadataUpdate update in updates)
            {
                update.Path.ThrowIfNullOrWhiteSpace("Path");
                update.Op.ThrowIfNull("Op");
            }
            BoxRequest request = new BoxRequest(hostUri, string.Format(Constants.MetadataPathString, id, scope, template))
                .Method(RequestMethod.Put)
                .Payload(_converter.Serialize(updates));

            request.ContentType = Constants.RequestParameters.ContentTypeJsonPatch;
            IBoxResponse<Dictionary<string, object>> response = await ToResponseAsync<Dictionary<string, object>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        private async Task<Dictionary<string, object>> CreateMetadata(Uri hostUri, string id, Dictionary<string, object> metadata, string scope, string template)
        {
            BoxRequest request = new BoxRequest(hostUri, string.Format(Constants.MetadataPathString, id, scope, template))
                .Method(RequestMethod.Post)
                .Payload(_converter.Serialize(metadata));

            request.ContentType = Constants.RequestParameters.ContentTypeJson;
            IBoxResponse<Dictionary<string, object>> response = await ToResponseAsync<Dictionary<string, object>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        private async Task<Dictionary<string, object>> GetMetadata(Uri hostUri, string id, string scope, string template)
        {
            BoxRequest request = new BoxRequest(hostUri, string.Format(Constants.MetadataPathString, id, scope, template))
                .Method(RequestMethod.Get);
            IBoxResponse<Dictionary<string, object>> response = await ToResponseAsync<Dictionary<string, object>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        private async Task<bool> DeleteMetadata(Uri hostUri, string id, string scope, string template)
        {
            BoxRequest request = new BoxRequest(hostUri, string.Format(Constants.MetadataPathString, id, scope, template))
                .Method(RequestMethod.Delete);

            IBoxResponse<Dictionary<string, object>> response = await ToResponseAsync<Dictionary<string, object>>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }
    }
}
