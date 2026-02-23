using System.Text;
using Application.Interfaces;
using Infrastructure.Data;
using Infrastructure.Jwt;
using Infrastructure.PasswordHash;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApp.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//AddSwaggerGen
builder.Services.AddRegisterSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionString")));

//AddScoped
builder.Services.AddRegistrationServices();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["Redis:ConnectionString"];
});

//Authorize Jwt
builder.Services.AddJwtRegister(builder.Configuration);
builder.Services.AddAuthorization();


var app = builder.Build();

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