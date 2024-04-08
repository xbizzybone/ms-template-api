using FluentValidation;
using ms.template.features.users.Application.DTOs;
using ms.template.features.users.Application.Interfaces;
using ms.template.features.users.Application.Schemas;
using ms.template.features.users.Application.Services;
using ms.template.features.users.Application.Validators;
using ms.template.features.users.Infraestructure;
using ms.template.shared.logger;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = LoggerConfig.GetConsoleLogger().CreateLogger();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IValidator<UserRequest>, UserValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/users", async (IUserService userService) =>
{
    var users = await userService.GetUsersAsync();

    return Results.Ok(users);
});

app.MapGet("/users/{id}", async (IUserService userService, Guid id) =>
{
    var user = await userService.GetUserAsync(id);

    return user is null ? Results.NotFound() : Results.Ok(user);
});

app.MapPost("/users", async (IUserService userService, UserRequest request, IValidator<UserRequest> Validator) =>
{
    var validationResult = Validator.Validate(request);

    if (!validationResult.IsValid)
    {
        return Results.BadRequest(validationResult.Errors);
    }

    var userDTO = new UserDTO
    {
        Name = request.Name,
        Email = request.Email,
        Password = request.Password
    };

    var user = await userService.CreateUserAsync(userDTO);

    return Results.Created($"/users/{user.Id}", user);
});


app.MapPut("/users/{id}", async (IUserService userService, Guid id, UserRequest request, IValidator<UserRequest> Validator) =>
{
    var validationResult = Validator.Validate(request);

    if (!validationResult.IsValid)
    {
        return Results.BadRequest(validationResult.Errors);
    }

    var userDTO = new UserDTO
    {
        Name = request.Name,
        Email = request.Email,
        Password = request.Password
    };

    var user = await userService.UpdateUserAsync(id, userDTO);

    return Results.Ok(user);
});

app.MapDelete("/users/{id}", async (IUserService userService, Guid id) =>
{
    await userService.DeleteUserAsync(id);

    return Results.NoContent();
});

app.Run();