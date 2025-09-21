using Box.Sdk.Gen.Schemas;
using System.Threading.Tasks;

namespace Box.Sdk.Gen
{
    /// <summary>
    /// Interface used for storing Access Token.
    /// </summary>
    public interface ITokenStorage
    {
        /// <summary>
        /// Stores access token.
        /// </summary>
        System.Threading.Tasks.Task StoreAsync(AccessToken token);

        /// <summary>
        /// Gets stored the token.
        /// </summary>
        /// <returns>An access token.</returns>
        Task<AccessToken?> GetAsync();

        /// <summary>
        /// Clears the stored token.
        /// </summary>
        System.Threading.Tasks.Task ClearAsync();
    }
}
