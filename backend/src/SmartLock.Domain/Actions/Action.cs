using SmartLock.Domain.Core;

namespace SmartLock.Domain.Actions;

public class Action : Entity<ActionModel>
{
    private Action(ActionModel model) : base(model) {}

    public Guid Id { get => _model.Id; }

    public Guid UserId { get => _model.UserId; }

    public Guid DeviceId { get => _model.DeviceId; }

    public CommandType CommandType { get => _model.CommandType; }

    public DateTime OccuredOn { get => _model.OccurredOn; }

    public static Action Create(
        Guid userId,
        Guid deviceId,
        CommandType commandType,
        DateTime dateTime)
    {
        var model = new ActionModel()
        {
            UserId = userId,
            DeviceId = deviceId,
            CommandType = commandType,
            OccurredOn = dateTime
        };

        return new Action(model);
    }
}
