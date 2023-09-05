using System.Threading.Tasks;
using Box.V2.Models;
using Box.V2.Models.Request;


namespace Box.V2.Managers
{
    public interface IBoxSignTemplatesManager
    {
        /// <summary>
        /// Retrieves information about a sign template by ID.
        /// </summary>
        /// <param name="signTemplateId">Id of the sign template.</param>
        /// <returns>A full SignTemplate object is returned if the id is valid and if the user has access to the sign template.</returns>
        Task<BoxSignTemplate> GetSignTemplateByIdAsync(string signTemplateId);

        /// <summary>
        /// Retrieves information about all sign templates.
        /// </summary>
        /// <param name="limit">Limit result size to this number. Defaults to 100, maximum is 1,000.</param>
        /// <param name="nextMarker">Take from "next_marker" column of a prior call to get the next page.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all items; defaults to false.</param>
        /// <returns>A a collection of sign templates is returned if the id if the user has access.</returns>
        Task<BoxCollectionMarkerBased<BoxSignTemplate>> GetSignTemplatesAsync(int limit = 100, string nextMarker = null, bool autoPaginate = false);
    }
}
