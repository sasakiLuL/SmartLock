using SmartLock.Domain.Core;
using System;
using System.Reflection;
using System.Threading;

namespace SmartLock.DataAccessLayer;

public abstract class Repository<TEntity, TModel> 
    : IRepository<TEntity, TModel> where TEntity 
    : Entity<TModel> where TModel : Model
{
    protected readonly SmartLockContext _context;

    protected Repository(SmartLockContext smartLockContext)
    {
        _context = smartLockContext;
    }

    public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _context.Set<TModel>().AddAsync(GetModel(entity), cancellationToken);
    }

    public void Delete(TEntity entity)
    {
        _context.Set<TModel>().Remove(GetModel(entity));
    }

    protected static TEntity CreateEntityFromModel(TModel model)
    {
        ConstructorInfo constructor = typeof(TEntity)
            .GetConstructor(
                BindingFlags.Instance | BindingFlags.NonPublic, 
                null,
                [typeof(TModel)], 
                null)
            ?? throw new MissingMemberException("No suitable constructor to create instance using model was found");

        var entity = (TEntity)constructor.Invoke([model]);

        return entity;
    }

    protected static TModel GetModel(TEntity entity)
    {
        var modelProperty = entity.GetType().GetField("_model", BindingFlags.NonPublic | BindingFlags.Instance)
            ?? throw new MissingMemberException("The field \"_model\" does not exist");

        var model = (TModel)(modelProperty?.GetValue(entity)
            ?? throw new NullReferenceException())
                ?? throw new Exception("The model type does not match the specified type");

        return model;
    }
}
