namespace Framework.Core.ApplicationServices.Commands;

public class CommandBus(ICommandHandlerResolver resolver) : ICommandBus
{
    public async Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, ICommand
    {
        var handler = resolver.ResolveHandlers(command);

        await handler.HandleAsync(command, cancellationToken);
    }

}