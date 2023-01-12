using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2.Models;
using Box.V2.Models.Request;

namespace Box.V2.Managers
{
    /// <summary>
    /// Metadata allows users and applications to define and store custom data associated with their files/folders
    /// </summary>
    public interface IBoxMetadataManager
    {
        /// <summary>
        /// Used to retrieve the metadata template instance for a corresponding Box file.
        /// </summary>
        /// <param name="fileId">Id of file</param>
        /// <param name="scope">Scope name. Currently, the only scopes supported are enterprise and global</param>
        /// <param name="template">Metadata template name</param>
        /// <returns>A Dictionary of key:value pairs representing the metadata.</returns>
        Task<Dictionary<string, object>> GetFileMetadataAsync(string fileId, string scope, string template);

        /// <summary>
        /// Used to retrieve the metadata template instance for a corresponding Box folder.
        /// </summary>
        /// <param name="folderId">Id of folder</param>
        /// <param name="scope">Scope name. Currently, only the enterprise scope is supported</param>
        /// <param name="template">Metadata template name</param>
        /// <returns>A Dictionary of key:value pairs representing the metadata.</returns>
        Task<Dictionary<string, object>> GetFolderMetadataAsync(string folderId, string scope, string template);

        /// <summary>
        /// Used to create the metadata template instance for a corresponding Box file. When creating metadata, only values that adhere to the metadata template schema will be accepted.
        /// </summary>
        /// <param name="fileId">Id of file</param>
        /// <param name="metadata">Metadata to create</param>
        /// <param name="scope">Scope name. Currently, the only scopes support are enterprise and global</param>
        /// <param name="template">Metadata template name</param>
        /// <returns>A Dictionary of key:value pairs representing the metadata.</returns>
        Task<Dictionary<string, object>> CreateFileMetadataAsync(string fileId, Dictionary<string, object> metadata, string scope, string template);

        /// <summary>
        /// Used to create the metadata template instance for a corresponding Box folder. When creating metadata, only values that adhere to the metadata template schema will be accepted.
        /// </summary>
        /// <param name="folderId">Id of folder</param>
        /// <param name="metadata">Metadata to create</param>
        /// <param name="scope">Scope name. Currently, only the enterprise scope is supported</param>
        /// <param name="template">Metadata template name</param>
        /// <returns>A Dictionary of key:value pairs representing the metadata.</returns>
        Task<Dictionary<string, object>> CreateFolderMetadataAsync(string folderId, Dictionary<string, object> metadata, string scope, string template);

        /// <summary>
        /// Used to update the template instance. The request body must follow the JSON-Patch specification, which is represented as a JSON array of operation objects (see examples for more details). Updates can be either add, replace, remove , test, move, or copy. The template instance can only be updated if the template instance already exists. When editing metadata, only values that adhere to the metadata template schema will be accepted.  The update is applied atomically. If any errors occur during the application of the update operations, the metadata instance remains unchanged.
        /// </summary>
        /// <param name="fileId">Id of file</param>
        /// <param name="updates">Metadata updates to apply</param>
        /// <param name="scope">Scope name. Currently, the only scopes support are enterprise and global</param>
        /// <param name="template">Metadata template name</param>
        /// <returns>A Dictionary of key:value pairs representing the metadata.</returns>
        Task<Dictionary<string, object>> UpdateFileMetadataAsync(string fileId, List<BoxMetadataUpdate> updates, string scope, string template);

        /// <summary>
        /// Used to update the template instance. Updates can be either add, replace, remove , or test. The template instance can only be updated if the template instance already exists. When editing metadata, only values that adhere to the metadata template schema will be accepted.
        /// </summary>
        /// <param name="folderId">Id of folder</param>
        /// <param name="updates">Metadata updates to apply</param>
        /// <param name="scope">Scope name. Currently, only the enterprise scope is supported</param>
        /// <param name="template">Metadata template name</param>
        /// <returns>A Dictionary of key:value pairs representing the metadata.</returns>
        Task<Dictionary<string, object>> UpdateFolderMetadataAsync(string folderId, List<BoxMetadataUpdate> updates, string scope, string template);

        /// <summary>
        /// Sets the provided metadata, overwriting any existing metadata on the file.
        /// </summary>
        /// <param name="fileId">The ID of the file to write metadata on.</param>
        /// <param name="metadata">The metadata key/value pairs to write.</param>
        /// <param name="scope">The scope of the metadata template to write to.</param>
        /// <param name="template">The key of the metadata template to write to.</param>
        /// <returns>The full metadata on the file, after writes are applied.</returns>
        Task<Dictionary<string, object>> SetFileMetadataAsync(string fileId, Dictionary<string, object> metadata, string scope, string template);

        /// <summary>
        /// Sets the provided metadata, overwriting any existing metadata on the folder.
        /// </summary>
        /// <param name="folderId">The ID of the folder to write metadata on.</param>
        /// <param name="metadata">The metadata key/value pairs to write.</param>
        /// <param name="scope">The scope of the metadata template to write to.</param>
        /// <param name="template">The key of the metadata template to write to.</param>
        /// <returns>The full metadata on the folder, after writes are applied.</returns>
        Task<Dictionary<string, object>> SetFolderMetadataAsync(string folderId, Dictionary<string, object> metadata, string scope, string template);

        /// <summary>
        /// Used to delete the template instance. To delete custom key:value pairs within a template instance, you should refer to the updating metadata section.
        /// </summary>
        /// <param name="fileId">Id of file</param>
        /// <param name="scope">Scope name. Currently, the only scopes support are enterprise and global</param>
        /// <param name="template">Metadata template name</param>
        /// <returns>True if successful, false otherwise.</returns>
        Task<bool> DeleteFileMetadataAsync(string fileId, string scope, string template);

        /// <summary>
        /// Used to delete the template instance. To delete custom key:value pairs within a template instance, you should refer to the updating metadata section.
        /// </summary>
        /// <param name="folderId">Id of folder</param>
        /// <param name="scope">Scope name. Currently, only the enterprise scope is supported</param>
        /// <param name="template">Metadata template name</param>
        /// <returns>True if successful, false otherwise.</returns>
        Task<bool> DeleteFolderMetadataAsync(string folderId, string scope, string template);

        /// <summary>
        /// Used to retrieve the schema for a given metadata template.
        /// </summary>
        /// <param name="scope">Scope name. Currently, the only scopes supported are enterprise and global</param>
        /// <param name="template">Metadata template name</param>
        /// <returns>Returns the schema for the specified metadata template.</returns>
        Task<BoxMetadataTemplate> GetMetadataTemplate(string scope, string template);

        /// <summary>
        /// Used to create a new metadata template with the specified schema.
        /// </summary>
        /// <param name="template">BoxMetadataTemplate object</param>
        /// <returns>The schema representing the metadata template created.</returns>
        Task<BoxMetadataTemplate> CreateMetadataTemplate(BoxMetadataTemplate template);

        /// <summary>
        /// Used to delete an existing metadata template with the specified schema.
        /// </summary>
        /// <param name="scope">Scope name. Currently, the only scopes supported are enterprise and global</param>
        /// <param name="template">Metadata template name</param>
        /// <returns></returns>Returns boolean true if metadata schema was deleted successfully. 
        Task<bool> DeleteMetadataTemplate(string scope, string template);

        /// <summary>
        /// Used to update the schema of an existing template.
        /// </summary>
        /// <param name="metadataTemplateUpdate">BoxMetadataTemplateUpdate object</param>
        /// <param name="scope">Scope name. Currently, the only scopes supported are enterprise and global</param>
        /// <param name="template">Metadata template name</param>
        /// <returns></returns>
        Task<BoxMetadataTemplate> UpdateMetadataTemplate(IEnumerable<BoxMetadataTemplateUpdate> metadataTemplateUpdate, string scope, string template);

        /// <summary>
        /// Used to retrieve the schema for a given metadata template by metadata template id.
        /// </summary>
        /// <param name="templateId">Metadata template id.</param>
        /// <returns>Returns the schema for the specified metadata template.</returns>
        Task<BoxMetadataTemplate> GetMetadataTemplateById(string templateId);

        /// <summary>
        /// Used to retrieve all metadata associated with a given file
        /// </summary>
        /// <param name="fileId">Id of file</param>
        /// <returns>Collection of metadata instances associated with the file.</returns>
        Task<BoxMetadataTemplateCollection<Dictionary<string, object>>> GetAllFileMetadataTemplatesAsync(string fileId);

        /// <summary>
        /// Used to retrieve all metadata associated with a given folder
        /// </summary>
        /// <param name="folderId">Id of folder</param>
        /// <returns>Collection of metadata instances associated with the file.</returns>
        Task<BoxMetadataTemplateCollection<Dictionary<string, object>>> GetAllFolderMetadataTemplatesAsync(string folderId);

        /// <summary>
        /// Used to retrieve all metadata templates within a user's enterprise. Currently only the enterprise scope is supported.
        /// </summary>
        /// <param name="scope">Scope name. Currently, the only scopes support are enterprise and global</param>
        /// <returns>Collection of enterprise metadata instances associated with the file.</returns>
        Task<BoxEnterpriseMetadataTemplateCollection<BoxMetadataTemplate>> GetEnterpriseMetadataAsync(string scope = "enterprise");

        /// <summary>
        /// Allows you to query by metadata on Box items with fields passed in
        /// </summary>
        /// <param name="queryRequest">Request object.</param>
        /// <returns>A collection of items and their associated metadata</returns>
        Task<BoxCollectionMarkerBased<BoxItem>> ExecuteMetadataQueryAsync(BoxMetadataQueryRequest queryRequest);
    }
}
