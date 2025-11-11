using Microsoft.EntityFrameworkCore;
using ReservasCanchas.BusinessLogic;
using ReservasCanchas.DataAccess.Persistance;
using ReservasCanchas.DataAccess.Repositories;
using System;

var builder = WebApplication.CreateBuilder(args);

// registro DbContext con Postgre
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//agrego la dependencia
builder.Services.AddScoped<ServiceBusinessLogic>();
builder.Services.AddScoped<ServiceRepository>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
