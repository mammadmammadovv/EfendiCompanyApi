namespace Core.Models;

public class Service
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string FullDescription { get; set; }
    public string ImageUrl { get; set; }
    public string ImageDetailsUrl { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedDate { get; set; }
}
