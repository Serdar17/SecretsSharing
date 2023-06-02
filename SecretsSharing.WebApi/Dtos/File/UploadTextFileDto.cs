using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos.File;

/// <summary>
/// Model for upload text file
/// </summary>
public class UploadTextFileDto
{
    /// <summary>
    /// Property for cascading deletion
    /// </summary>
    [Required]
    public bool DeleteCascade { get; set; }

    /// <summary>
    /// String with text for upload
    /// </summary>
    [Required]
    public string Text { get; set; }
}