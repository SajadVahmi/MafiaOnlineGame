namespace Framework.Core.Application.Queries;

public class QueryBus(IQueryHandlerResolver handlerResolver) : IQueryBus
{
    public Task<QueryResult<TResponse>> ExecuteAsync<TQuery, TResponse>(TQuery query, CancellationToken cancellationToken) where TQuery : class, IQuery<TResponse>
    {

        var handler = handlerResolver.ResolveHandlers<TQuery, TResponse>(query);

        return handler.HandleAsync(query, cancellationToken);
    }

}