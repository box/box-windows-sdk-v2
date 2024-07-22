using System.Threading.Tasks;
using Box.V2.Models;

namespace Box.V2.Managers
{
    /// <summary>
    /// The manager that represents all of the AI endpoints.
    /// </summary>
    public interface IBoxAIManager
    {
        /// <summary>
        /// Sends an AI request to supported LLMs and returns an answer specifically focused on the user's question given the provided context.
        /// </summary>
        /// <param name="aiAskRequest">AI ask request</param>
        /// <returns>Response for AI question</returns>
        Task<BoxAIResponse> SendAIQuestionAsync(BoxAIAskRequest aiAskRequest);

        /// <summary>
        /// Sends an AI request to supported LLMs and returns an answer specifically focused on the creation of new text.
        /// </summary>
        /// <param name="aiTextGenRequest">AI text gen request/param>
        /// <returns>Response for AI text gen request</returns>
        Task<BoxAIResponse> SendAITextGenRequestAsync(BoxAITextGenRequest aiTextGenRequest);
    }
}
