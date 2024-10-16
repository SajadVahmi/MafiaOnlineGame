namespace Framework.Core.Application.Queries;

public interface IQueryHandler<in TQuery, TResult>
    where TQuery : class, IQuery<TResult>
{
    Task<TResult> HandleAsync(TQuery request, CancellationToken cancellationToken = default);
}