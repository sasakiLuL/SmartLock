using Microsoft.Extensions.Options;
using SmartLock.Application.Abstractions;
using SmartLock.Authorization.Options;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SmartLock.Authorization.IdentityProvider;

public class IdentityService(
    HttpClient httpClient, 
    IOptions<AuthenticationOptions> options) : IIdentityService
{
    public async Task<bool> IsExistsAsync(Guid identityProviderId, CancellationToken token = default)
    {
        var authorizationResponse = await AuthorizeClient(token);

        using var isExistRequest = new HttpRequestMessage(
            HttpMethod.Get,
            $"{options.Value.Address}/admin/realms/{options.Value.Realm}/users/{identityProviderId}");

        isExistRequest.Headers.Authorization = new AuthenticationHeaderValue(
            authorizationResponse.TokenType,
            authorizationResponse.AccessToken);

        var response = await httpClient.SendAsync(isExistRequest, token);

        return response.IsSuccessStatusCode;
    }

    private async Task<AuthorizationResponse> AuthorizeClient(CancellationToken token = default)
    {
        using var authorizationRequest = new HttpRequestMessage(
           HttpMethod.Post,
           $"{options.Value.Address}/realms/{options.Value.Realm}/protocol/openid-connect/token");

        authorizationRequest.Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
        {
            new("grant_type", "client_credentials"),
            new("client_id", options.Value.ClientId),
            new("client_secret", options.Value.Secret)
        });

        var authorizationResponse = await httpClient.SendAsync(authorizationRequest, token);

        authorizationResponse.EnsureSuccessStatusCode();

        var json = await authorizationResponse.Content.ReadAsStringAsync();

        var deserializationOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };

        return JsonSerializer.Deserialize<AuthorizationResponse>(json, deserializationOptions) 
            ?? throw new Exception("Authorization response can not be parsed.");
    }
}
