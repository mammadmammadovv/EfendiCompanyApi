namespace Core.Models;

public class Language
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
}

