using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace SecretsSharing.Persistence.Interfaces;

public interface IApplicationDbContext
{
    DbSet<ApplicationUser> Users { get; set; }
    
    DbSet<FileDetails> Files { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}