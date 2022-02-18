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
    /// The manager that represents all of the sign requests endpoints.
    /// </summary>
    public class BoxSignRequestsManager : BoxResourceManager, IBoxSignRequestsManager
    {
        public BoxSignRequestsManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
            : base(config, service, converter, auth, asUser, suppressNotifications) { }

        /// <summary>
        /// Retrieves information about a sign requests by ID.
        /// </summary>
        /// <param name="signRequestId">Id of the sign request.</param>
        /// <returns>A full SignRequest object is returned if the id is valid and if the user has access to the sign request.</returns>
        public async Task<BoxSignRequest> GetSignRequestByIdAsync(string signRequestId)
        {
            signRequestId.ThrowIfNullOrWhiteSpace("signRequestId");

            var request = new BoxRequest(_config.SignRequestsEndpointWithPathUri, signRequestId)
                .Method(RequestMethod.Get);

            IBoxResponse<BoxSignRequest> response = await ToResponseAsync<BoxSignRequest>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Retrieves information about all sign requests.
        /// </summary>
        /// <param name="limit">Limit result size to this number. Defaults to 100, maximum is 1,000.</param>
        /// <param name="nextMarker">Take from "next_marker" column of a prior call to get the next page.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all items; defaults to false.</param>
        /// <returns>A a collection of sign requests is returned if the id if the user has access.</returns>
        public async Task<BoxCollectionMarkerBased<BoxSignRequest>> GetSignRequestsAsync(int limit = 100, string nextMarker = null, bool autoPaginate = false)
        {
            BoxRequest request = new BoxRequest(_config.SignRequestsEndpointUri)
                .Param("limit", limit.ToString())
                .Param("marker", nextMarker)
                .Method(RequestMethod.Get);

            if (autoPaginate)
            {
                return await AutoPaginateMarker<BoxSignRequest>(request, limit).ConfigureAwait(false);
            }
            else
            {
                var response = await ToResponseAsync<BoxCollectionMarkerBased<BoxSignRequest>>(request).ConfigureAwait(false);
                return response.ResponseObject;
            }
        }

        /// <summary>
        /// Create a sign request object.
        /// </summary>
        /// <param name="signRequestCreateRequest">Sign request create request object in order to create a sign request object.</param>
        /// <returns>The sign request object that was successfully created.</returns>
        public async Task<BoxSignRequest> CreateSignRequestAsync(BoxSignRequestCreateRequest signRequestCreateRequest)
        {
            signRequestCreateRequest.ThrowIfNull("signRequestCreateRequest")
                .Signers.ThrowIfNull("signRequestCreateRequest.Signers");
            signRequestCreateRequest.SourceFiles.ThrowIfNull("signRequestCreateRequest.SourceFiles");
            signRequestCreateRequest.ParentFolder.ThrowIfNull("signRequestCreateRequest.ParentFolder")
                .Id.ThrowIfNull("signRequestCreateRequest.ParentFolder.Id");

            BoxRequest request = new BoxRequest(_config.SignRequestsEndpointUri)
                .Method(RequestMethod.Post)
                .Payload(_converter.Serialize(signRequestCreateRequest));

            IBoxResponse<BoxSignRequest> response = await ToResponseAsync<BoxSignRequest>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Cancels a sign request.
        /// </summary>
        /// <param name="signRequestId">Id of the sign request.</param>
        /// <returns>The sign request object that was successfully cancelled.</returns>
        public async Task<BoxSignRequest> CancelSignRequestAsync(string signRequestId)
        {
            signRequestId.ThrowIfNullOrWhiteSpace("signRequestId");

            BoxRequest request = new BoxRequest(_config.SignRequestsEndpointWithPathUri, string.Format(Constants.SignRequestsCancelPathString, signRequestId))
                .Method(RequestMethod.Post);

            IBoxResponse<BoxSignRequest> response = await ToResponseAsync<BoxSignRequest>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Resends a sign request email to all outstanding signers.
        /// </summary>
        /// <param name="signRequestId">Id of the sign request.</param>
        public async Task ResendSignRequestAsync(string signRequestId)
        {
            signRequestId.ThrowIfNullOrWhiteSpace("signRequestId");

            BoxRequest request = new BoxRequest(_config.SignRequestsEndpointWithPathUri, string.Format(Constants.SignRequestsResendPathString, signRequestId))
                .Method(RequestMethod.Post);

            await ToResponseAsync<object>(request).ConfigureAwait(false);

            return;
        }
    }
}
