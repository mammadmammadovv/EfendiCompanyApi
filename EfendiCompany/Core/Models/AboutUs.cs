namespace Core.Models;

public class AboutUs
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? MissionTitle { get; set; }
    public string? MissionDescription { get; set; }
    public string? MissionImageUrl { get; set; }
    public string? VisionTitle { get; set; }
    public string? VisionDescription { get; set; }
    public string? VisionImageUrl { get; set; }
    public string? WhyUsTitle { get; set; }
    public string? WhyUsDescription { get; set; }
    public string? WhyUsImageUrl { get; set; }
    public string? TeamMembers { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedDate { get; set; }
}
