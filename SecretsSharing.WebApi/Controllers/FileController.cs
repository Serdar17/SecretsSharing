using AutoMapper;
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

    public FileController(IMapper mapper, IFileService fileService)
    {
        _fileService = fileService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uploadFileDto"></param>
    /// <returns></returns>
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
    /// 
    /// </summary>
    /// <param name="uploadTextFileDto"></param>
    /// <returns></returns>
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
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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