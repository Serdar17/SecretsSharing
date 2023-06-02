using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos.File;

/// <summary>
/// Model for upload file
/// </summary>
public class UploadFileDto
{
    /// <summary>
    /// Property for cascading deletion
    /// </summary>
    [Required]
    public bool DeleteCascade { get; set; }
    
    /// <summary>
    /// Downloadable file
    /// </summary>
    public IFormFile File { get; set; }
}