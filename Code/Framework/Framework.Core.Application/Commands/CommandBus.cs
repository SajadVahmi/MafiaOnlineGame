namespace Framework.Core.Application.Commands;

public class CommandBus(ICommandHandlerResolver resolver) : ICommandBus
{
    public Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, ICommand
    {
        var handler = resolver.ResolveHandlers(command);

        return handler.HandleAsync(command, cancellationToken);
    }

    Task<TResult> ICommandBus.SendAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken)
    {
        var handler = resolver.ResolveHandlers<TCommand,TResult>(command);

        return handler.HandleAsync(command, cancellationToken);
    }
}