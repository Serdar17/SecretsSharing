using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos.Auth;

/// <summary>
/// Authorization model with password and user login
/// </summary>
public class AuthorizationModel
{
    /// <summary>
    /// User's email
    /// </summary>
    [Required] 
    [EmailAddress] 
    public string Email { get; set; }

    /// <summary>
    /// User password
    /// </summary>
    [Required] 
    public string Password { get; set; }
}