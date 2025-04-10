using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartLock.Api.Users;
using SmartLock.Application.Devices.GetById;

namespace SmartLock.Api.Devices.GetById;

public class GetDeviceByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            $"{DeviceConstants.Routes.Base}/{DeviceConstants.Routes.GetById}",
            async (
                [FromRoute] Guid id,
                ISender sender,
                IMapper mapper,
                HttpContext context,
                CancellationToken cancellationToken) =>
            {
                var query = new GetDeviceByIdQuery(id);

                var device = await sender.Send(query, cancellationToken);

                return Results.Ok(device);
            })
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .WithTags(UserConstants.UsersTag);
    }
}
