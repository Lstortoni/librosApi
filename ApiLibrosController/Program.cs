using ApiLibrosController.Contracts.JwtDto;
using ApiLibrosController.Infrastructure;
using ApiLibrosController.Interfaces.Jwt;
using ApiLibrosController.Interfaces.Repositories;
using ApiLibrosController.Interfaces.Services;
using ApiLibrosController.Repository.Data;
using ApiLibrosController.Repository.FakeImplementation;
using ApiLibrosController.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Npgsql.EntityFrameworkCore.PostgreSQL;                 // UseNpgsql



var builder = WebApplication.CreateBuilder(args);


//Entity Framework Core - Postgres

// Configurar el DbContext con PostgreSQL
// Connection string (appsettings o env var)
var cs = builder.Configuration.GetConnectionString("DefaultConnection")
         ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

// EF Core + Npgsql
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseNpgsql(cs, x =>
    {
        // x.MigrationsHistoryTable("__EFMigrationsHistory", "public"); // opcional
    })
    .UseSnakeCaseNamingConvention(); // opcional, prolijo en Postgres
});

// Add services to the container.

builder.Services.AddControllers();


builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection("Jwt"));
// Services

builder.Services.AddScoped<ILibroService, LibroService>();


// Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();



// Repos InMemory (para probar YA)

builder.Services.AddSingleton<ILibroRepository, LibroRepositoryInMemory>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//JWT SETTINGS//


// Configurar JWT
var key = builder.Configuration["Jwt:Key"]; // viene del appsettings.json
var issuer = builder.Configuration["Jwt:Issuer"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = issuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });

builder.Services.AddAuthorization(); // 👈 para usar [Authorize]



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); 

app.UseAuthorization();

app.MapControllers();

app.Run();
