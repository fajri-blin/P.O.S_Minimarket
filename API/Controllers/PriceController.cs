using API.DTOs.PriceDTO;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class PriceController : ControllerBase
{
    private readonly PriceService _priceService;

    public PriceController(PriceService priceService)
    {
        _priceService = priceService;
    }

    [HttpGet]
    public IActionResult GetPrice()
    {
        var listPrice = _priceService.GetAll();
        if(listPrice == null) return NotFound();

        return Ok(listPrice);
    }

    [HttpPost("AddPrice/")]
    public IActionResult AddPrice(NewPriceDTO newPriceDTO)
    {
        var created = _priceService.Create(newPriceDTO);
        if(created != null)
        {
            return Ok(created);
        }
        return NotFound();
    }
}
