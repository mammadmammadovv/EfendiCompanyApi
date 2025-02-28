using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileDefinitionController(IImageService _service, IWebHostEnvironment _webHost) : ControllerBase
    {
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm] List<IFormFile> files)
        {
            CheckFiles(files);
            var res = _service.Post(files.First(), _webHost.WebRootPath);
            return Ok(res);
        }

        private static void CheckFiles(List<IFormFile>? files)
        {
            if (files is not null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    if (file == null || file.Length == 0)
                        throw new BadRequestException("File is empty or not uploaded.");

                    var allowedExtensions = new[] { ".png", ".jpg", ".jpeg", ".pdf", ".txt" };
                    var extension = Path.GetExtension(file.FileName).ToLower();

                    if (!allowedExtensions.Contains(extension))
                        throw new BadRequestException("Unsupported file type.");
                }
            }
        }
    }
}
