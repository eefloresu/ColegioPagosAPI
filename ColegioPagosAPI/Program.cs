using ColegioPagosAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Builder;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<ColegioDbContext>(options =>
//    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
//    new MySqlServerVersion(new Version(8, 0, 40))));

var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
    throw new ArgumentException("La cadena de conexión 'DefaultConnection' no está definida como variable de entorno.");

builder.Services.AddDbContext<ColegioDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 40))));



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var jwtKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
if (string.IsNullOrWhiteSpace(jwtKey))
    throw new ArgumentException("La clave secreta 'JWT_SECRET_KEY' no está definida como variable de entorno.");

Console.WriteLine($"JWT Key en configuracion de programa: {jwtKey}");
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "tu_issuer",
            ValidAudience = "tu_audience",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)),
            RoleClaimType = ClaimTypes.Role
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("❌ Error de autenticación: " + context.Exception.Message);
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("✅ Token validado correctamente.");
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                Console.WriteLine("⚠️ Token rechazado o no enviado.");
                return Task.CompletedTask;
            }
        };

    });

builder.Services.AddAuthorization();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.Use(async (context, next) =>
{
    context.Request.Headers.TryGetValue("Authorization", out var authHeader);
    Console.WriteLine($"Authorization header recibido: [{authHeader}]");
    await next();
});

app.Use(async (context, next) =>
{
    int count = 0;
    foreach (var claim in context.User.Claims)
    {
        count++;
        Console.WriteLine($"Claim {count} {claim.Type}: {claim.Value}");
    }
    await next();
});
app.UseAuthorization();
app.MapControllers();
app.Run();

