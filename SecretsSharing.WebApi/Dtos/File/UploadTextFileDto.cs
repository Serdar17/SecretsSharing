using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos.File;

public class UploadTextFileDto
{
    [Required]
    public bool DeleteCascade { get; set; }

    [Required]
    public string Text { get; set; }
}