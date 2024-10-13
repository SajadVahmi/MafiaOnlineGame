namespace Backgrounds.Projection.Sql._Shared;

public class SqlServerCursorOptions
{
    public string? ConnectionString { get; set; }
    public string? CursorId { get; set; }
    public string? CursorTableName { get; set; }
    public string? CursorIdFiledName { get; set; }
    public string? CursorPositionFiledName { get; set; }

}