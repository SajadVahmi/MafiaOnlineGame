namespace Framework.Core.Application.Queries;

public abstract class QueryHandler<TQuery, TResponse> : IQueryHandler<TQuery, TResponse>
    where TQuery : class, IQuery<TResponse>
{

    public abstract Task<TResponse> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
}