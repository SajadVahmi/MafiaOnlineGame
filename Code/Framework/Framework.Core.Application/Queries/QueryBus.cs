namespace Framework.Core.Application.Queries;

public class QueryBus(IQueryHandlerResolver handlerResolver) : IQueryBus
{
    public Task<TResult> ExecuteAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken) where TQuery : class, IQuery<TResult>
    {

        var handler = handlerResolver.ResolveHandlers<TQuery, TResult>(query);

        return handler.HandleAsync(query, cancellationToken);
    }

}