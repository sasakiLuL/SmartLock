using MediatR;
using SmartLock.Application.Pagination;

namespace SmartLock.Application.Features.Actions.GetByDeviceId;

public record GetActionsByDeviceIdQuery(
    Guid DeviceId,
    int PageNumber,
    int PageSize,
    string? SortColumn,
    string? SortOrder,
    int? Type,
    int? Status,
    DateTime? RequestedOnGte,
    DateTime? RequestedOnLte,
    DateTime? ExecutedOnGte,
    DateTime? ExecutedOnLte) : IRequest<PageResponse<ActionResponse>>;
