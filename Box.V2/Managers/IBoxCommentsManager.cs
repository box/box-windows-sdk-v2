using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2.Models;

namespace Box.V2.Managers
{
    /// <summary>
    /// The manager that represents all of the comment endpoints
    /// </summary>
    public interface IBoxCommentsManager
    {
        /// <summary>
        /// Used to add a comment by the user to a specific file, discussion, or comment (i.e. as a reply comment).
        /// </summary>
        /// <param name="commentRequest">BoxCommentRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The new comment object is returned.</returns>
        Task<BoxComment> AddCommentAsync(BoxCommentRequest commentRequest, IEnumerable<string> fields = null);

        /// <summary>
        /// Used to retrieve the message and metadata about a specific comment. Information about the user who created the comment is also included.
        /// </summary>
        /// <param name="id">Id of the comment.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>A full comment object is returned is the ID is valid and if the user has access to the comment.</returns>
        Task<BoxComment> GetInformationAsync(string id, IEnumerable<string> fields = null);

        /// <summary>
        /// Used to update the message of the comment.
        /// </summary>
        /// <param name="id">Id of the comment.</param>
        /// <param name="commentsRequest">BoxCommentsRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The full updated comment object is returned if the ID is valid and if the user has access to the comment.</returns>
        Task<BoxComment> UpdateAsync(string id, BoxCommentRequest commentsRequest, IEnumerable<string> fields = null);

        /// <summary>
        /// Permanently deletes a comment.
        /// </summary>
        /// <param name="id">Id of the comment.</param>
        /// <returns>True is returned to confirm deletion of the comment.</returns>
        Task<bool> DeleteAsync(string id);
    }
}
