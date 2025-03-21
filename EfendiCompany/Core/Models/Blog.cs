namespace Core.Models;

public class Blog
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string DetailDescription { get; set; }
    public string ImageUrl { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsDeleted { get; set; }
}

