namespace WebApi.ServiceExtensions;

/// <summary>
/// extension class for cors configuring
/// </summary>
public static class ConfigureCorsExtension
{
    /// <summary>
    /// method for cors configuring
    /// </summary>
    /// <param name="services">IServiceCollection object</param>
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("EnableCORS", cfg => 
            { 
                cfg.WithOrigins("*")
                    .AllowAnyHeader()
                    .AllowAnyMethod(); 
            });
        });
    }
}