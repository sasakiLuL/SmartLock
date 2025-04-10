using AutoMapper;
using MediatR;
using SmartLock.Api.Users.Register;
using SmartLock.Api.Users;
using Microsoft.AspNetCore.Mvc;
using SmartLock.Application.Devices.ActivationRequest;

namespace SmartLock.Api.Devices.Activation;

public class ActivationEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
            $"{DeviceConstants.Routes.Base}/{DeviceConstants.Routes.Activate}",
            async (
                [FromRoute] Guid id,
                ISender sender,
                IMapper mapper,
                HttpContext context,
                CancellationToken cancellationToken) =>
            {
                var activateDeviceCommand = new ActivationRequestCommand(id);

                var deviceId = await sender.Send(activateDeviceCommand, cancellationToken);

                var deviceUri = new Uri($"{context.Request.Scheme}://{context.Request.Host}/{DeviceConstants.Routes.Base}/{id}");

                return Results.Created(deviceUri, id);
            })
            .RequireAuthorization()
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .WithTags(UserConstants.UsersTag);
    }
}
