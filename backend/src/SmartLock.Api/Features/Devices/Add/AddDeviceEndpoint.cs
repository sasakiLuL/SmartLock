using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartLock.Api.Features.Users;
using SmartLock.Application.Features.Devices.Add;

namespace SmartLock.Api.Features.Devices.Add;

public class AddDeviceEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
            $"{DeviceConstants.Routes.Base}/{DeviceConstants.Routes.Add}",
            async (
                [FromBody] AddDeviceRequest request,
                ISender sender,
                IMapper mapper,
                HttpContext context,
                CancellationToken cancellationToken) =>
            {
                var command = mapper.Map<AddDeviceCommand>(request);

                var deviceId = await sender.Send(command, cancellationToken);

                var deviceUri = new Uri($"{context.Request.Scheme}://{context.Request.Host}/{DeviceConstants.Routes.Base}/{deviceId}");

                return Results.Created(deviceUri, deviceId);
            })
            .RequireAuthorization()
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .WithTags(UserConstants.UsersTag);
    }
}
