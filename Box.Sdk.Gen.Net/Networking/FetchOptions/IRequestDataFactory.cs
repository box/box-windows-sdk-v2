using Box.Sdk.Gen;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen {
    public interface IRequestDataFactory {
        /// <summary>
    /// Creates a new request body
    /// </summary>
    public SerializedData Create() => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}