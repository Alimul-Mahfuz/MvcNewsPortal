namespace NewsPortal.Application.DTOs;

public class AppFile
{
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public byte[] Content { get; set; }
}