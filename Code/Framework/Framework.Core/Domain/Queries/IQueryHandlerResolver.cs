namespace Framework.Core.Domain.Queries;

public interface IQueryHandlerResolver
{
    IQueryHandler<TQuery, TResult> ResolveHandlers<TQuery, TResult>(TQuery request) where TQuery : class, IQuery<TResult>;
}