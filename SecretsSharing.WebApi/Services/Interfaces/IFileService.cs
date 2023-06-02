using Ardalis.Result;
using Domain.Models;
using WebApi.Dtos.File;

namespace WebApi.Services.Interfaces;

public interface IFileService
{
    Task<Result<Guid>> UploadFileAsync(UploadFileDto uploadFileDto, Guid userId,
        CancellationToken cancellationToken = default);

    Task<Result<CustomFileDto>> DownloadFileAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Result<Guid>> UploadTextFileAsync(UploadTextFileDto uploadTextFileDto, Guid userId,
        CancellationToken cancellationToken = default);

    Task<Result<IEnumerable<Guid>>> GetUserFilesAsync(Guid userId, CancellationToken cancellationToken = default);

    Task DeleteFileAsync(FileDetails fileDetails, CancellationToken cancellationToken = default);

    Task<Result> DeleteFileByIdAsync(Guid id, CancellationToken cancellationToken = default);
}