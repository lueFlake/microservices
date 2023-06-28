using AdAstra.HRPlatform.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using FluentValidation.AspNetCore;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Afonin.AuthService.DbContexts;
using Afonin.AuthService.Services;
using Afonin.AuthService.Services.Injection;
using Afonin.AuthService.Helpers;
using Afonin.AuthService.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddDbContext<MainDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MainDbConnection")));

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            ValidateLifetime = true,
            IssuerSigningKey = JwtHelper.GetSymmetricSecurityKey(builder.Configuration["JwtSettings:Secret"]),
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddScoped(typeof(IEfRepository<>), typeof(EfRepository<>));

//builder.Services.AddCors();
builder.Services.AddMainServices();
builder.Services.AddAutoMapper(typeof(UserProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "api/v1/swagger";
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
        c.DocExpansion(DocExpansion.None);
    });
}

app.MapControllers();


app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
