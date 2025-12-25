namespace Core.Models;

public class CarModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BrandId { get; set; }
    public DateTime? StartYear { get; set; }
    public string LogoUrl { get; set; }
    public string? Condition { get; set; }
    public string? FuelType { get; set; }
    public string? EnginePower { get; set; }
    public string? StartingPrice { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedDate { get; set; }
}
