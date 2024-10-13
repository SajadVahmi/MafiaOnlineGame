using Dapper;
using System.Data;

namespace Backgrounds.Projection.Sql._Shared;

public static class SqlServerCursorQueryExtension
{
    public static void CreateCursorTableIfNotExist(this IDbConnection connection, SqlServerCursorOptions options)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", options.CursorId);
        parameters.Add("Position", 0);

        string creationScript = $"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{options.CursorTableName}') BEGIN  CREATE TABLE [dbo].[{options.CursorTableName}]([{options.CursorIdFiledName}] [varchar](100) NOT NULL, [{options.CursorPositionFiledName}] [bigint] NOT NULL,  CONSTRAINT [PK_Cursor] PRIMARY KEY CLUSTERED  ([{options.CursorIdFiledName}] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]) ON [PRIMARY] ALTER TABLE [dbo].[{options.CursorTableName}] ADD  CONSTRAINT [DF_{options.CursorTableName}_{options.CursorPositionFiledName}]  DEFAULT ((0)) FOR {options.CursorPositionFiledName} END";

        connection.Execute(creationScript, parameters);

        var existRowCount = connection.ExecuteScalar<long>($"SELECT COUNT([{options.CursorIdFiledName}]) FROM [dbo].[{options.CursorTableName}] WHERE [{options.CursorIdFiledName}] = @Id",parameters);


        if (existRowCount == 0)
            connection.Execute($"INSERT INTO [dbo].[{options.CursorTableName}] ([{options.CursorIdFiledName}] ,[{options.CursorPositionFiledName}]) VALUES (@Id,@Position)",parameters);

    }

    public static long GetCursorPosition(this IDbConnection connection, SqlServerCursorOptions options)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", options.CursorId);

       return connection.ExecuteScalar<long>(
            $"SELECT [{options.CursorPositionFiledName}] FROM [dbo].[{options.CursorTableName}] WHERE [{options.CursorIdFiledName}]=@Id",parameters);
    }

    public static Task MoveCursorPositionAsync(this IDbConnection connection, SqlServerCursorOptions options,long position,CancellationToken cancellationToken=default)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", options.CursorId);
        parameters.Add("Position", position);

       return connection.ExecuteAsync($"UPDATE [dbo].[{options.CursorTableName}] SET [{options.CursorPositionFiledName}]=@Position WHERE [{options.CursorIdFiledName}]=@Id", parameters);

    }
}