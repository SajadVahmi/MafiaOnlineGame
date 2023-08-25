namespace Framework.Core.ApplicationServices.Commands;

public interface ICommandBus
{
    Task<CommandResult> SendAsync<TCommand>(TCommand command,CancellationToken cancellationToken=default) where TCommand : class, IAsyncCommand;
    Task<CommandResult<TData>> SendAsync<TCommand, TData>(TCommand command,CancellationToken cancellationToken=default) where TCommand : class, IAsyncCommand<TData>;

    CommandResult Send<TCommand>(TCommand command) where TCommand : class, ICommand;
    CommandResult<TData> Send<TCommand, TData>(TCommand command) where TCommand : class, ICommand<TData>;
}
     