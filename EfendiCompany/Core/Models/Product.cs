namespace Core.Models;

public class Product
{
    public int Id { get; set; }
    public int ParentProductId { get; set; }
    public string? ParentProductName { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedDate { get; set; }
}

