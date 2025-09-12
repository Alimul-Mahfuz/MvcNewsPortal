using NewsPortal.Application.DTOs;

namespace NewsPortal.Application.Interfaces;

public class FileServiceImpl : IFileService
{
    public Task<string> UploadFileAsync(AppFile file, string modelName)
    {
        string timeStamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
        string fileName = $"{timeStamp}_{file.FileName}";
        string filePath = Path.Combine(GetFolderPath(modelName), fileName);
        File.WriteAllBytes(filePath, file.Content);
        return Task.FromResult<string>(fileName);
    }

    private readonly Dictionary<string, string> _modelFolderMap = new()
    {
        { "Article", "uploads/articles" },
        { "User", "uploads/users" },
    };

    public string GetFolderPath(string modelName)
    {
        if (_modelFolderMap.TryGetValue(modelName, out var folder))
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folder);
        }

        return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
    }
}