namespace Framework.Core.Application.Queries
{
    public interface IQueryBus
    {
        Task<TResult> ExecuteAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default) where TQuery : class, IQuery<TResult>;
    }


}
