namespace Framework.Core.Application.Queries;

public interface IPageQuery<TResult> : IQuery<TResult>
{
    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int SkipCount => (PageNumber - 1) * PageSize;

    public bool NeedTotalCount { get; set; }

    public string SortBy { get; set; }

    public bool SortDescending { get; set; }
}


