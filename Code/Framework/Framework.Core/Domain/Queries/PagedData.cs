namespace Framework.Core.Domain.Queries;

public class PagedData<T>
{
    public required List<T> QueryResult { get; set; }

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public int TotalCount { get; set; }

}
