namespace Framework.Core.Application.Commands
{
    public interface ICommandHandlerResolver
    {
        public ICommandHandler<TCommand> ResolveHandlers<TCommand>(TCommand command) where TCommand : ICommand;
        public ICommandHandler<TCommand, TResult> ResolveHandlers<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>;

    }
}
