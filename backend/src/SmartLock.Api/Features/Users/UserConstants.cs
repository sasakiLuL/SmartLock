namespace SmartLock.Api.Features.Users;

public static class UserConstants
{
    public static class Routes
    {
        public const string Base = "users";

        public const string GetById = "{id:guid}";

        public const string UpdateUsername = "{id:guid}/username";

        public const string Register = "register";
    }

    public static readonly string UsersTag = "Users";

    public static readonly string FollowersTag = "Followers";
}
