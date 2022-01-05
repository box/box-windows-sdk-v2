using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2.Models;

namespace Box.V2.Managers
{
    public interface IBoxCollaborationWhitelistManager
    {
        /// <summary>
        /// Used to whitelist a domain for a Box collaborator. You can specify the domain and direction of the whitelist. When whitelisted successfully, only users from the whitelisted
        /// domain can be invited as a collaborator. 
        /// </summary>
        /// <param name="domainToWhitelist">This is the domain to whitelist collaboration.</param>
        /// <param name="directionForWhitelist">Can be set to inbound, outbound, or both for the direction of the whitelist.</param>
        /// <returns>The whitelist entry if successfully created.</returns>
        Task<BoxCollaborationWhitelistEntry> AddCollaborationWhitelistEntryAsync(string domainToWhitelist, string directionForWhitelist);

        /// <summary>
        /// Used to get information about a single collaboration whitelist for domain.
        /// </summary>
        /// <param name="id">Id of the domain whitelist object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The domain collaboration whitelist object is returned. Errors may occur if id is invalid.</returns>
        Task<BoxCollaborationWhitelistEntry> GetCollaborationWhitelistEntryAsync(string id, IEnumerable<string> fields = null);

        /// <summary>
        /// Used to get information about all domain collaboration whitelists.
        /// </summary>
        /// <param name="marker">Position to return results from.</param>
        /// <param name="limit">Maximum number of entries to return. Default is 100.</param>
        /// <returns>The collection of domain collaboration whitelist objects is returned.</returns>
        Task<BoxCollectionMarkerBased<BoxCollaborationWhitelistEntry>> GetAllCollaborationWhitelistEntriesAsync(int limit = 100, string nextMarker = null, bool autoPaginate = false);

        /// <summary>
        /// Used to delete a domain collaboration whitelists.
        /// </summary>
        /// <param name="id">The id of the collaboration whitelist to delete.</param>
        /// <returns>A boolean value indicating whether or not the collaboration whitelist was successfully deleted.</returns>
        Task<bool> DeleteCollaborationWhitelistEntryAsync(string id);

        /// <summary>
        /// Used to add a user to the exempt user list. Once on the exempt user list this user is whitelisted as a collaborator.
        /// </summary>
        /// <param name="userId">This is the Box User to add to the exempt list.</param>
        /// <returns>The specific exempt user or user on the collaborator whitelist.</returns>
        Task<BoxCollaborationWhitelistTargetEntry> AddCollaborationWhitelistExemptUserAsync(string userId);

        /// <summary>
        /// Used to get information about a single collaboration whitelist for a user.
        /// </summary>
        /// <param name="id">Id of the collaboration whitelist exempt target object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The collaboration whitelist object for a user is returned. Errors may occur if id is invalid.</returns>
        Task<BoxCollaborationWhitelistTargetEntry> GetCollaborationWhitelistExemptUserAsync(string id, IEnumerable<string> fields = null);

        /// <summary>
        /// Used to get information about all collaboration whitelists for users.
        /// </summary>
        /// <param name="marker">Position to return results from.</param>
        /// <param name="limit">Maximum number of entries to return. Default is 100.</param>
        /// <returns>The collection of collaboration whitelist object is returned for users.</returns>
        Task<BoxCollectionMarkerBased<BoxCollaborationWhitelistTargetEntry>> GetAllCollaborationWhitelistExemptUsersAsync(int limit = 100, string nextMarker = null, bool autoPaginate = false);

        /// <summary>
        /// Used to delete a user from the exemption list or collaboration whitelist.
        /// </summary>
        /// <param name="id">The id of the collaboration whitelist to delete for user.</param>
        /// <returns>A boolean value indicating whether or not the user was successfully deleted from the collaboration whitelist.</returns>
        Task<bool> DeleteCollaborationWhitelistExemptUserAsync(string id);
    }
}
