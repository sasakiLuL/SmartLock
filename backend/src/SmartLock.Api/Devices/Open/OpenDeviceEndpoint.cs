using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartLock.Api.Users;
using SmartLock.Application.Devices.Open;

namespace SmartLock.Api.Devices.Open;

public class OpenDeviceEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
            $"{DeviceConstants.Routes.Base}/{DeviceConstants.Routes.Open}",
            async (
                [FromRoute] Guid id,
                ISender sender,
                IMapper mapper,
                HttpContext context,
                CancellationToken cancellationToken) =>
            {
                var openDeviceCommand = new OpenDeviceCommand(id);

                await sender.Send(openDeviceCommand, cancellationToken);

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
