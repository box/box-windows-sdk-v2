
namespace Box.V2
{
    /// <summary>
    /// Interface that defines a Box form part
    /// </summary>
    public interface IBoxFormPart
    {
        /// <summary>
        /// The name of the form part
        /// </summary>
        string Name { get; }
    }

    /// <summary>
    /// Interface that takes different types of Form parts
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBoxFormPart<T> : IBoxFormPart
    {
        /// <summary>
        /// The value of the form part
        /// </summary>
        T Value { get; }
    }
}
