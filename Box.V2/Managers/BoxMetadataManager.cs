using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Exceptions;
using Box.V2.Extensions;
using Box.V2.Models;
using Box.V2.Services;
using Newtonsoft.Json.Linq;
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
        /// Sets the provided metadata, overwriting any existing metadata on the file.
        /// </summary>
        /// <param name="fileId">The ID of the file to write metadata on.</param>
        /// <param name="metadata">The metadata key/value pairs to write.</param>
        /// <param name="scope">The scope of the metadata template to write to.</param>
        /// <param name="template">The key of the metadata template to write to.</param>
        /// <returns>The full metadata on the file, after writes are applied.</returns>
        public async Task<Dictionary<string, object>> SetFileMetadataAsync(string fileId, Dictionary<string, object> metadata, string scope, string template)
        {
            try
            {
                return await CreateFileMetadataAsync(fileId, metadata, scope, template);
            }
            catch (BoxException ex)
            {
                if (ex.StatusCode == HttpStatusCode.Conflict)
                {
                    // Metadata already exists, try updating instead
                    var updates = new List<BoxMetadataUpdate>();
                    foreach (KeyValuePair<string, object> md in metadata)
                    {
                        updates.Add(new BoxMetadataUpdate()
                        {
                            Op = MetadataUpdateOp.add,
                            Path = "/" + md.Key,
                            Value = md.Value,
                        });
                    }
                    return await UpdateFileMetadataAsync(fileId, updates, scope, template);
                }

                // Some other exception, just rethrow it
                throw ex;
            }
        }

        /// <summary>
        /// Sets the provided metadata, overwriting any existing metadata on the folder.
        /// </summary>
        /// <param name="folderId">The ID of the folder to write metadata on.</param>
        /// <param name="metadata">The metadata key/value pairs to write.</param>
        /// <param name="scope">The scope of the metadata template to write to.</param>
        /// <param name="template">The key of the metadata template to write to.</param>
        /// <returns>The full metadata on the folder, after writes are applied.</returns>
        public async Task<Dictionary<string, object>> SetFolderMetadataAsync(string folderId, Dictionary<string, object> metadata, string scope, string template)
        {
            try
            {
                return await CreateFolderMetadataAsync(folderId, metadata, scope, template);
            }
            catch (BoxException ex)
            {
                if (ex.StatusCode == HttpStatusCode.Conflict)
                {
                    // Metadata already exists, try updating instead
                    var updates = new List<BoxMetadataUpdate>();
                    foreach (KeyValuePair<string, object> md in metadata)
                    {
                        updates.Add(new BoxMetadataUpdate()
                        {
                            Op = MetadataUpdateOp.add,
                            Path = "/" + md.Key,
                            Value = md.Value,
                        });
                    }
                    return await UpdateFolderMetadataAsync(folderId, updates, scope, template);
                }

                // Some other exception, just rethrow it
                throw ex;
            }
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
        /// Used to delete an existing metadata template with the specified schema.
        /// </summary>
        /// <param name="scope">Scope name. Currently, the only scopes supported are enterprise and global</param>
        /// <param name="template">Metadata template name</param>
        /// <returns></returns>Returns boolean true if metadata schema was deleted successfully. 
        public async Task<bool> DeleteMetadataTemplate(string scope, string template)
        {
            BoxRequest request = new BoxRequest(_config.MetadataTemplatesUri, string.Format(Constants.MetadataTemplatesPathString, scope, template))
                .Method(RequestMethod.Delete);
            IBoxResponse<Dictionary<string, object>> response = await ToResponseAsync<Dictionary<string, object>>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }

        /// <summary>
        /// Used to update the schema of an existing template.
        /// </summary>
        /// <param name="metadataTemplateUpdate">BoxMetadataTemplateUpdate object</param>
        /// <param name="scope">Scope name. Currently, the only scopes supported are enterprise and global</param>
        /// <param name="template">Metadata template name</param>
        /// <returns></returns>
        public async Task<BoxMetadataTemplate> UpdateMetadataTemplate(IEnumerable<BoxMetadataTemplateUpdate> metadataTemplateUpdate, string scope, string template)
        {
            BoxRequest request = new BoxRequest(_config.MetadataTemplatesUri, string.Format(Constants.MetadataTemplatesPathString, scope, template))
                .Method(RequestMethod.Put)
                .Payload(_converter.Serialize(metadataTemplateUpdate));

            IBoxResponse<BoxMetadataTemplate> response = await ToResponseAsync<BoxMetadataTemplate>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to retrieve the schema for a given metadata template by metadata template id.
        /// </summary>
        /// <param name="templateId">Metadata template id.</param>
        /// <returns>Returns the schema for the specified metadata template.</returns>
        public async Task<BoxMetadataTemplate> GetMetadataTemplateById(string templateId)
        {
            BoxRequest request = new BoxRequest(_config.MetadataTemplatesUri, templateId);
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

        /// <summary>
        /// Allows you to query by metadata on Box items
        /// </summary>
        /// <param name="from">The template used in the query. Must be in the form scope.templateKey</param>
        /// <param name="ancestorFolderId">The folder_id to which to restrain the query</param>
        /// <param name="query">The logical expression of the query</param>
        /// <param name="queryParameters">Required if query present. The arguments for the query</param>
        /// <param name="indexName">The name of the Index to use</param>
        /// <param name="orderBy">A list of BoxMetadataQueryOrderBy objects that contain field_key(s) to order on and the corresponding direction(s)</param>
        /// <param name="limit">The maximum number of items to return in a page. The default is 100 and the max is 1000.</param>
        /// <param name="marker">The marker to use for requesting the next page</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all items; defaults to false.</param>
        /// <returns>A collection of items and their associated metadata</returns>
        [Obsolete("This method is deprecated in favor of ExecuteMetadataQueryAsync() that has a fields parameter. The API will eventually not support this method.")]
        public async Task<BoxCollectionMarkerBased<BoxMetadataQueryItem>> ExecuteMetadataQueryAsync(string from, string ancestorFolderId, string query = null, Dictionary<string, object> queryParameters = null, string indexName = null, List<BoxMetadataQueryOrderBy> orderBy = null, int limit = 100, string marker = null, bool autoPaginate = false)
        {
            from.ThrowIfNullOrWhiteSpace("from");
            ancestorFolderId.ThrowIfNullOrWhiteSpace("ancestorFolderId");

            JObject bodyObject = getMetadataQueryBody(from, ancestorFolderId, query, queryParameters, indexName, orderBy, null, limit, marker);

            BoxRequest request = new BoxRequest(_config.MetadataQueryUri)
                .Method(RequestMethod.Post)
                .Payload(_converter.Serialize(bodyObject));
            request.ContentType = Constants.RequestParameters.ContentTypeJson;

            if (autoPaginate)
            {
                return await AutoPaginateMarkerMetadataQuery<BoxMetadataQueryItem>(request);
            }
            else
            {
                IBoxResponse<BoxCollectionMarkerBased<BoxMetadataQueryItem>> response = await ToResponseAsync<BoxCollectionMarkerBased<BoxMetadataQueryItem>>(request).ConfigureAwait(false);
                return response.ResponseObject;
            }
        }

        /// <summary>
        /// Allows you to query by metadata on Box items with fields passed in
        /// </summary>
        /// <param name="from">The template used in the query. Must be in the form scope.templateKey</param>
        /// <param name="ancestorFolderId">The folder_id to which to restrain the query</param>
        /// <param name="fields">Attribute(s) to include in the response</param>
        /// <param name="query">The logical expression of the query</param>
        /// <param name="queryParameters">Required if query present. The arguments for the query</param>
        /// <param name="indexName">The name of the Index to use</param>
        /// <param name="orderBy">A list of BoxMetadataQueryOrderBy objects that contain field_key(s) to order on and the corresponding direction(s)</param>
        /// <param name="limit">The maximum number of items to return in a page. The default is 100 and the max is 1000.</param>
        /// <param name="marker">The marker to use for requesting the next page</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all items; defaults to false.</param>
        /// <returns>A collection of items and their associated metadata</returns>
        public async Task<BoxCollectionMarkerBased<BoxItem>> ExecuteMetadataQueryAsync(string from, string ancestorFolderId, IEnumerable<string> fields, string query = null, Dictionary<string, object> queryParameters = null, string indexName = null, List<BoxMetadataQueryOrderBy> orderBy = null, int limit = 100, string marker = null, bool autoPaginate = false)
        {
            from.ThrowIfNullOrWhiteSpace("from");
            ancestorFolderId.ThrowIfNullOrWhiteSpace("ancestorFolderId");
            if (fields == null)
            {
                throw new ArgumentException("Required field cannot be null", "fields");
            }

            JObject bodyObject = getMetadataQueryBody(from, ancestorFolderId, query, queryParameters, indexName, orderBy, fields, limit, marker);

            BoxRequest request = new BoxRequest(_config.MetadataQueryUri)
                .Method(RequestMethod.Post)
                .Payload(_converter.Serialize(bodyObject));
            request.ContentType = Constants.RequestParameters.ContentTypeJson;

            if (autoPaginate)
            {
                return await AutoPaginateMarkerMetadataQueryV2<BoxItem>(request);
            }
            else
            {
                IBoxResponse<BoxCollectionMarkerBased<BoxItem>> response = await ToResponseAsync<BoxCollectionMarkerBased<BoxItem>>(request).ConfigureAwait(false);
                return response.ResponseObject;
            }
        }

        //************************************
        //Private methods
        //************************************

        private JObject getMetadataQueryBody(string from, string ancestorFolderId, string query = null, Dictionary<string, object> queryParameters = null, string indexName = null, List<BoxMetadataQueryOrderBy> orderBy = null, IEnumerable<string> fields = null, int limit = 100, string marker = null)
        {
            dynamic bodyObject = new JObject();

            bodyObject.from = from;
            bodyObject.ancestor_folder_id = ancestorFolderId;
            bodyObject.limit = limit;

            if (query != null)
            {
                bodyObject.query = query;
            }

            if (queryParameters != null)
            {
                bodyObject.query_params = JObject.FromObject(queryParameters);
            }

            if (indexName != null)
            {
                bodyObject.use_index = indexName;
            }

            if (orderBy != null)
            {
                List<JObject> orderByList = new List<JObject>();
                foreach (var order in orderBy)
                {
                    dynamic orderByObject = new JObject();
                    orderByObject.field_key = order.FieldKey;
                    orderByObject.direction = order.Direction.ToString();
                    orderByList.Add(orderByObject);
                }
                bodyObject.order_by = JArray.FromObject(orderByList);
            }

            if (fields != null)
            {
                JArray fieldArray = new JArray();
                foreach (var field in fields)
                {
                    fieldArray.Add(field);
                }
                bodyObject.fields = fieldArray;
            }

            if (marker != null)
            {
                bodyObject.marker = marker;
            }

            return bodyObject;
        }

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
