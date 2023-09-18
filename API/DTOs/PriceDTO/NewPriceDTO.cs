namespace API.DTOs.PriceDTO;

public class NewPriceDTO
{
    public Guid ProductGuid { get; set; }
    public string Unit { get; set; }
    public float Price { get; set; }

}
