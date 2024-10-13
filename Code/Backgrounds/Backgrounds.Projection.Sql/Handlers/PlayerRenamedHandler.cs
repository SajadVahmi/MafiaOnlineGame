using System.Data;
using Backgrounds.Projection.Sql._Shared;
using Dapper;
using Games.Domain.PlayerAggregate.DomainEvents;

namespace Backgrounds.Projection.Sql.Handlers;

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