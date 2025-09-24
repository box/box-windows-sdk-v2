
namespace Box.V2
{
    /// <summary>
    /// Interface that defines a Box part
    /// </summary>
    public interface IBoxPart
    { }

    /// <summary>
    /// Interface that takes different types of box part
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBoxPart<T> : IBoxPart
    {
        /// <summary>
        /// The value of the part
        /// </summary>
        T Value { get; }
    }
}
