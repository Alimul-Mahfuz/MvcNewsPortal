using NewsPortal.Application.DTOs;

namespace NewsPortal.Application.Interfaces;
public interface IFileService
{
    Task<string> UploadFileAsync(AppFile  file, string modelName);
    string GetFolderPath(string modelName);
}