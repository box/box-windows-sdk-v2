using Box.V2.Auth;
using Box.V2.Contracts;
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
        public async Task<BoxComment> AddCommentAsync(BoxCommentRequest commentRequest)
        {
            CheckPrerequisite(commentRequest.ThrowIfNull("commentRequest")
                .Item.ThrowIfNull("commentRequest.Item").Id);
            if (commentRequest.Item.Type == null)
                throw new ArgumentNullException("commentRequest.Item.Type");

            BoxRequest request = new BoxRequest(_config.CommentsEndpointUri)
                .Method(RequestMethod.POST)
                .Authorize(_auth.Session.AccessToken)
                .Payload(_converter.Serialize(commentRequest));

            IBoxResponse<BoxComment> response = await ToResponseAsync<BoxComment>(request);

            return response.ResponseObject;
        }

        /// <summary>
        /// A full comment object is returned is the ID is valid and if the user has access to the comment.
        /// </summary>
        /// <param name="commentRequest"></param>
        /// <returns></returns>
        public async Task<BoxComment> GetInformationAsync(string id)
        {
            CheckPrerequisite(id);

            BoxRequest request = new BoxRequest(_config.CommentsEndpointUri, id)
                .Authorize(_auth.Session.AccessToken);

            IBoxResponse<BoxComment> response = await ToResponseAsync<BoxComment>(request);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to update the message of the comment.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BoxComment> UpdateAsync(string id, BoxCommentRequest commentsRequest)
        {
            CheckPrerequisite(id, 
                commentsRequest.ThrowIfNull("commentsRequest").Message);

            BoxRequest request = new BoxRequest(_config.CommentsEndpointUri, id)
                .Method(RequestMethod.PUT)
                .Authorize(_auth.Session.AccessToken)
                .Payload(_converter.Serialize(commentsRequest));

            IBoxResponse<BoxComment> response = await ToResponseAsync<BoxComment>(request);

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
            CheckPrerequisite(id);

            BoxRequest request = new BoxRequest(_config.CommentsEndpointUri, id)
                .Method(RequestMethod.DELETE)
                .Authorize(_auth.Session.AccessToken);

            IBoxResponse<BoxComment> response = await ToResponseAsync<BoxComment>(request);
            return response.Status == ResponseStatus.Success;
        }
    }
}
