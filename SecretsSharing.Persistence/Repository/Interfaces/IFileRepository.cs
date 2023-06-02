using Domain.Models;

namespace SecretsSharing.Persistence.Repository.Interfaces;

public interface IFileRepository
{
    Task<Guid> AddAsync(FileDetails fileDetails, CancellationToken cancellationToken = default);

    Task<FileDetails?> GetFileByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task DeleteAsync(FileDetails fileDetails, CancellationToken cancellationToken = default);
}