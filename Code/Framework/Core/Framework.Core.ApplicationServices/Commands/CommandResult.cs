using Framework.Core.ApplicationServices.Services;

namespace Framework.Core.ApplicationServices.Commands;

public class CommandResult : ApplicationServiceResult
{

}

public class CommandResult<TData> : CommandResult
{
    public TData? Data { get; set; }
}

