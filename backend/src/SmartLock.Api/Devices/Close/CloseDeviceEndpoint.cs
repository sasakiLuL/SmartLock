using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartLock.Api.Users;
using SmartLock.Application.Devices.Close;

namespace SmartLock.Api.Devices.Close;

public class CloseDeviceEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
            $"{DeviceConstants.Routes.Base}/{DeviceConstants.Routes.Close}",
            async (
                [FromRoute] Guid id,
                ISender sender,
                IMapper mapper,
                HttpContext context,
                CancellationToken cancellationToken) =>
            {
                var closeDeviceCommand = new CloseDeviceCommand(id);

                await sender.Send(closeDeviceCommand, cancellationToken);

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
