using Ardalis.Result;
using Domain.Models;
using WebApi.Dtos.File;

namespace WebApi.Services.Interfaces;

/// <summary>
/// interface for interacting with files
/// </summary>
public interface IFileService
{
    /// <summary>
    /// method for uploading a file
    /// </summary>
    /// <param name="uploadFileDto">downloadable file</param>
    /// <param name="userId">Unique user id</param>
    /// <param name="cancellationToken">Cancellation token with default value</param>
    /// <returns>generated unique file id</returns>
    Task<Result<Guid>> UploadFileAsync(UploadFileDto uploadFileDto, Guid userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// method for downloading a file
    /// </summary>
    /// <param name="id">unique file id</param>
    /// <param name="cancellationToken">Cancellation token with default value</param>
    /// <returns>Model with file data</returns>
    Task<Result<CustomFileDto>> DownloadFileAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// method for uploading a text file
    /// </summary>
    /// <param name="uploadTextFileDto">object with text for upload</param>
    /// <param name="userId">unique user id</param>
    /// <param name="cancellationToken">Cancellation token with default value</param>
    /// <returns>generated unique text file id</returns>
    Task<Result<Guid>> UploadTextFileAsync(UploadTextFileDto uploadTextFileDto, Guid userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// method for returning all files of the current user
    /// </summary>
    /// <param name="userId">unique user id</param>
    /// <param name="cancellationToken">Cancellation token with default value</param>
    /// <returns>list of all user files</returns>
    Task<Result<IEnumerable<Guid>>> GetUserFilesAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// method for deleting a file
    /// </summary>
    /// <param name="fileDetails">file details for deleting</param>
    /// <param name="cancellationToken">Cancellation token with default value</param>
    /// <returns></returns>
    Task DeleteFileAsync(FileDetails fileDetails, CancellationToken cancellationToken = default);

    /// <summary>
    /// method for deleting a file by id
    /// </summary>
    /// <param name="id">unique file id</param>
    /// <param name="cancellationToken">Cancellation token with default value</param>
    /// <returns></returns>
    Task<Result> DeleteFileByIdAsync(Guid id, CancellationToken cancellationToken = default);
}