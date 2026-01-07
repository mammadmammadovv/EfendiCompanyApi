namespace Core.Models;

public class CarModelImage
{
    public int Id { get; set; }
    public int CarModelId { get; set; }
    public string ImageUrl { get; set; }
    public int? DisplayOrder { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedDate { get; set; }
}

