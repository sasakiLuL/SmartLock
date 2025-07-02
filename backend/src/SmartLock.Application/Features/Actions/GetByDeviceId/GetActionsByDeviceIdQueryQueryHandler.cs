using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartLock.Application.Interfaces;
using SmartLock.Application.Pagination;
using SmartLock.Domain.Exceptions;
using SmartLock.Domain.Features.Devices;
using SmartLock.Domain.Features.Devices.Actions;
using SmartLock.Domain.Features.Users;
using System.Linq.Expressions;

namespace SmartLock.Application.Features.Actions.GetByDeviceId;

public class GetActionsByDeviceIdQueryQueryHandler(
    IUserRepository userRepository,
    IDeviceRepository deviceRepository,
    IReadModelService readModelService,
    IUserCredentialsProvider userCredentialsProvider,
    IMapper mapper) : IRequestHandler<GetActionsByDeviceIdQuery, PageResponse<ActionResponse>>
{
    public async Task<PageResponse<ActionResponse>> Handle(GetActionsByDeviceIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.ReadByIdentityProviderIdAsync(
            userCredentialsProvider.UserId,
            cancellationToken) ?? throw new ForbiddenException();

        var device = await deviceRepository.ReadByIdAsync(
            request.DeviceId,
            cancellationToken) ?? throw new NotFoundException(DeviceErrors.NotFound(request.DeviceId));

        var actionsQuery = readModelService.Query<ActionModel>()
            .Where(x => x.UserId == user.Model.Id && x.DeviceId == request.DeviceId);


        if (request.Type is not null)
        {
            actionsQuery = actionsQuery.Where(x => x.Type == (ActionType)request.Type);
        }

        if (request.Status is not null)
        {
            actionsQuery = actionsQuery.Where(x => x.Status == (ActionStatus)request.Status);
        }

        if (request.RequestedOnGte is not null)
        {
            actionsQuery = actionsQuery.Where(x => x.RequestedOn >= request.RequestedOnGte);
        }

        if (request.RequestedOnLte is not null)
        {
            actionsQuery = actionsQuery.Where(x => x.RequestedOn <= request.RequestedOnLte);
        }

        if (request.ExecutedOnGte is not null)
        {
            actionsQuery = actionsQuery.Where(x => x.ExecutedOn >= request.ExecutedOnGte);
        }

        if (request.ExecutedOnLte is not null)
        {
            actionsQuery = actionsQuery.Where(x => x.ExecutedOn <= request.ExecutedOnLte);
        }

        var sortColumn = GetSortColumn(request);

        if (request.SortOrder?.ToLower() == "asc")
        {
            actionsQuery = actionsQuery.OrderBy(sortColumn);
        }
        else
        {
            actionsQuery = actionsQuery.OrderByDescending(sortColumn);
        }

        var totalCount = await actionsQuery.CountAsync(cancellationToken);

        actionsQuery = actionsQuery
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize);

        var items = await actionsQuery.ToListAsync(cancellationToken);

        var page = PageResponse<ActionResponse>.Create(
            items.Select(mapper.Map<ActionResponse>).ToList(),
            totalCount,
            request.PageNumber,
            request.PageSize);

        return page;
    }

    private Expression<Func<ActionModel, object?>> GetSortColumn(GetActionsByDeviceIdQuery request) =>
        request.SortColumn?.ToLower() switch
        {
            "type" => action => action.Type,
            "status" => action => action.Status,
            "requestedon" => action => action.RequestedOn,
            "executedon" => action => action.ExecutedOn,
            _ => action => action.RequestedOn
        };
}
