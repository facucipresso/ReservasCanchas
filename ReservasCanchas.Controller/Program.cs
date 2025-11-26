using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservasCanchas.BusinessLogic;
using ReservasCanchas.BusinessLogic.Middlewares;
using ReservasCanchas.Controller.Background;
using ReservasCanchas.DataAccess.Persistance;
using ReservasCanchas.DataAccess.Repositories;
using ReservasCanchas.Domain.Entities;
using System;

var builder = WebApplication.CreateBuilder(args);

// registro DbContext con Postgre
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//agrego dependencias de BusinessLogic y DataAccess
builder.Services.AddScoped<ServiceBusinessLogic>();
builder.Services.AddScoped<ServiceRepository>();

builder.Services.AddScoped<UserBusinessLogic>();
builder.Services.AddScoped<UserRepository>();

builder.Services.AddScoped<ComplexBusinessLogic>();
builder.Services.AddScoped<ComplexRepository>();

builder.Services.AddScoped<FieldBusinessLogic>();
builder.Services.AddScoped<FieldRepository>();

builder.Services.AddScoped<ReservationBusinessLogic>();
builder.Services.AddScoped<ReservationRepository>();

builder.Services.AddScoped<ReviewBusinessLogic>();
builder.Services.AddScoped<ReviewRepository>();

builder.Services.AddScoped<NotificationBusinessLogic>();
builder.Services.AddScoped<NotificationRepository>();

// PARA EL TRABAJO EN BACKGROUND
builder.Services.AddHostedService<ReservationCompletionService>();


// Configurar respuestas de error de validación de modelo
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Solicitud incorrecta",
            Detail = "Uno o más campos tienen errores",
            Instance = context.HttpContext.Request.Path,
        };

        problemDetails.Extensions["errors"] = errors;

        return new ObjectResult(problemDetails)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };
    };
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

//builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

// HABILITAR WEBROOT
builder.Environment.WebRootPath ??= Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();

//app.UseHttpsRedirection();

//app.UseAuthorization();

// permito archivos estaticos
app.UseStaticFiles();

app.MapControllers();

app.Run();
