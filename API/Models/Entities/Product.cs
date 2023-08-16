using API.Models;

namespace API.Model.Entities;

public class Product : BaseEntity
{
    public string BarcodeID { get; set; }
    public string Title { get; set; }
}
