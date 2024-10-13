using Framework.Core.Domain.Events;
using System.Data;
using System.Transactions;

namespace Backgrounds.Projection.Sql._Shared;

public abstract class SqlServerCursorAwareHandler<T>(ICursor cursor,SqlServerCursorOptions sqlServerCursorOptions,IDbConnection connection) : IEventHandler<T>
    where T : IEvent
{
    protected IDbConnection Connection { get; set; } = connection ?? throw new ArgumentNullException();
    public async Task HandleAsync(T @event,CancellationToken cancellationToken=default)
    {
        connection.Open();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
  
        try
        {
            await HandleEventAsync(@event, cancellationToken);

            var position = cursor.CurrentPosition().CommitPosition;

            await connection.MoveCursorPositionAsync(sqlServerCursorOptions, (long)position, cancellationToken);

            scope.Complete();
        }
        catch (Exception e)
        {
            throw;
        }
        finally
        {

            connection.Close();
        }

        

    }

    public abstract Task HandleEventAsync(T @event, CancellationToken cancellationToken = default);
}