using System.Threading.Tasks;

namespace Box.V2.Test.IntegrationNew.Configuration.Commands
{
    public interface ICommand
    {
        CommandScope Scope { get; }
        CommandAccessLevel AccessLevel { get; }
    }

    public interface ICleanupCommand : ICommand
    {
        Task Execute(IBoxClient client);
    }

    public interface IDisposableCommand : ICommand
    {
        Task<string> Execute(IBoxClient client);
        Task Dispose(IBoxClient client);
    }
}
