namespace Framework.Core.Application.Queries;

public interface IQueryHandler<in TQuery, TResponse>
    where TQuery : class, IQuery<TResponse>
{
    Task<QueryResult<TResponse>> HandleAsync(TQuery request, CancellationToken cancellationToken = default);
}