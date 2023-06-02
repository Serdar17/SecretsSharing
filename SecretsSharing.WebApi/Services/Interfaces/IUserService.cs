using Ardalis.Result;
using Domain.Models;
using WebApi.Dtos.Auth;

namespace WebApi.Services.Interfaces;

/// <summary>
/// interface that represents abstraction over user interaction
/// </summary>
public interface IUserService
{
    /// <summary>
    /// method for user registration
    /// </summary>
    /// <param name="user">user to register</param>
    /// <param name="cancellationToken">Cancellation token with default value</param>
    /// <returns>Model about successful authorization</returns>
    Task<Result<AuthResponse>> RegistrationAsync(ApplicationUser user, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// method for user authorization
    /// </summary>
    /// <param name="user">user to authorization</param>
    /// <param name="cancellationToken">Cancellation token with default value</param>
    /// <returns>Model about successful authorization</returns>
    Task<Result<AuthResponse>> LoginAsync(ApplicationUser user, CancellationToken cancellationToken = default);

    /// <summary>
    /// method for getting a user by id
    /// </summary>
    /// <param name="id">unique user id</param>
    /// <param name="cancellationToken">Cancellation token with default value</param>
    /// <returns>User object</returns>
    Task<Result<ApplicationUser>> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// method for updating the user's refresh token
    /// </summary>
    /// <param name="tokenApiDto">Model with access and refresh tokens</param>
    /// <returns>Model with new access and refresh tokens</returns>
    Task<Result<TokenApiDto>> RefreshTokenAsync(TokenApiDto tokenApiDto);
}