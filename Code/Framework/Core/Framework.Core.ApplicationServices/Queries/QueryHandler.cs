namespace Framework.Core.ApplicationServices.Queries;

public abstract class QueryHandler<TQuery, TResponse> : IQueryHandler<TQuery, TResponse>
    where TQuery : class, IQuery<TResponse>
{

    public abstract Task<QueryResult<TResponse>> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
}