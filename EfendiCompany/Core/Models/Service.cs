namespace Core.Models;

public class Service
{
    public Service()
    {
        
    }
    public Service(string title, string description, string fullDescription, string imageUrl, string imageDetailsUrl)
    {
        Title = title;
        Description = description;
        FullDescription = fullDescription;
        ImageUrl = imageUrl;
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string FullDescription { get; set; }
    public string ImageUrl { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedDate { get; set; }
}
