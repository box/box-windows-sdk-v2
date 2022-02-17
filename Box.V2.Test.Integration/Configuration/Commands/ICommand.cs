using System.Threading.Tasks;

namespace Box.V2.Test.Integration.Configuration.Commands
{
    /// <summary>
    /// Base interface for commands. Implement IDisposableCommand or ICleanupCommand instead.
    /// </summary>
    public interface ICommand
    {
        CommandScope Scope { get; }
        CommandAccessLevel AccessLevel { get; }
    }

    /// <summary>
    /// Interface used to create and remove/dispose of resources. Use for test setup.
    /// </summary>
    public interface IDisposableCommand : ICommand
    {
        /// <summary>
        /// Creates resources required for the test. Returns the identifier of the resource.
        /// </summary>
        /// <param name="client">Box Client</param>
        /// <returns>Resource Id</returns>
        Task<string> Execute(IBoxClient client);

        /// <summary>
        /// Executed during tear down of the test. Clean resources created during Execute.
        /// </summary>
        /// <param name="client">Box Client</param>
        /// <returns></returns>
        Task Dispose(IBoxClient client);
    }

    /// <summary>
    /// Interface used to remove/dispose of resources. Use in test teardown if IDisposableCommand was not used for resource creation.
    /// </summary>
    public interface ICleanupCommand : ICommand
    {
        /// <summary>
        /// Clean resources created in the test.
        /// </summary>
        /// <param name="client">Box Client</param>
        /// <returns></returns>
        Task Execute(IBoxClient client);
    }
}
