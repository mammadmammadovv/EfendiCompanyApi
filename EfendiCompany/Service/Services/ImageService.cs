using Core.Constants;
using Microsoft.AspNetCore.Http;

namespace Services.Services;

public interface IImageService
{
    public string Post(IFormFile file, string webRootPath);
}

public class ImageService : IImageService
{
    public string Post(IFormFile file, string webRootPath)
    {
        string path = "";
        string filePath = "";
        if (file is not null)
        {
            string fileName = $"{Guid.NewGuid().ToString()}" + file.FileName;
            fileName = fileName.Replace(" ", "_").Replace(":", ".").Replace("-", "_").Replace(@"\", "/");
            path = Path.Combine(webRootPath, "Pictures", fileName);

            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            filePath = $"Pictures/{fileName}";
            string host = Statics.BaseUrl;
            filePath = @$"{host}/{filePath}";
        }

        return filePath;
    }
}
