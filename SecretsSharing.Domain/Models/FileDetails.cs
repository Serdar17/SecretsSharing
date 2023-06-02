using Domain.Enums;
using Domain.Primitives;

namespace Domain.Models;

public class FileDetails : BaseEntity<Guid>
{
    public string FileName { get; set; }
    
    public FileType FileType { get; set; }

    public string Path { get; set; }

    public bool DeleteCascade { get; set; }

    public Guid UserId { get; set; }
    
    public ApplicationUser ApplicationUser { get; set; }
}