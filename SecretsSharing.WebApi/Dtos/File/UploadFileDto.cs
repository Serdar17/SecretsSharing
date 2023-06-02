using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos.File;

public class UploadFileDto
{
    [Required]
    public bool DeleteCascade { get; set; }
    
    public IFormFile File { get; set; }
}