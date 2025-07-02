using AutoMapper;
using MediatR;
using SmartLock.Application.Features.Actions.GetByDeviceId;

namespace SmartLock.Api.Features.Actions.GetByDeviceId;

public class GetActionByDeviceIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            $"{ActionsConstants.Routes.Base}/{ActionsConstants.Routes.GetByDeviceId}",
            async (
                Guid id,
                int pageNumber,
                int pageSize,
                string? sortColumn,
                string? sortOrder,
                int? type,
                int? status,
                DateTime? requestedOnGte,
                DateTime? requestedOnLte,
                DateTime? executedOnGte,
                DateTime? executedOnLte,
                ISender sender,
                IMapper mapper,
                HttpContext context,
                CancellationToken cancellationToken) =>
            {
                var query = new GetActionsByDeviceIdQuery(
                    DeviceId: id,
                    PageNumber: pageNumber,
                    PageSize: pageSize,
                    SortColumn: sortColumn,
                    SortOrder: sortOrder,
                    Type: type,
                    Status: status,
                    RequestedOnGte: requestedOnGte,
                    RequestedOnLte: requestedOnLte,
                    ExecutedOnGte: executedOnGte,
                    ExecutedOnLte: executedOnLte);

                var page = await sender.Send(query, cancellationToken);

                return Results.Ok(page);
            })
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .WithTags(ActionsConstants.ActionTag);
    }
}
