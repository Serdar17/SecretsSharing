using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.ServiceExtensions;

/// <summary>
/// extension class for configuring authentication
/// </summary>
public static class ConfigureAuthenticationExtension
{
    /// <summary>
    /// method for configuring authentication
    /// </summary>
    /// <param name="services">IServiceCollection object</param>
    /// <param name="configuration">configuration from appsettings.json</param>
    public static void ConfigureAuthentication(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JwtOption:Issuer"],
                ValidAudience = configuration["JwtOption:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtOption:Key"]))
            };
        });
    }
}