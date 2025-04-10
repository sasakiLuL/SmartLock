namespace SmartLock.Authorization.Options;

public class AuthenticationOptions
{
    public string Address { get; set; } = string.Empty;

    public string Realm { get; set; } = string.Empty;

    public string ClientId { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;

    public string Secret { get; set; } = string.Empty;

    public bool? RequireHttpsMetadata { get; set; } = true;

    public bool? ValidateIssuer { get; set; } = true;

    public static readonly string Section = "Authentication";
}
