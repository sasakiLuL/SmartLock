namespace SmartLock.Api.Features.Devices;

public static class DeviceConstants
{
    public static class Routes
    {
        public const string Base = "devices";

        public const string Add = "";

        public const string GetById = "{id:guid}";

        public const string GetAll = "";

        public const string Activate = "{id:guid}/activate";

        public const string Deactivate = "{id:guid}/deactivate";

        public const string Remove = "{id:guid}";

        public const string Unlock = "{id:guid}/unlock";

        public const string Lock = "{id:guid}/lock";
    }

    public static readonly string UsersTag = "Users";
}
