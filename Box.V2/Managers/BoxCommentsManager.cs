using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Extensions;
using Box.V2.Models;
using Box.V2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Managers
{
    public class BoxCommentsManager : BoxResourceManager
    {
        public BoxCommentsManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth)
            : base(config, service, converter, auth) { }

        /// <summary>
        /// Used to add a comment by the user to a specific file, discussion, or comment (i.e. as a reply comment).
        /// </summary>
        /// <param name="commentRequest"></param>
        /// <returns></returns>
        public async Task<BoxComment> AddCommentAsync(BoxCommentRequest commentRequest, List<string> fields = null)
        {
            commentRequest.ThrowIfNull("commentRequest")
                .Item.ThrowIfNull("commentRequest.Item")
                .Id.ThrowIfNullOrWhiteSpace("commentRequest.Item.Id");
            if (commentRequest.Item.Type == null)
                throw new ArgumentNullException("commentRequest.Item.Type");

            BoxRequest request = new BoxRequest(_config.CommentsEndpointUri)
                .Method(RequestMethod.Post)
                .Param(ParamFields, fields)
                .Payload(_converter.Serialize(commentRequest));

            IBoxResponse<BoxComment> response = await ToResponseAsync<BoxComment>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// A full comment object is returned is the ID is valid and if the user has access to the comment.
        /// </summary>
        /// <param name="commentRequest"></param>
        /// <returns></returns>
        public async Task<BoxComment> GetInformationAsync(string id, List<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.CommentsEndpointUri, id)
                .Param(ParamFields, fields);

            IBoxResponse<BoxComment> response = await ToResponseAsync<BoxComment>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to update the message of the comment.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BoxComment> UpdateAsync(string id, BoxCommentRequest commentsRequest, List<string> fields = null)
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
        /// <param name="id"></param>
        /// <param name="commentsRequest"></param>
        /// <returns></returns>
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
