namespace Framework.Core.Application.Commands;

public interface ICommandBus
{
    Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, ICommand;
    Task<TResult> SendAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, ICommand<TResult>;

}
