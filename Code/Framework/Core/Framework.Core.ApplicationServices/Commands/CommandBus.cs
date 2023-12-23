namespace Framework.Core.ApplicationServices.Commands;

public class CommandBus : ICommandBus
{
    private readonly ICommandHandlerResolver _resolver;

    public CommandBus(ICommandHandlerResolver resolver)
    {
        _resolver = resolver;
    }

    public async Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, ICommand
    {
        var handler = _resolver.ResolveHandlers(command);

        await handler.HandleAsync(command, cancellationToken);
    }

}