namespace SmartLock.Domain.Core;

public abstract class Entity<TModel> where TModel : Model
{
    protected readonly TModel _model;

    protected Entity(TModel model)
    {
        _model = model;
    }
}
