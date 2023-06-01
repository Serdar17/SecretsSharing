using Domain.Models;
using Microsoft.EntityFrameworkCore;
using File = Domain.Models.File;

namespace SecretsSharing.Persistence.Interfaces;

public interface IApplicationDbContext
{
    DbSet<ApplicationUser> Users { get; set; }
    
    DbSet<File> Files { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}