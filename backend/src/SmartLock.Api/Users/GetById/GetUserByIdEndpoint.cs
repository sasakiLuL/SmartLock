using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartLock.Application.Users;
using SmartLock.Application.Users.GetById;

namespace SmartLock.Api.Users.GetById;

public class GetUserByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            $"{UserConstants.Routes.Base}/{UserConstants.Routes.GetById}",
            async (
                [FromRoute] Guid id, 
                ISender sender, 
                CancellationToken cancellationToken) =>
            {
                var query = new GetUserByIdQuery(id);

                var result = await sender.Send(query, cancellationToken);

                return Results.Ok(result);
            })
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK, typeof(UserResponse))
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(UserConstants.UsersTag);
    }
}
