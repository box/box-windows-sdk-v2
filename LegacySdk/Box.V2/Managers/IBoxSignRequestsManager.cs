using System.Threading.Tasks;
using Box.V2.Models;
using Box.V2.Models.Request;

namespace Box.V2.Managers
{
    /// <summary>
    /// The manager that represents all of the sign requests endpoints.
    /// </summary>
    public interface IBoxSignRequestsManager
    {
        /// <summary>
        /// Retrieves information about a sign requests by ID.
        /// </summary>
        /// <param name="signRequestId">Id of the sign request.</param>
        /// <returns>A full SignRequest object is returned if the id is valid and if the user has access to the sign request.</returns>
        Task<BoxSignRequest> GetSignRequestByIdAsync(string signRequestId);

        /// <summary>
        /// Retrieves information about all sign requests.
        /// </summary>
        /// <param name="limit">Limit result size to this number. Defaults to 100, maximum is 1,000.</param>
        /// <param name="nextMarker">Take from "next_marker" column of a prior call to get the next page.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all items; defaults to false.</param>
        /// <returns>A a collection of sign requests is returned if the id if the user has access.</returns>
        Task<BoxCollectionMarkerBased<BoxSignRequest>> GetSignRequestsAsync(int limit = 100, string nextMarker = null, bool autoPaginate = false);

        /// <summary>
        /// Create a sign request object.
        /// </summary>
        /// <param name="signRequestCreateRequest">Sign request create request object in order to create a sign request object.</param>
        /// <returns>The sign request object that was successfully created.</returns>
        Task<BoxSignRequest> CreateSignRequestAsync(BoxSignRequestCreateRequest signRequestCreateRequest);

        /// <summary>
        /// Cancels a sign request.
        /// </summary>
        /// <param name="signRequestId">Id of the sign request.</param>
        /// <returns>The sign request object that was successfully cancelled.</returns>
        Task<BoxSignRequest> CancelSignRequestAsync(string signRequestId);

        /// <summary>
        /// Resends a sign request email to all outstanding signers.
        /// </summary>
        /// <param name="signRequestId">Id of the sign request.</param>
        Task ResendSignRequestAsync(string signRequestId);
    }
}
