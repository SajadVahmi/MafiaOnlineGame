namespace Framework.Core.ApplicationServices.Commands;

public interface ICommandHandler<in TCommand, TResult> where TCommand : ICommand<TResult>
{
    Task<CommandResult<TResult>> HandleAsync(TCommand request,CancellationToken cancellationToken=default);
}
public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    Task<CommandResult> HandleAsync(TCommand request,CancellationToken cancellationToken= default);
}



