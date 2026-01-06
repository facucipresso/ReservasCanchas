using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ReservasCanchas.BusinessLogic;
using ReservasCanchas.BusinessLogic.JWTService;
using ReservasCanchas.BusinessLogic.Middlewares;
using ReservasCanchas.Controller.Background;
using ReservasCanchas.DataAccess.Persistance;
using ReservasCanchas.DataAccess.Repositories;
using ReservasCanchas.Domain.Entities;
using System;

var builder = WebApplication.CreateBuilder(args);

//agrego cors para no tener problemas con swagger
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .WithExposedHeaders("Location")
              .AllowAnyMethod();
    });
});

//PASO 16 ESO DE JWT, esto es para que swagger me pida el token y lo mande en cada request (paso 17 en el controllador de acciones)
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

// registro DbContext con Postgre
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// PASO 1 USO DE JWT
//addidentity es el sist de autenticacion y manejo de usuarios que me da .net , appUser es mi usuario personalizado y identityroles representa los roles
builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    //estan son las reglas con las que tiene que cumplir una contraseña
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
})
//le digo a identity donde guardar los usuarios y roles
.AddEntityFrameworkStores<AppDbContext>();

// PASO 2 USO DE JWT
//configuro mi sistema de autenticacion global de mi app, addauthentication inicializa el middleware de autenticacion
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    options.DefaultChallengeScheme =
    options.DefaultForbidScheme =
    options.DefaultScheme =
    options.DefaultSignInScheme =
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    // PASO 3 USO DE JWT (validación del token), paso 4 appsettingsjson
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SignInKey"])
        ),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(5) //TimeSpan.Zero  sin margen de tiempo extra
    };

    //  Agregá este bloque para ver logs del proceso de validación
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($" Token inválido: {context.Exception.Message}");
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Console.WriteLine($" Token válido para: {context.Principal.Identity?.Name}");
            return Task.CompletedTask;
        }
    };
});

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

builder.Services.AddScoped<AccountBusinessLogic>();

// PARA EL TRABAJO EN BACKGROUND
builder.Services.AddHostedService<ReservationCompletionService>();

//PASO 12 (paso 13 en los dto, para devolver un dto que contenga en token)
builder.Services.AddScoped<TokenService>();

//para el auth service
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<AuthService>();



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
//builder.Services.AddSwaggerGen();

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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();

    // Asegurar que los roles existen (sin recrearlos)
    string superAdminRole = "SuperAdmin";

    // Crear admin si no existe
    var admin = await userManager.FindByNameAsync("admin");

    if (admin == null)
    {
        var newAdmin = new User
        {
            UserName = "admin",
            Email = "admin@example.com",
        };

        var result = await userManager.CreateAsync(newAdmin, "Admin1234");

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(newAdmin, superAdminRole);
            Console.WriteLine("? Usuario SuperAdmin creado correctamente");
        }
        else
        {
            Console.WriteLine("? Error creando SuperAdmin:");
            foreach (var e in result.Errors)
                Console.WriteLine($" - {e.Description}");
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();


app.UseHttpsRedirection();

//Activar CORS antes de MapControllers
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

// permito archivos estaticos
app.UseStaticFiles();

app.MapControllers();

app.Run();
