using Microsoft.AspNetCore.Http;
using SmartLock.Application.Interfaces;
using System.Security.Claims;

namespace SmartLock.Authorization;

public class UserCredentialsProvider(IHttpContextAccessor httpContextAccessor) : IUserCredentialsProvider
{
    public Guid UserId
    {
        get
        {
            var idString = httpContextAccessor?.HttpContext?.User.Claims
                .FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value
                    ?? throw new ArgumentException("The user identifier claim is required.", nameof(httpContextAccessor));

            return Guid.Parse(idString);
        }
    }
}
