using SmartLock.Domain.Core;

namespace SmartLock.Domain.Actions;

public class ActionModel : Model
{
    internal ActionModel() {}

    public Guid UserId { get; set; }

    public Guid DeviceId { get; set; }

    public CommandType CommandType { get; set; }

    public DateTime OccurredOn { get; set; }
}
