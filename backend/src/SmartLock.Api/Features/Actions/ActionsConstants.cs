namespace SmartLock.Api.Features.Actions;

public static class ActionsConstants
{
    public static class Routes
    {
        public const string Base = "actions";

        public const string GetByDeviceId = "devices/{id:guid}";

        public const string GetById = "{id:guid}";

        public const string GetPendingByDeviceId = "devices/{id:guid}/pending";
    }

    public static readonly string ActionTag = "Actions";
}
