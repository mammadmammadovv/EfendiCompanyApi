using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SparePart
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ModelId { get; set; }
    public string? PartNumber { get; set; }
    public decimal Price { get; set; }
    public int InStock { get; set; }
    public string ImageUrl { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedDate { get; set; }
}
