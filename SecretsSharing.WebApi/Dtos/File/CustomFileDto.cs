namespace WebApi.Dtos.File;

public class CustomFileDto
{
    public CustomFileDto(byte[] fileContents, string contentType, string fileName)
    {
        FileContents = fileContents;
        ContentType = contentType;
        FileName = fileName;
    }
    
    public byte[] FileContents { get; set; }
    
    public string ContentType { get; set; }
    
    public string FileName { get; set; }
}