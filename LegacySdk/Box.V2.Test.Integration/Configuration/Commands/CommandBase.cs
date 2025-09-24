namespace Box.V2.Test.Integration.Configuration.Commands
{
    /// <summary>
    /// Base class for ICommand intefaces.
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        public CommandScope Scope { get; }
        public CommandAccessLevel AccessLevel { get; }

        public CommandBase(CommandScope scope = CommandScope.Test, CommandAccessLevel accessLevel = CommandAccessLevel.User)
        {
            Scope = scope;
            AccessLevel = accessLevel;
        }
    }
}
