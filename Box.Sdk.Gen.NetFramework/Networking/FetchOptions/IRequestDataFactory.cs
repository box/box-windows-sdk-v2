using Box.Sdk.Gen;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen {
    public interface IRequestDataFactory {
        /// <summary>
    /// Creates a new request body
    /// </summary>
    SerializedData Create();

    }
}