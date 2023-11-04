using API.Models.Entities;

namespace API.DTOs.PriceDTO;

public class PriceDTO
{
    public Guid Guid { get; set; }
    public Guid? ProductGuid { get; set; }
    public Guid? UnitGuid { get; set; }
    public decimal Ammount { get; set; }

    public static explicit operator PriceDTO(Price price)
    {
        return new PriceDTO
        {
            Guid = price.Guid,
            ProductGuid = price.ProductGuid,
            UnitGuid = price.UnitGuid,
            Ammount = price.Ammount,
        };
    }

    public static explicit operator Price(PriceDTO price)
    {
        return new Price
        {
            Guid = price.Guid,
            Ammount = price.Ammount
        };
    }
}
