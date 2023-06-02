using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApi.Options;
using WebApi.Services.Interfaces;

namespace WebApi.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly JwtOption _jwtSetting;

    public JwtMiddleware(RequestDelegate next, 
        IOptions<JwtOption> optionsSnapshot)
    {
        _next = next;
        _jwtSetting = optionsSnapshot.Value;
    }
    
    public async Task Invoke(HttpContext context, [FromServices] IUserService _userService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
            await AttachAccountToContextAsync(context, token, _userService);

        await _next(context);
    }
    
    private async Task AttachAccountToContextAsync(HttpContext context, string token, IUserService _userService)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSetting.Key);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var accountId = jwtToken.Claims.First(x => x.Type == "Id").Value;
            var result = await _userService.GetUserByIdAsync(Guid.Parse(accountId));
            context.Items["User"] = result.Value;
        }
        catch
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Something went wrong...");
        }
    }
}