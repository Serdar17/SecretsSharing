using Ardalis.Result;
using Domain.Models;
using WebApi.Dtos.Auth;

namespace WebApi.Services.Interfaces;

public interface IUserService
{
    Task<Result<AuthResponse>> RegistrationAsync(ApplicationUser user, CancellationToken cancellationToken = default);
    
    Task<Result<AuthResponse>> LoginAsync(ApplicationUser user, CancellationToken cancellationToken = default);
}