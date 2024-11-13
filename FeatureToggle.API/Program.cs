using System.Reflection;
using FeatureToggle.Domain.Entity.User_Schema;
using FeatureToggle.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddIdentityApiEndpoints<User>()
                .AddEntityFrameworkStores<UserContext>();
///below use addidentity later
builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<UserContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+!#$%^&*()";
    options.User.RequireUniqueEmail = true; 
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

app.MapIdentityApi<User>();

app.UseHttpsRedirection();
//app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());


app.UseAuthorization();

app.MapControllers();

app.Run();

