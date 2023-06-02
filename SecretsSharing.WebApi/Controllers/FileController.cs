using Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos.File;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class FileController : ApiController
{
    private readonly IFileService _fileService;

    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }

    /// <summary>
    /// Endpoint for upload file
    /// </summary>
    /// <param name="uploadFileDto">The model that contains the file</param>
    /// <returns>Uri to the file</returns>
    [HttpPost("upload-file")]
    [ProducesResponseType(typeof(Uri), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UploadFileAsync([FromForm] UploadFileDto uploadFileDto)
    {
        var user = (ApplicationUser)HttpContext.Items["User"]!;

        var result = await _fileService.UploadFileAsync(uploadFileDto, user.Id);

        if (result.IsSuccess)
        {
            return Ok(new Uri($"{Request.Scheme}://{Request.Host}/api/file/{result.Value}"));
        }
       
        return BadRequest("Something went wrong");
    }

    /// <summary>
    /// Endpoint for upload text file
    /// </summary>
    /// <param name="uploadTextFileDto">The model that contains the text as string</param>
    /// <returns>Uri to the text file</returns>
    [HttpPost("upload-text")]
    [ProducesResponseType(typeof(Uri), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UploadTextFileAsync([FromBody] UploadTextFileDto uploadTextFileDto)
    {
        var user = (ApplicationUser)HttpContext.Items["User"]!;
        
        var result = await _fileService.UploadTextFileAsync(uploadTextFileDto, user.Id);

        if (result.IsSuccess)
        {
            return Ok(new Uri($"{Request.Scheme}://{Request.Host}/api/file/{result.Value}"));
        }
        
        return BadRequest("Something went wrong");
    }
    
    /// <summary>
    /// Endpoint for download file by id
    /// </summary>
    /// <param name="id">Unique id of the uploaded file</param>
    /// <returns>The required file to upload</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(File), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    public async Task<IActionResult> DownloadFileAsync([FromRoute] Guid id)
    {
        var result = await _fileService.DownloadFileAsync(id);
        
        if (!result.IsSuccess)
            return NotFound(result.Errors);

        var fileContent = result.Value;

        return File(fileContent.FileContents, fileContent.ContentType, fileContent.FileName);
    }

    /// <summary>
    /// Endpoint to get all files of the current user
    /// </summary>
    /// <param name="id">Unique user id</param>
    /// <returns>List of uri with all the files of the current user</returns>
    [HttpGet("get-files/{id:guid}")]
    [ProducesResponseType(typeof(IEnumerable<Uri>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserFilesAsync([FromRoute] Guid id)
    {
        var result = await _fileService.GetUserFilesAsync(id);

        if (!result.IsSuccess)
        {
            return NotFound(result.Errors);
        }

        var paths = result.Value
            .Select(x => new Uri($"{Request.Scheme}://{Request.Host}/api/file/{x}"));

        return Ok(paths);
    }

    /// <summary>
    /// Endpoint
    /// </summary>
    /// <param name="id">Unique id the file</param>
    /// <returns>No content if the file is deleted successfully</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteFileAsync([FromRoute] Guid id)
    {
        var result = await _fileService.DeleteFileByIdAsync(id);

        if (result.IsSuccess)
            return NoContent();

        return NotFound(result.Errors);
    }
}