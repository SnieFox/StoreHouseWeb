using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.OpenApi.Models;
using StoreHouse.Api.Model.Extensions;
using StoreHouse.Api.Model.Mapping;
using StoreHouse.Api.Services;
using StoreHouse.Api.Services.Interfaces;
using StoreHouse.Database.Entities;
using StoreHouse.Database.Extensions;

var builder = WebApplication.CreateBuilder(args);
var tokenLifetimeManager = new JwtTokenLifetimeManager();
//CORS
builder.Services.AddCors();
builder.Services
    .AddSingleton<ITokenLifetimeManager>(tokenLifetimeManager);
//Add Authentication 
builder.Services.AddAuthentication(j =>
{
    j.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    j.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    j.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(j =>
{
    j.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        LifetimeValidator = tokenLifetimeManager.ValidateTokenLifetime

    };
});

builder.Services.AddAuthorization();

//Database Context
var sqlServerConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbServices(sqlServerConnectionString);
//Api Services
builder.Services.AddApiServices();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    // Добавьте конфигурацию для Swagger с использованием JWT
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter JWT Bearer token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",  // Use "bearer" for JWT
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, new string[] { } }
    });
});
//AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();