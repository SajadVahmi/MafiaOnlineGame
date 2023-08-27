

namespace Framework.Core.ApplicationServices.Commands;

public class CommandBus : ICommandBus
{
    private readonly ICommandHandlerResolver _resolver;

    public CommandBus(ICommandHandlerResolver resolver)
    {
        _resolver = resolver;
    }

    public async Task<CommandResult> SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, ICommand
    {
        var handler = _resolver.ResolveHandlers(command);

        return await handler.HandleAsync(command, cancellationToken);
    }


    public async Task<CommandResult<TData>> SendAsync<TCommand, TData>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, ICommand<TData>
    {
        var handler = _resolver.ResolveHandlers<TCommand, TData>(command);

        return await handler.HandleAsync(command,cancellationToken);

    }

    
}