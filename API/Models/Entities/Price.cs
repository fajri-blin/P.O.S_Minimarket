namespace API.Models.Entities;

public class Price
{
    public Guid ProductGuid { get; set; }
    public string UnitGuid { get; set; }
    public decimal Ammount { get; set; }
}
