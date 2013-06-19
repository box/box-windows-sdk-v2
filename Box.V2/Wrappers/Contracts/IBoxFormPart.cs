
namespace Box.V2
{
    public interface IBoxFormPart
    {
        string Name { get; }
    }

    public interface IBoxFormPart<T> : IBoxFormPart
    {
        T Value { get; }
    }
}
