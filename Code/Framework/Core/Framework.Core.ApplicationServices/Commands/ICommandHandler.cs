namespace Framework.Core.ApplicationServices.Commands;

public interface IAsyncCommandHandler<in TCommand, TData> where TCommand : IAsyncCommand<TData>
{
    Task<CommandResult<TData>> HandleAsync(TCommand request,CancellationToken cancellationToken=default);
}
public interface IAsyncCommandHandler<in TCommand> where TCommand : IAsyncCommand
{
    Task<CommandResult> HandleAsync(TCommand request,CancellationToken cancellationToken= default);
}

public interface ICommandHandler<in TCommand, TData> where TCommand : ICommand<TData>
{
    CommandResult<TData> Handle(TCommand request);
}
public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    CommandResult Handle(TCommand request);
}

