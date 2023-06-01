using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos.Auth;

public class AuthorizationModel
{
    [Required] 
    [EmailAddress] 
    public string Email { get; set; }

    [Required] 
    public string Password { get; set; }
}