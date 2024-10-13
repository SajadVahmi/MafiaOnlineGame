using Backgrounds.Projection.Sql._Shared;
using Dapper;
using Games.Domain.PlayerAggregate.DomainEvents;
using System.Data;

namespace Backgrounds.Projection.Sql.Handlers;

public class PlayerGenderChangedHandler(ICursor cursor, SqlServerCursorOptions sqlServerCursorOptions, IDbConnection connection) : SqlServerCursorAwareHandler<PlayerGenderChanged>(cursor, sqlServerCursorOptions, connection)
{

    public override async Task HandleEventAsync(PlayerGenderChanged @event, CancellationToken cancellationToken = default)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", @event.Id);
        parameters.Add("Gender", @event.Gender);

        string sqlStatement = "UPDATE [dbo].[Player] SET [Gender]=@Gender WHERE Id=@Id";

        await Connection.ExecuteAsync(sqlStatement, parameters);
    }
}