using Domain.Enums;
using Domain.Primitives;

namespace Domain.Models;

/// <summary>
/// File details data access model 
/// </summary>
public class FileDetails : BaseEntity<Guid>
{
    /// <summary>
    /// File name
    /// </summary>
    public string FileName { get; set; }
    
    /// <summary>
    /// File type
    /// </summary>
    public FileType FileType { get; set; }

    /// <summary>
    /// Path to the file
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// Сascade deletion property
    /// </summary>
    public bool DeleteCascade { get; set; }

    /// <summary>
    /// Unique user id
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// User object
    /// </summary>
    public ApplicationUser ApplicationUser { get; set; }
}