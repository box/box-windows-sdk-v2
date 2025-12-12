using Box.Sdk.Gen;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen {
    public class BoxRetryStrategy : IRetryStrategy {
        public int MaxAttempts { get; set; }

        public double RetryRandomizationFactor { get; set; }

        public double RetryBaseInterval { get; set; }

        public int MaxRetriesOnException { get; set; }

        public BoxRetryStrategy(int maxAttempts = 5, double retryRandomizationFactor = 0.5, double retryBaseInterval = 1, int maxRetriesOnException = 2) {
            MaxAttempts = maxAttempts;
            RetryRandomizationFactor = retryRandomizationFactor;
            RetryBaseInterval = retryBaseInterval;
            MaxRetriesOnException = maxRetriesOnException;
        }
        public async System.Threading.Tasks.Task<bool> ShouldRetryAsync(FetchOptions fetchOptions, FetchResponse fetchResponse, int attemptNumber) {
            if (fetchResponse.Status == 0) {
                return attemptNumber <= this.MaxRetriesOnException;
            }
            bool isSuccessful = fetchResponse.Status >= 200 && fetchResponse.Status < 400;
            string retryAfterHeader = fetchResponse.Headers.ContainsKey("Retry-After") ? fetchResponse.Headers["Retry-After"] : null;
            bool isAcceptedWithRetryAfter = fetchResponse.Status == 202 && retryAfterHeader != null;
            if (attemptNumber >= this.MaxAttempts) {
                return false;
            }
            if (isAcceptedWithRetryAfter) {
                return true;
            }
            if (fetchResponse.Status >= 500) {
                return true;
            }
            if (fetchResponse.Status == 429) {
                return true;
            }
            if (fetchResponse.Status == 401 && fetchOptions.Auth != null) {
                await fetchOptions.Auth.RefreshTokenAsync(networkSession: fetchOptions.NetworkSession).ConfigureAwait(false);
                return true;
            }
            if (isSuccessful) {
                return false;
            }
            return false;
        }

        public double RetryAfter(FetchOptions fetchOptions, FetchResponse fetchResponse, int attemptNumber) {
            string retryAfterHeader = fetchResponse.Headers.ContainsKey("Retry-After") ? fetchResponse.Headers["Retry-After"] : null;
            if (retryAfterHeader != null) {
                return double.Parse(NullableUtils.Unwrap(retryAfterHeader));
            }
            double randomization = Utils.Random(min: 1 - this.RetryRandomizationFactor, max: 1 + this.RetryRandomizationFactor);
            double exponential = System.Math.Pow(2, attemptNumber);
            return exponential * this.RetryBaseInterval * randomization;
        }

    }
}