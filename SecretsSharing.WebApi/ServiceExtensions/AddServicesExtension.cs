using Domain.Models;
using Microsoft.AspNetCore.Identity;
using WebApi.Services.Implementation;
using WebApi.Services.Interfaces;

namespace WebApi.ServiceExtensions;

public static class AddServicesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IFileService, FileService>();

        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}