﻿namespace Framework.Core.Application.Queries;

public class PageQuery<TData> : IPageQuery<TData>
{

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public int SkipCount => (PageNumber - 1) * PageSize;

    public bool NeedTotalCount { get; set; }

    public string SortBy { get; set; } = "EntityId";

    public bool SortDescending { get; set; }
}



