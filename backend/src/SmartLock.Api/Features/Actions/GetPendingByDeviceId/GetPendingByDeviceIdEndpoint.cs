using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartLock.Application.Features.Actions.GetPendingAction;

namespace SmartLock.Api.Features.Actions.GetPendingByDeviceId;

public class GetPendingByDeviceIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                $"{ActionsConstants.Routes.Base}/{ActionsConstants.Routes.GetPendingByDeviceId}",
                async (
                    [FromRoute] Guid id,
                    ISender sender,
                    IMapper mapper,
                    HttpContext context,
                    CancellationToken cancellationToken) =>
                {
                    var query = new GetPendingActionQuery(id);
                    var action = await sender.Send(query, cancellationToken);
                    return Results.Ok(action);
                })
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .WithTags(ActionsConstants.ActionTag);
    }
}
