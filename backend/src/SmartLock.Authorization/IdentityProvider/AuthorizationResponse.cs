namespace SmartLock.Authorization.IdentityProvider;

public record AuthorizationResponse(
    string AccessToken, 
    string TokenType);
