using WebApi.Services.Implementation;
using WebApi.Services.Interfaces;

namespace WebApi.ServiceExtensions;

/// <summary>
/// extension class for adding services
/// </summary>
public static class AddServicesExtension
{
    /// <summary>
    /// method for adding services
    /// </summary>
    /// <param name="services">IServiceCollection object</param>
    /// <returns>IServiceCollection object</returns>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IFileService, FileService>();

        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}