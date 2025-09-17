using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen {
    public interface INetworkClient {
        public System.Threading.Tasks.Task<FetchResponse> FetchAsync(FetchOptions options) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}