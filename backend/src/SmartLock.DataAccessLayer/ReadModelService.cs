using Microsoft.EntityFrameworkCore;
using SmartLock.Application.Interfaces;
using SmartLock.Domain.Entities;

namespace SmartLock.DataAccessLayer;

public class ReadModelService(SmartLockContext context) : IReadModelService
{
    public IQueryable<TModel> Query<TModel>() where TModel : Entity
    {
        return context.Set<TModel>()
            .AsQueryable()
            .AsNoTracking();
    }
}
