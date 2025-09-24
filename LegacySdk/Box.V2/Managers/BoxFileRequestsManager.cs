using System.Threading.Tasks;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Extensions;
using Box.V2.Models;
using Box.V2.Models.Request;
using Box.V2.Services;

namespace Box.V2.Managers
{
    /// <summary>
    /// The manager that represents all of the file requests endpoints.
    /// </summary>
    public class BoxFileRequestsManager : BoxResourceManager, IBoxFileRequestsManager
    {
        public BoxFileRequestsManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
                : base(config, service, converter, auth, asUser, suppressNotifications) { }

        /// <summary>
        /// Retrieves the information about a file request by ID.
        /// </summary>
        /// <param name="fileRequestId">Id of the file request.</param>
        /// <returns>A full FileRequest object is returned if the id is valid and if the user has access to the file request.</returns>
        public async Task<BoxFileRequestObject> GetFileRequestByIdAsync(string fileRequestId)
        {
            fileRequestId.ThrowIfNullOrWhiteSpace("fileRequestId");

            var request = new BoxRequest(_config.FileRequestsEndpointWithPathUri, fileRequestId)
                .Method(RequestMethod.Get);

            IBoxResponse<BoxFileRequestObject> response = await ToResponseAsync<BoxFileRequestObject>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Copies an existing file request that is already present on one folder, and applies it to another folder.
        /// </summary>
        /// <param name="fileRequestId">Id of the file request.</param>
        /// <returns>A full FileRequest object is returned if the id is valid and if the user has access to the file request.</returns>
        public async Task<BoxFileRequestObject> CopyFileRequestAsync(string fileRequestId, BoxFileRequestCopyRequest copyRequest)
        {
            fileRequestId.ThrowIfNullOrWhiteSpace("fileRequestId");

            var request = new BoxRequest(_config.FileRequestsEndpointWithPathUri, string.Format(Constants.FileRequestsCopyPathString, fileRequestId))
                .Method(RequestMethod.Post)
                .Payload(_converter.Serialize(copyRequest));

            IBoxResponse<BoxFileRequestObject> response = await ToResponseAsync<BoxFileRequestObject>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Updates a file request. This can be used to activate or deactivate a file request.
        /// </summary>
        /// <param name="fileRequestId">Id of the file request.</param>
        /// <returns>A full FileRequest object is returned if the id is valid and if the user has access to the file request.</returns>
        public async Task<BoxFileRequestObject> UpdateFileRequestAsync(string fileRequestId, BoxFileRequestUpdateRequest updateRequest)
        {
            fileRequestId.ThrowIfNullOrWhiteSpace("fileRequestId");

            var request = new BoxRequest(_config.FileRequestsEndpointWithPathUri, fileRequestId)
               .Method(RequestMethod.Put)
               .Payload(_converter.Serialize(updateRequest));

            IBoxResponse<BoxFileRequestObject> response = await ToResponseAsync<BoxFileRequestObject>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Deletes a file request permanently.
        /// </summary>
        /// <param name="fileRequestId">Id of the file request.</param>
        /// <returns>True if successfully deleted.</returns>
        public async Task<bool> DeleteFileRequestAsync(string fileRequestId)
        {
            fileRequestId.ThrowIfNullOrWhiteSpace("fileRequestId");

            var request = new BoxRequest(_config.FileRequestsEndpointWithPathUri, fileRequestId)
                .Method(RequestMethod.Delete);

            var response = await ToResponseAsync<object>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }
    }
}
