namespace SmartLock.Application.Pagination;

public class PaginationService
{
    public IQueryable<TItem> GetPagedQuery<TItem>(
        IQueryable<TItem> query,
        int pageNumber,
        int pageSize)
    {
        var pagedQuery = query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        return pagedQuery;
    }
}
