using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartLock.Application.Users.Register;

namespace SmartLock.Api.Users.Register;

public class RegisterUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
            $"{UserConstants.Routes.Base}/{UserConstants.Routes.Register}",
            async (
                [FromBody] RegisterUserRequest request, 
                ISender sender, 
                IMapper mapper,
                HttpContext context,
                CancellationToken cancellationToken) => 
            {
                var registerUserCommand = mapper.Map<RegisterUserCommand>(request);

                var id = await sender.Send(registerUserCommand, cancellationToken);

                var userUri = new Uri($"{context.Request.Scheme}://{context.Request.Host}/{UserConstants.Routes.Base}/{id}");

                return Results.Created(userUri, id);
            })
            .RequireAuthorization()
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags(UserConstants.UsersTag);
    }
}
