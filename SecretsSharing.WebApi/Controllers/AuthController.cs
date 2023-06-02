using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos.Auth;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers;

public class AuthController : ApiController
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public AuthController(IMapper mapper, IUserService userService)
    {
        _mapper = mapper;
        _userService = userService;
    }
    
    /// <summary>
    /// Enpoint for user registration
    /// </summary>
    /// <param name="authorizationModel">Authorization model with password and user login</param>
    /// <returns>Message about successful authorization</returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] AuthorizationModel authorizationModel)
    {
        var user = _mapper.Map<ApplicationUser>(authorizationModel);

        var result = await _userService.RegistrationAsync(user);

        if (result.IsSuccess)
        {
            return Ok(new
            {
                Message = "Registration was successful"
            });
        }

        return BadRequest(new
        {
            Status = "Error",
            Message = result.Errors.FirstOrDefault()
        });
    }
    
    /// <summary>
    /// Endpoint for user authorization
    /// </summary>
    /// <param name="authorizationModel">Authorization model with password and user login</param>
    /// <returns>Model with user, access and refresh tokens</returns>
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] AuthorizationModel authorizationModel)
    {
        var user = _mapper.Map<ApplicationUser>(authorizationModel);

        var result = await _userService.LoginAsync(user);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(new
        {
            Status = "Error",
            Message = result.Errors.FirstOrDefault()
        });
    }
    
    /// <summary>
    /// Endpoint for refresh token
    /// </summary>
    /// <param name="tokenApiModel">Model with access and refresh tokens</param>
    /// <returns>Object with new access and refresh tokens</returns>
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] TokenApiDto tokenApiModel)
    {
        var result = await _userService.RefreshTokenAsync(tokenApiModel);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Errors);
    }
}