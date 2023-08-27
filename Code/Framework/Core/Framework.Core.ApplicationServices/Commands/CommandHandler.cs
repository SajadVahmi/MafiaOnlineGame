using Framework.Core.ApplicationServices.Services;

namespace Framework.Core.ApplicationServices.Commands;

public abstract class AsyncCommandHandler<TCommand, TData> : ICommandHandler<TCommand, TData>
    where TCommand : ICommand<TData>
{

    protected readonly CommandResult<TData> Result = new();
    protected AsyncCommandHandler() { }

    public abstract Task<CommandResult<TData>> HandleAsync(TCommand request, CancellationToken cancellationToken = default);

    protected virtual CommandResult<TData> Ok(TData data)
    {
        Result.Data = data;
        Result.Status = ApplicationServiceStatus.Ok;
        return Result;
    }

    protected virtual CommandResult<TData> CommandResult(TData data, ApplicationServiceStatus status)
    {
        Result.Data = data;
        Result.Status = status;
        return Result;
    }

    protected void AddMessage(string message)
    {
        Result.AddMessage(message);

        //TODO:add multi language
        //result.AddMessage(_zaminServices.Translator[message]);
    }
    //protected void AddMessage(string message, params string[] arguments)
    //{
    //    result.AddMessage(_zaminServices.Translator[message, arguments]);
    //}


}


public abstract class AsyncCommandHandler<TCommand> : ICommandHandler<TCommand>
where TCommand : ICommand
{

    protected readonly CommandResult Result = new();
    protected AsyncCommandHandler() { }
    public abstract Task<CommandResult> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
    protected virtual CommandResult Ok()
    {
        Result.Status = ApplicationServiceStatus.Ok;
        return Result;
    }
    protected virtual CommandResult CommandResult(ApplicationServiceStatus status)
    {
        Result.Status = status;
        return Result;
    }
    protected void AddMessage(string message)
    {
        Result.AddMessage(message);
        //TODO:add multi language
        //Result.AddMessage(_zaminServices.Translator[message]);
    }
    //protected void AddMessage(string message, params string[] arguments)
    //{
    //    Result.AddMessage(_zaminServices.Translator[message, arguments]);
    //}
}

