using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StoreHouse.Api.Model.Extensions;
using StoreHouse.Api.Model.Mapping;
using StoreHouse.Database.Extensions;

var builder = WebApplication.CreateBuilder(args);
//CORS
builder.Services.AddCors();
//Add Authentication 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "http://localhost:5211",
            ValidAudience = "http://localhost:5211",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ABCDEF"))
        };
    });
//Database Context
var sqlServerConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbServices(sqlServerConnectionString);
//Api Services
builder.Services.AddApiServices();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();

//56465
