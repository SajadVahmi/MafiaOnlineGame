

namespace Framework.Core.ApplicationServices.Commands;

public class CommandBus : ICommandBus
{
    private readonly ICommandHandlerResolver _resolver;

    public CommandBus(ICommandHandlerResolver resolver)
    {
        _resolver = resolver;
    }

    public async Task<CommandResult> SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, IAsyncCommand
    {
        var handler = _resolver.ResolveAsyncHandlers(command);

        return await handler.HandleAsync(command, cancellationToken);
    }


    public async Task<CommandResult<TData>> SendAsync<TCommand, TData>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, IAsyncCommand<TData>
    {
        var handler = _resolver.ResolveAsyncHandlers<TCommand, TData>(command);

        return await handler.HandleAsync(command,cancellationToken);

    }

    public CommandResult Send<TCommand>(TCommand command) where TCommand : class, ICommand
    {
        var handler = _resolver.ResolveHandlers(command);

        return handler.Handle(command);
    }

   public CommandResult<TData> Send<TCommand, TData>(TCommand command) where TCommand : class, ICommand<TData>
    {
        var handler = _resolver.ResolveHandlers<TCommand, TData>(command);

        return handler.Handle(command);
    }
}