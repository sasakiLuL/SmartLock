using SmartLock.Domain.Entities;

namespace SmartLock.Application.Interfaces;

public interface IReadModelService
{
    IQueryable<TModel> Query<TModel>() where TModel : Entity;
}
