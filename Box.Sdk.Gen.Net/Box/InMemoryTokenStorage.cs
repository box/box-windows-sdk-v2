using Box.Sdk.Gen.Schemas;
using System.Threading.Tasks;

namespace Box.Sdk.Gen
{
    /// <summary>
    /// Class used for storing Access Token in memory.
    /// </summary>
    public class InMemoryTokenStorage : ITokenStorage
    {
        private AccessToken? _token;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryTokenStorage"/> class.
        /// </summary>
        public InMemoryTokenStorage(AccessToken? token = null)
        {
            this._token = token;
        }

        /// <summary>
        /// Stores access token.
        /// </summary>
        public System.Threading.Tasks.Task StoreAsync(AccessToken token)
        {
            _token = token;
            return System.Threading.Tasks.Task.FromResult(_token);
        }

        /// <summary>
        /// Gets stored the token.
        /// </summary>
        /// <returns>An access token.</returns>
        public Task<AccessToken?> GetAsync()
        {
            return System.Threading.Tasks.Task.FromResult(_token);
        }

        /// <summary>
        /// Clears the stored token.
        /// </summary>
        public System.Threading.Tasks.Task ClearAsync()
        {
            _token = null;
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
