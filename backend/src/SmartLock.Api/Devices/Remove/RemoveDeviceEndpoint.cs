
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartLock.Api.Users;
using SmartLock.Application.Devices.GetById;
using SmartLock.Application.Devices.Remove;

namespace SmartLock.Api.Devices.Remove;

public class RemoveDeviceEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(
            $"{DeviceConstants.Routes.Base}/{DeviceConstants.Routes.Remove}",
            async (
                [FromRoute] Guid id,
                ISender sender,
                IMapper mapper,
                HttpContext context,
                CancellationToken cancellationToken) =>
            {
                var command = new RemoveDeviceCommand(id);

                await sender.Send(command, cancellationToken);

                return Results.NoContent();
            })
            .RequireAuthorization()
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .WithTags(UserConstants.UsersTag);
    }
}
