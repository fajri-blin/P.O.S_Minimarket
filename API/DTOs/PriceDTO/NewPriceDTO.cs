﻿using API.Models.Entities;

namespace API.DTOs.PriceDTO;

public class NewPriceDTO
{
    public Guid ProductGuid { get; set; }
    public String UnitName { get; set; }
    public decimal Amount { get; set; }

    public static explicit operator Price(NewPriceDTO newPriceDTO)
    {
        return new Price
        {
            Guid = Guid.NewGuid(),
            ProductGuid = newPriceDTO.ProductGuid,
            Ammount = newPriceDTO.Amount
        };
    }

}
