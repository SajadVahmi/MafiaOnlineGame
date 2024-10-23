using System.Data;
using Dapper;
using Framework.Projection;
using Framework.Projection.SqlServer;
using Games.Contract.PlayerAggregate.DomainEvents;

namespace Backgrounds.SqlServerProjection.Handlers;

public class PlayerRenamedHandler(ICursor cursor, SqlServerCursorOptions sqlServerCursorOptions, IDbConnection connection) : SqlServerCursorAwareHandler<PlayerRenamed>(cursor, sqlServerCursorOptions, connection)
{
    public override async Task HandleEventAsync(PlayerRenamed @event, CancellationToken cancellationToken = default)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", @event.Id);
        parameters.Add("FirstName", @event.FirstName);
        parameters.Add("LastName", @event.LastName);

        string sqlStatement = "UPDATE [dbo].[Player] SET [FirstName]=@FirstName , [LastName]=@LastName  WHERE Id=@Id";

        await Connection.ExecuteAsync(sqlStatement, parameters);
    }
}