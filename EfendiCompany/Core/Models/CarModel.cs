using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CarModel
{
    public int Id { get; set; }
    public string Name { get; set; };
    public int BrandId { get; set; }
    public int? StartYear { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedDate { get; set; }
}
