namespace SmartLock.Application.Pagination;

public record PageResponse<TItem>
{
    public int PageSize { get; init; }

    public int PageNumber { get; init; }

    public int PageCount { get; init; }

    public int TotalItemCount { get; init; }

    public List<TItem> PageItems { get; init; }

    private PageResponse(
        int pageSize,
        int pageNumber,
        int pageCount,
        int totalItemCount,
        List<TItem> pageItems) 
    {
        PageCount = pageCount;
        PageSize = pageSize;
        PageNumber = pageNumber;
        TotalItemCount = totalItemCount;
        PageItems = pageItems;
    }

    public static PageResponse<TItem> Create(
        List<TItem> items,
        int totalItemCount,
        int pageNumber, 
        int pageSize)
    {
        int count = totalItemCount;

        return new PageResponse<TItem>(
            pageSize,
            pageNumber,
            count % pageSize == 0 ? count / pageSize : count / pageSize + 1,
            count,
            items);
    }
}
