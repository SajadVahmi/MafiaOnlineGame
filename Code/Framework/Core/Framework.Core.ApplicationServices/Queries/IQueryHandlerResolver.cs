namespace Framework.Core.ApplicationServices.Queries;

public interface IQueryHandlerResolver
{
    IQueryHandler<TQuery, TResponse> ResolveHandlers<TQuery, TResponse>(TQuery request) where TQuery : class, IQuery<TResponse>;
}