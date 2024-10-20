using System.Data;
using Dapper;
using Framework.Projection;
using Framework.Projection.SqlServer;
using Games.Domain.PlayerAggregate.DomainEvents;

namespace Backgrounds.SqlServerProjection.Handlers;

public class PlayerRegisteredHandler(ICursor cursor, SqlServerCursorOptions sqlServerCursorOptions, IDbConnection connection) : SqlServerCursorAwareHandler<PlayerRegistered>(cursor, sqlServerCursorOptions, connection)
{
    public override async Task HandleEventAsync(PlayerRegistered @event, CancellationToken cancellationToken = default)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", @event.Id);
        parameters.Add("FirstName", @event.FirstName);
        parameters.Add("LastName", @event.LastName);
        parameters.Add("Gender", @event.Gender);
        parameters.Add("UserId", @event.UserId);

        string sqlStatement =
            "INSERT INTO [dbo].[Player] ([Id],[FirstName],[LastName],[Gender],[UserId]) VALUES(@Id,@FirstName,@LastName,@Gender,@UserId)";

        await Connection.ExecuteAsync(sqlStatement, parameters);
    }
}