namespace Framework.Core.ApplicationServices.Commands;

public interface ICommandBus
{
    Task<CommandResult> SendAsync<TCommand>(TCommand command,CancellationToken cancellationToken=default) where TCommand : class, ICommand;
    Task<CommandResult<TData>> SendAsync<TCommand, TData>(TCommand command,CancellationToken cancellationToken=default) where TCommand : class, ICommand<TData>;


}
     