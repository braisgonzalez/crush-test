using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Application;
using Users.Domain;
using Users.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Users.Application.Users).Assembly));

builder.Services.AddDbContext<UsersDbContext>(opt => opt.UseInMemoryDatabase("users"));
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/users", async (ISender sender, Users.Application.Users.CreateUserCommand cmd, CancellationToken ct) =>
{
    var dto = await sender.Send(cmd, ct);
    return Results.Created($"/users/{dto.Id}", dto);
}).WithOpenApi();

app.MapPut("/users/{id:guid}", async (Guid id, ISender sender, Users.Application.Users.RenameUserCommand body, CancellationToken ct) =>
{
    var dto = await sender.Send(body with { Id = id }, ct);
    return Results.Ok(dto);
}).WithOpenApi();

app.MapGet("/users/{id:guid}", async (Guid id, ISender sender, CancellationToken ct) =>
{
    var dto = await sender.Send(new Users.Application.Users.GetUserByIdQuery(id), ct);
    return dto is null ? Results.NotFound() : Results.Ok(dto);
}).WithOpenApi();

app.MapGet("/users", async (ISender sender, CancellationToken ct) =>
{
    var dtos = await sender.Send(new Users.Application.Users.ListUsersQuery(), ct);
    return Results.Ok(dtos);
}).WithOpenApi();

app.Run();
