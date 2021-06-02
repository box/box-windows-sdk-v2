using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2.Models;

namespace Box.V2.Managers
{
    /// <summary>
    /// Allow create, update, get, delete weblink for folder.
    /// </summary>
    public interface IBoxWebLinksManager
    {
        /// <summary>
        /// Creates a web link object within a given folder.
        /// </summary>
        /// <param name="createWebLinkRequest">BoxWebLinkRequest object</param>
        /// <returns>The web link object is returned.</returns>
        Task<BoxWebLink> CreateWebLinkAsync(BoxWebLinkRequest createWebLinkRequest);

        /// <summary>
        /// Deletes a web link and moves it to the trash.
        /// </summary>
        /// <param name="webLinkId">Id of the weblink.</param>
        /// <returns>True, if successfully deleted and moved to trash</returns>
        Task<bool> DeleteWebLinkAsync(string webLinkId);

        /// <summary>
        /// Use to get information about the web link.
        /// </summary>
        /// <param name="webLinkId">Id of the weblink.</param>
        /// <returns>The web link object is returned.</returns>
        Task<BoxWebLink> GetWebLinkAsync(string webLinkId);

        /// <summary>
        /// Updates information for a web link.
        /// </summary>
        /// <param name="webLinkId">Id of the weblink.</param>
        /// <param name="updateWebLinkRequest">BoxWebLinkRequest object</param>
        /// <returns>An updated web link object if the update was successful.</returns>
        Task<BoxWebLink> UpdateWebLinkAsync(string webLinkId, BoxWebLinkRequest updateWebLinkRequest);

        /// <summary>
        /// Used to create a copy of a web link in another folder. The original version of the web link will not be altered.
        /// </summary>
        /// <param name="webLinkId">The Id of the web link to copy.</param>
        /// <param name="destinationFolderId">The Id of the destination folder, where the new copy will be created.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>
        /// A full web link object is returned if the ID is valid and if the update is successful. 
        /// Errors can be thrown if the destination folder is invalid or if a name collision occurs. 
        /// </returns>
        Task<BoxWebLink> CopyAsync(string webLinkId, string destinationFolderId, IEnumerable<string> fields = null);

        /// <summary>
        /// Used to create a shared link for a web link.
        /// </summary>
        /// <param name="id">Id of the file.</param>
        /// <param name="sharedLinkRequest">BoxSharedLinkRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>A full web link object containing the updated shared link is returned
        /// if the ID is valid and if the update is successful.</returns>
        Task<BoxWebLink> CreateSharedLinkAsync(string id, BoxSharedLinkRequest sharedLinkRequest, IEnumerable<string> fields = null);

        /// <summary>
        /// Used to delete the shared link for this particular file.
        /// </summary>
        /// <param name="id">The id of the web link to remove the shared link from.</param>
        /// <returns>A full web link object with the shared link removed is returned
        /// if the ID is valid and if the update is successful.</returns>
        Task<BoxWebLink> DeleteSharedLinkAsync(string id, IEnumerable<string> fields = null);
    }
}
