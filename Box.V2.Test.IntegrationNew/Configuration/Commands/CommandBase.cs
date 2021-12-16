namespace Box.V2.Test.IntegrationNew.Configuration.Commands
{
    public abstract class CommandBase : ICommand
    {
        public CommandScope Scope { get; }

        public CommandBase(CommandScope scope = CommandScope.Test)
        {
            Scope = scope;
        }
    }
}
