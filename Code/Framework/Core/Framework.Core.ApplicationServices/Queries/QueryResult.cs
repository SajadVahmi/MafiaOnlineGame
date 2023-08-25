
using Framework.Core.ApplicationServices.Services;

namespace Framework.Core.ApplicationServices.Queries;


public sealed class QueryResult<TData> : ApplicationServiceResult
{
    public TData? Data { get; set; }
}

