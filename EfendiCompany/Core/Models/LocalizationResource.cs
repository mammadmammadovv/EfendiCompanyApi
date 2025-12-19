namespace Core.Models;

public class LocalizationResource
{
    public int Id { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
    public string LanguageCode { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; }
}

