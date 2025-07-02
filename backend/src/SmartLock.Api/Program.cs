using SmartLock.Authorization;
using SmartLock.DataAccessLayer;
using SmartLock.Application;
using SmartLock.Domain;
using SmartLock.Api.Extensions;
using SmartLock.Api;
using SmartLock.Api.Middlewares;
using SmartLock.Messaging.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDomain();
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddKeycloack(builder.Configuration);
builder.Services.AddApi(builder.Configuration);

builder.Services.AddExceptionHandler<ExceptionHandler>();

var frontendOrigin = "_frontendOrigin";

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(frontendOrigin, policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

await app.UseMessagingAsync();
app.UseExceptionHandler("/error");

app.UseCors(frontendOrigin);

app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints();

app.UseHttpsRedirection();

app.Run();