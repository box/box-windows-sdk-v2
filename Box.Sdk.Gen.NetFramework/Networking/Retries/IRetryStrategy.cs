using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen {
    public interface IRetryStrategy {
        System.Threading.Tasks.Task<bool> ShouldRetryAsync(FetchOptions fetchOptions, FetchResponse fetchResponse, int attemptNumber);

        double RetryAfter(FetchOptions fetchOptions, FetchResponse fetchResponse, int attemptNumber);

    }
}