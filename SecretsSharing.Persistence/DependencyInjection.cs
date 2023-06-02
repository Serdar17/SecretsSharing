using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SecretsSharing.Persistence.Data;
using SecretsSharing.Persistence.Repository.Implementation;
using SecretsSharing.Persistence.Repository.Interfaces;

namespace SecretsSharing.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetValue<string>("ConnectionString:DefaultConnection"));
        });
        
           
        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetService<ApplicationDbContext>());
        
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IFileRepository, FileRepository>();

        return services;
    }
}