using Microsoft.AspNetCore.Http;

namespace Core.DTO.Services;

public class CreateServiceModel
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? FullDescription { get; set; }
    public IFormFile? Image { get; set; }
    public IFormFile? ImageDetails { get; set; }
}
