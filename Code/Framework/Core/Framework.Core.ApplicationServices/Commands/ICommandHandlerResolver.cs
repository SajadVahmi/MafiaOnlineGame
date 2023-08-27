namespace Framework.Core.ApplicationServices.Commands
{
    public interface ICommandHandlerResolver
    {
        public ICommandHandler<TCommand> ResolveHandlers<TCommand>(TCommand command) where TCommand : ICommand;

    }
}
