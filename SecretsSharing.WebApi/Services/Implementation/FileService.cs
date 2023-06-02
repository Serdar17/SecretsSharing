using Ardalis.Result;
using Domain.Enums;
using Domain.Models;
using SecretsSharing.Persistence.Repository.Interfaces;
using WebApi.Dtos.File;
using WebApi.Services.Interfaces;

namespace WebApi.Services.Implementation;

public class FileService : IFileService
{
    private readonly IFileRepository _fileRepository;
    private readonly IUserRepository _userRepository;

    public FileService(IFileRepository fileRepository, 
        IUserRepository userRepository)
    {
        _fileRepository = fileRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<Guid>> UploadFileAsync(UploadFileDto uploadFileDto, Guid userId,
        CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetUserByIdAsync(userId, cancellationToken);
        
        var fileName = Path.GetFileName(uploadFileDto.File.FileName);

        var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "upload/files");
        CreateDirectoryIfNotExists(rootPath);

        var filePath = Path.Combine(rootPath, fileName);

        await using var localFile = File.OpenWrite(filePath);

        await using var uploadedFile = uploadFileDto.File.OpenReadStream();
        
        await uploadedFile.CopyToAsync(localFile, cancellationToken);

        var fileDetails = new FileDetails()
        {
            FileName = fileName,
            Path = filePath,
            FileType = FileType.File,
            DeleteCascade = uploadFileDto.DeleteCascade,
            UserId = user.Id,
            ApplicationUser = user
        };
        
        var id = await _fileRepository.AddAsync(fileDetails, cancellationToken);

        return id;
    }

    public async Task<Result<CustomFileDto>> DownloadFileAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var fileDetails = await _fileRepository.GetFileByIdAsync(id, cancellationToken);

        if (fileDetails is null)
        {
            return Result.Error($"File with id={id} not found");
        }

        if (!File.Exists(fileDetails.Path))
        {
            await _fileRepository.DeleteAsync(fileDetails, cancellationToken);
            return Result.Error($"File with id={id} not found");
        }
        
        var memory = new MemoryStream();
        
        await using (var stream = new FileStream(fileDetails.Path, FileMode.Open))
        {
            await stream.CopyToAsync(memory, cancellationToken);
        }
        
        memory.Position = 0;

        if (fileDetails.DeleteCascade)
            await DeleteFileAsync(fileDetails, cancellationToken);
        
        return new CustomFileDto(memory.GetBuffer(), GetContentType(fileDetails.Path),
            Path.GetFileName(fileDetails.Path));
        
    }

    public async Task<Result<Guid>> UploadTextFileAsync(UploadTextFileDto uploadTextFileDto, Guid userId, 
        CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetUserByIdAsync(userId, cancellationToken);
        
        var fileName = Path.Combine($"{Path.GetRandomFileName()}.txt");

        var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "upload/texts");
        CreateDirectoryIfNotExists(rootPath);

        var filePath = Path.Combine(rootPath, fileName);
        
        await using var outputFile = new StreamWriter(filePath);
        
        await outputFile.WriteAsync(uploadTextFileDto.Text);
        
        var fileDetails = new FileDetails()
        {
            FileName = fileName,
            Path = filePath,
            FileType = FileType.TextFile,
            DeleteCascade = uploadTextFileDto.DeleteCascade,
            UserId = user.Id,
            ApplicationUser = user
        };
        
        var id = await _fileRepository.AddAsync(fileDetails, cancellationToken);

        return id;
    }

    public async Task<Result<IEnumerable<Guid>>> GetUserFilesAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetUserByIdAsync(userId, cancellationToken);

        if (user is null)
        {
            return Result.Error($"User with id={userId} not found");
        }

        return Result.Success(user.Files.Select(x => x.Id));
    }

    public async Task DeleteFileAsync(FileDetails fileDetails, CancellationToken cancellationToken = default)
    {
        var file = new FileInfo(fileDetails.Path);
        if (file.Exists)
            file.Delete();

        await _fileRepository.DeleteAsync(fileDetails, cancellationToken);
    }

    public async Task<Result> DeleteFileByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var file = await _fileRepository.GetFileByIdAsync(id, cancellationToken);

        if (file is null)
        {
            return Result.Error($"File with id={id} not exists");
        }

        await DeleteFileAsync(file, cancellationToken);
        
        return Result.Success();
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="rootPath"></param>
    private static void CreateDirectoryIfNotExists(string rootPath)
    {
        if (!Directory.Exists(rootPath))
        {
            Directory.CreateDirectory(rootPath);
        }
    }

    /// <summary>
    /// method for getting file content type
    /// </summary>
    /// <param name="path">File path</param>
    /// <returns>Content type as string</returns>
    private string GetContentType(string path)
    {
        var types = GetMimeTypes();
        var ext = Path.GetExtension(path).ToLowerInvariant();
        return types[ext];
    }
    
    /// <summary>
    /// method for getting all mime types
    /// </summary>
    /// <returns></returns>
    private Dictionary<string, string> GetMimeTypes()
    {
        return new Dictionary<string, string>
        {
            {".txt", "text/plain"},
            {".pdf", "application/pdf"},
            {".doc", "application/vnd.ms-word"},
            {".docx", "application/vnd.ms-word"},
            {".xls", "application/vnd.ms-excel"},
            {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
            {".png", "image/png"},
            {".jpg", "image/jpeg"},
            {".jpeg", "image/jpeg"},
            {".gif", "image/gif"},
            {".csv", "text/csv"}
        };
    }
}