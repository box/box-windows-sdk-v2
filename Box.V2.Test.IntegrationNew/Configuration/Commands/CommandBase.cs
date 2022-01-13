namespace Box.V2.Test.IntegrationNew.Configuration.Commands
{
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
