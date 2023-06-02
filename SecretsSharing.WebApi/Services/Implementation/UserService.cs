using System.Security.Claims;
using Ardalis.Result;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SecretsSharing.Persistence.Repository.Interfaces;
using WebApi.Dtos.Auth;
using WebApi.Options;
using WebApi.Services.Interfaces;

namespace WebApi.Services.Implementation;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly JwtOption _jwtOption;
    private readonly PasswordHasher<ApplicationUser> _hasher;
    private readonly ITokenService _tokenService;

    public UserService(IUserRepository userRepository, 
        IOptions<JwtOption> optionsSnapshot, 
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        _hasher = new PasswordHasher<ApplicationUser>();
        _tokenService = tokenService;
        _jwtOption = optionsSnapshot.Value;
    }

    public async Task<Result<AuthResponse>> RegistrationAsync(ApplicationUser user, 
        CancellationToken cancellationToken = default)
    {
        var userExists = await _userRepository.FindByEmailAsync(user.Email, cancellationToken);

        if (userExists is not null)
        {
            return Result.Error("User with current email already exists");
        }
        
        user.PasswordHash = _hasher.HashPassword(user, user.PasswordHash);

        await _userRepository.AddAsync(user, cancellationToken);

        return Result.Success();
    }

    public async Task<Result<AuthResponse>> LoginAsync(ApplicationUser user, 
        CancellationToken cancellationToken = default)
    {
        var userExists = await _userRepository.FindByEmailAsync(user.Email, cancellationToken);

        if (userExists is null || _hasher.VerifyHashedPassword(userExists, userExists.PasswordHash, user.PasswordHash) 
            != PasswordVerificationResult.Success)
        {
            return Result.Error("Invalid email or password");
        }
        
        var authClaims = new List<Claim>()
        {
            new (ClaimTypes.Email, userExists.Email),
            new ("Id", userExists.Id.ToString())
        };
        
        var accessToken = _tokenService.GenerateToken(authClaims);
        var refreshToken = _tokenService.GenerateRefreshToken();

        userExists.RefreshToken = refreshToken;
        userExists.RefreshTokenExpiryTime = DateTime.Now.AddDays(_jwtOption.RefreshTokenExpiryDurationDays);

        await _userRepository.UpdateAsync(userExists, cancellationToken);
        
        return new AuthResponse(userExists.Id, userExists.Email, accessToken, refreshToken);
    }

    public async Task<Result<ApplicationUser>> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetUserByIdAsync(id, cancellationToken);

        if (user is null)
        {
            Result.NotFound($"User with this id={id} not exists");
        }

        return user;
    }
    
    public async Task<Result<TokenApiDto>> RefreshTokenAsync(TokenApiDto tokenApiModel)
    {
        var accessToken = tokenApiModel.AccessToken;
        var refreshToken = tokenApiModel.RefreshToken;

        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

        if (principal is null)
        {
            return Result.Error("Invalid access token or refresh token");
        }
        
        var userId = principal.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
        
        var user = await _userRepository.GetUserByIdAsync(Guid.Parse(userId));
        
        if (user == null || user.RefreshToken != refreshToken ||
            user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            return Result.Error("Invalid access token or refresh token");
        }
        
        var newAccessToken = _tokenService.GenerateToken(principal.Claims.ToList());
        var newRefreshToken = _tokenService.GenerateRefreshToken();
        
        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_jwtOption.RefreshTokenExpiryDurationDays);
        
        await _userRepository.UpdateAsync(user);

        return Result.Success(new TokenApiDto()
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        });
    }
}