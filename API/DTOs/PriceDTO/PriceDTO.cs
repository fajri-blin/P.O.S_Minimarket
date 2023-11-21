using API.Models.Entities;

namespace API.DTOs.PriceDTO;

public class PriceDTO
{
    public Guid Guid { get; set; }
    public Guid? ProductGuid { get; set; }
    public Guid? UnitGuid { get; set; }
    public decimal Amount { get; set; }

    public static explicit operator PriceDTO(Price price)
    {
        return new PriceDTO
        {
            Guid = price.Guid,
            ProductGuid = price.ProductGuid,
            UnitGuid = price.UnitGuid,
            Amount = price.Amount,
        };
    }

    public static explicit operator Price(PriceDTO price)
    {
        return new Price
        {
            Guid = price.Guid,
            Amount = price.Amount
        };
    }
}
