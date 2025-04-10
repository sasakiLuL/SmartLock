namespace SmartLock.Api.Devices;

public class DeviceConstants
{
    public static class Routes
    {
        public const string Base = "devices";

        public const string GetById = "{id:guid}";

        public const string GetAll = "";

        public const string Activate = "{id:guid}/activate";

        public const string Remove = "{id:guid}";

        public const string Open = "{id:guid}/open";

        public const string Close = "{id:guid}/close";
    }

    public static readonly string UsersTag = "Users";

    public static readonly string FollowersTag = "Followers";
}
