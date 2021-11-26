using System.Threading.Tasks;
using Box.V2.Models;
using Box.V2.Models.Request;

namespace Box.V2.Managers
{
    /// <summary>
    /// The manager that represents all of the file requests endpoints.
    /// </summary>
    public interface IBoxFileRequestsManager
    {
        /// <summary>
        /// Retrieves the information about a file request by ID.
        /// </summary>
        /// <param name="fileRequestId">Id of the file request.</param>
        /// <returns>A full FileRequest object is returned if the id is valid and if the user has access to the file request.</returns>
        Task<BoxFileRequestObject> GetFileRequestByIdAsync(string fileRequestId);

        /// <summary>
        /// Copies an existing file request that is already present on one folder, and applies it to another folder.
        /// </summary>
        /// <param name="fileRequestId">Id of the file request.</param>
        /// <returns>A full FileRequest object is returned if the id is valid and if the user has access to the file request.</returns>
        Task<BoxFileRequestObject> CopyFileRequestAsync(string fileRequestId, BoxFileRequestCopyRequest copyRequest);

        /// <summary>
        /// Updates a file request. This can be used to activate or deactivate a file request.
        /// </summary>
        /// <param name="fileRequestId">Id of the file request.</param>
        /// <returns>A full FileRequest object is returned if the id is valid and if the user has access to the file request.</returns>
        Task<BoxFileRequestObject> UpdateFileRequestAsync(string fileRequestId, BoxFileRequestUpdateRequest updateRequest);

        /// <summary>
        /// Deletes a file request permanently.
        /// </summary>
        /// <param name="fileRequestId">Id of the file request.</param>
        /// <returns>True if successfully deleted.</returns>
        Task<bool> DeleteFileRequestAsync(string fileRequestId);
    }
}
