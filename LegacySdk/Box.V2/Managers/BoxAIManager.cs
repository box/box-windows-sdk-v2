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
    /// The manager that represents all of the AI endpoints.
    /// </summary>
    public class BoxAIManager : BoxResourceManager, IBoxAIManager
    {
        public BoxAIManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
            : base(config, service, converter, auth, asUser, suppressNotifications) { }

        /// <summary>
        /// Sends an AI request to supported LLMs and returns an answer specifically focused on the user's question given the provided context.
        /// </summary>
        /// <param name="aiAskRequest">AI ask request</param>
        /// <returns>Response for AI question</returns>
        public async Task<BoxAIResponse> SendAIQuestionAsync(BoxAIAskRequest aiAskRequest)
        {
            var request = new BoxRequest(_config.AIEndpointWithPathUri, Constants.AIAskString)
                .Method(RequestMethod.Post)
                .Payload(_converter.Serialize(aiAskRequest));

            IBoxResponse<BoxAIResponse> response = await ToResponseAsync<BoxAIResponse>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Sends an AI request to supported LLMs and returns an answer specifically focused on the creation of new text.
        /// </summary>
        /// <param name="aiTextGenRequest">AI text gen request</param>
        /// <returns>Response for AI text gen request</returns>
        public async Task<BoxAIResponse> SendAITextGenRequestAsync(BoxAITextGenRequest aiTextGenRequest)
        {
            var request = new BoxRequest(_config.AIEndpointWithPathUri, Constants.AITextGenString)
                .Method(RequestMethod.Post)
                .Payload(_converter.Serialize(aiTextGenRequest));

            IBoxResponse<BoxAIResponse> response = await ToResponseAsync<BoxAIResponse>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }
    }
}
