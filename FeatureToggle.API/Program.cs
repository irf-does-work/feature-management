using System.Reflection;
using System.Text;
using FeatureToggle.Application.Requests.Commands.UserCommands;
using FeatureToggle.Domain.ConfigurationModels;
using FeatureToggle.Domain.Entity.User_Schema;
using FeatureToggle.Domain.Validators;
using FeatureToggle.Infrastructure.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddIdentityApiEndpoints<User>()
                .AddEntityFrameworkStores<UserContext>();
///below used addidentity earlier
builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<UserContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<Authentication>(builder.Configuration.GetSection("Authentication"));
//builder.Services.AddTransient<UserValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AddUserCommandValidator>();



builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789.@";
    options.User.RequireUniqueEmail = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireDigit = false;
});

builder.Services.AddAuthentication(x =>
                                    {
                                        x.DefaultAuthenticateScheme =
                                        x.DefaultChallengeScheme =
                                        x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                                    })
                                    .AddJwtBearer(x =>
                                    {
                                        x.SaveToken = false;
                                        x.TokenValidationParameters = new TokenValidationParameters
                                        {
                                            ValidateIssuerSigningKey = true,
                                            IssuerSigningKey = new SymmetricSecurityKey(
                                                Encoding.UTF8.GetBytes(builder.Configuration["Authentication:JWTSecret"]!))
                                        };
                                    });

builder.Services.AddDbContext<UserContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("UserDbContext")));
builder.Services.AddDbContext<FeatureContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("FeatureDbContext")));

builder.Services.AddMediatR(x =>
    x.RegisterServicesFromAssembly(Assembly.Load("FeatureToggle.Application"))
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.MapIdentityApi<User>();

app.UseHttpsRedirection();
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

