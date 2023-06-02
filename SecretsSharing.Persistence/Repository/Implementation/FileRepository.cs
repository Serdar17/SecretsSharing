using Domain.Models;
using Microsoft.EntityFrameworkCore;
using SecretsSharing.Persistence.Data;
using SecretsSharing.Persistence.Repository.Interfaces;

namespace SecretsSharing.Persistence.Repository.Implementation;

public class FileRepository : IFileRepository
{
    private readonly ApplicationDbContext _dbContext;

    public FileRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> AddAsync(FileDetails fileDetails, CancellationToken cancellationToken = default)
    {
        await _dbContext.AddAsync(fileDetails, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return fileDetails.Id;
    }

    public async Task<FileDetails?> GetFileByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Files
            .Where(x => x.Id.Equals(id))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task DeleteAsync(FileDetails fileDetails, CancellationToken cancellationToken = default)
    {
        _dbContext.Files.Remove(fileDetails);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}