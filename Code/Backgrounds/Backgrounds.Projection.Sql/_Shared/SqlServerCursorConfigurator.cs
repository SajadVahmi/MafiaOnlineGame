using Microsoft.Data.SqlClient;
using System.Data;

namespace Backgrounds.Projection.Sql._Shared;

public static class SqlServerCursorConfigurator
{
    public static IServiceCollection ConfigureSqlServerCursor(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var options = configuration.GetSection("Cursor").Get<SqlServerCursorOptions>();

        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(options.ConnectionString);
        ArgumentNullException.ThrowIfNull(options.CursorTableName);
        ArgumentNullException.ThrowIfNull(options.CursorIdFiledName);
        ArgumentNullException.ThrowIfNull(options.CursorPositionFiledName);
        ArgumentNullException.ThrowIfNull(options.CursorId);

        serviceCollection.AddSingleton(options);

        var connection = new SqlConnection(options.ConnectionString);

        connection.Open();

        connection.CreateCursorTableIfNotExist(options);

        var position = connection.GetCursorPosition(options);

        serviceCollection.AddSingleton<ICursor>(_ => new Cursor((ulong)position));

        connection.Close();

        serviceCollection.AddTransient<IDbConnection>(_ => new SqlConnection(options.ConnectionString));

        return serviceCollection;
    }
}