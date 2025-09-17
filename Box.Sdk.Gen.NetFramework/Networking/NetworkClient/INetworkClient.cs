using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen {
    public interface INetworkClient {
        System.Threading.Tasks.Task<FetchResponse> FetchAsync(FetchOptions options);

    }
}