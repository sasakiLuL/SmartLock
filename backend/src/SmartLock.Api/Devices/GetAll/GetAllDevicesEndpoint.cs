using AutoMapper;
using MediatR;
using SmartLock.Api.Users;
using SmartLock.Application.Devices.GetAll;

namespace SmartLock.Api.Devices.GetAll;

public class GetAllDevicesEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            $"{DeviceConstants.Routes.Base}",
            async (
                ISender sender,
                IMapper mapper,
                HttpContext context,
                CancellationToken cancellationToken) =>
            {
                var query = new GetAllDevicesQuery();

                var devices = await sender.Send(query, cancellationToken);

                return Results.Ok(devices);
            })
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .WithTags(UserConstants.UsersTag);
    }
}
