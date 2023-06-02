using Domain.Models;

namespace SecretsSharing.Persistence.Repository.Interfaces;

/// <summary>
/// interface for executing database queries
/// </summary>
public interface IFileRepository
{
    /// <summary>
    /// method for adding file details to the database
    /// </summary>
    /// <param name="fileDetails">Object with file details</param>
    /// <param name="cancellationToken">Cancellation token with default value</param>
    /// <returns>returns the unique identifier of the file</returns>
    Task<Guid> AddAsync(FileDetails fileDetails, CancellationToken cancellationToken = default);

    /// <summary>
    /// method for file search by unique id
    /// </summary>
    /// <param name="id">Unique file id</param>
    /// <param name="cancellationToken">Cancellation token with default value</param>
    /// <returns>Returns the found file details object</returns>
    Task<FileDetails?> GetFileByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// method for deleting file details from database
    /// </summary>
    /// <param name="fileDetails">object to delete</param>
    /// <param name="cancellationToken">Cancellation token with default value</param>
    /// <returns></returns>
    Task DeleteAsync(FileDetails fileDetails, CancellationToken cancellationToken = default);
}