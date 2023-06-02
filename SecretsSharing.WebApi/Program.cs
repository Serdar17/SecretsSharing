using SecretsSharing.Persistence;
using WebApi.Mappings;
using WebApi.Middleware;
using WebApi.Options;
using WebApi.ServiceExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<JwtOption>(builder.Configuration.GetSection(JwtOption.Section));


builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<ApplicationUserProfile>();
});

builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddServices();

#region Configure Cors

builder.Services.ConfigureCors();

#endregion

#region Configure Swagger

builder.Services.ConfigureSwagger();

#endregion

#region Authentication

builder.Services.ConfigureAuthentication(builder.Configuration);

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseStaticFiles();
// app.UseCustomExceptionHandle();

app.UseExceptionHandler("/Error");
app.UseHsts();
app.UseStaticFiles();
// app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("EnableCORS");

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<JwtMiddleware>();

// app.MapDefaultControllerRoute();
app.MapControllers();

app.Run();