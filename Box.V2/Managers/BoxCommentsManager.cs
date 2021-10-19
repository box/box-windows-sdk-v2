using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Extensions;
using Box.V2.Models;
using Box.V2.Services;

namespace Box.V2.Managers
{
    /// <summary>
    /// The manager that represents all of the comment endpoints
    /// </summary>
    public class BoxCommentsManager : BoxResourceManager, IBoxCommentsManager
    {
        public BoxCommentsManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
            : base(config, service, converter, auth, asUser, suppressNotifications) { }

        /// <summary>
        /// Used to add a comment by the user to a specific file, discussion, or comment (i.e. as a reply comment).
        /// </summary>
        /// <param name="commentRequest">BoxCommentRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The new comment object is returned.</returns>
        public async Task<BoxComment> AddCommentAsync(BoxCommentRequest commentRequest, IEnumerable<string> fields = null)
        {
            commentRequest.ThrowIfNull("commentRequest")
                .Item.ThrowIfNull("commentRequest.Item")
                .Id.ThrowIfNullOrWhiteSpace("commentRequest.Item.Id");
            if (commentRequest.Item.Type == null)
            {
                throw new ArgumentNullException("commentRequest.Item.Type");
            }

            BoxRequest request = new BoxRequest(_config.CommentsEndpointUri)
                .Method(RequestMethod.Post)
                .Param(ParamFields, fields)
                .Payload(_converter.Serialize(commentRequest));

            IBoxResponse<BoxComment> response = await ToResponseAsync<BoxComment>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to retrieve the message and metadata about a specific comment. Information about the user who created the comment is also included.
        /// </summary>
        /// <param name="id">Id of the comment.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>A full comment object is returned is the ID is valid and if the user has access to the comment.</returns>
        public async Task<BoxComment> GetInformationAsync(string id, IEnumerable<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.CommentsEndpointUri, id)
                .Method(RequestMethod.Get)
                .Param(ParamFields, fields);

            IBoxResponse<BoxComment> response = await ToResponseAsync<BoxComment>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to update the message of the comment.
        /// </summary>
        /// <param name="id">Id of the comment.</param>
        /// <param name="commentsRequest">BoxCommentsRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The full updated comment object is returned if the ID is valid and if the user has access to the comment.</returns>
        public async Task<BoxComment> UpdateAsync(string id, BoxCommentRequest commentsRequest, IEnumerable<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");
            commentsRequest.ThrowIfNull("commentsRequest")
                .Message.ThrowIfNullOrWhiteSpace("commentsRequest.Message");

            BoxRequest request = new BoxRequest(_config.CommentsEndpointUri, id)
                .Method(RequestMethod.Put)
                .Param(ParamFields, fields)
                .Payload(_converter.Serialize(commentsRequest));

            IBoxResponse<BoxComment> response = await ToResponseAsync<BoxComment>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Permanently deletes a comment.
        /// </summary>
        /// <param name="id">Id of the comment.</param>
        /// <returns>True is returned to confirm deletion of the comment.</returns>
        public async Task<bool> DeleteAsync(string id)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.CommentsEndpointUri, id)
                .Method(RequestMethod.Delete);

            IBoxResponse<BoxComment> response = await ToResponseAsync<BoxComment>(request).ConfigureAwait(false);
            return response.Status == ResponseStatus.Success;
        }
    }
}
