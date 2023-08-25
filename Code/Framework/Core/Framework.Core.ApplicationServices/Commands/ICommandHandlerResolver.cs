namespace Framework.Core.ApplicationServices.Commands
{
    public interface ICommandHandlerResolver
    {
        ICommandHandler<TCommand> ResolveHandlers<TCommand>(TCommand command) where TCommand : ICommand;
        ICommandHandler<TCommand, TData> ResolveHandlers<TCommand, TData>(TCommand command) where TCommand : ICommand<TData>;
        IAsyncCommandHandler<TCommand> ResolveAsyncHandlers<TCommand>(TCommand command) where TCommand : IAsyncCommand;
        IAsyncCommandHandler<TCommand, TData> ResolveAsyncHandlers<TCommand, TData>(TCommand command) where TCommand : IAsyncCommand<TData>;
    }
}
