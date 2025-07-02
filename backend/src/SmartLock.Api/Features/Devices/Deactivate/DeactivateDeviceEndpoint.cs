using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartLock.Api.Features.Users;
using SmartLock.Application.Features.Devices.Deactivate;

namespace SmartLock.Api.Features.Devices.Deactivate;

public class DeactivateDeviceEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
            $"{DeviceConstants.Routes.Base}/{DeviceConstants.Routes.Deactivate}",
            async (
                [FromRoute] Guid id,
                ISender sender,
                IMapper mapper,
                CancellationToken cancellationToken) =>
            {
                var command = new DeactivateRequestCommand(id);

                await sender.Send(command, cancellationToken);

                return Results.Ok();
            })
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .WithTags(UserConstants.UsersTag);
    }
}
