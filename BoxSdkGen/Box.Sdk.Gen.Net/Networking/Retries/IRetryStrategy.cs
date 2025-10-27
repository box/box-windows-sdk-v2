using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen {
    public interface IRetryStrategy {
        public System.Threading.Tasks.Task<bool> ShouldRetryAsync(FetchOptions fetchOptions, FetchResponse fetchResponse, int attemptNumber) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        public double RetryAfter(FetchOptions fetchOptions, FetchResponse fetchResponse, int attemptNumber) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}