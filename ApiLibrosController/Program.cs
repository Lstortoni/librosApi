using ApiLibrosController.Contracts.JwtDto;
using ApiLibrosController.Infrastructure;
using ApiLibrosController.Interfaces.Jwt;
using ApiLibrosController.Interfaces.Repositories;
using ApiLibrosController.Interfaces.Services;
using ApiLibrosController.Repository.FakeImplementation;
using ApiLibrosController.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection("Jwt"));
// Services
builder.Services.AddScoped<IVendedorService, VendedorService>();
builder.Services.AddScoped<ILibroService, LibroService>();


// Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();



// Repos InMemory (para probar YA)
builder.Services.AddSingleton<IVendedorRepository, VendedorRepositoryInMemory>();
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
