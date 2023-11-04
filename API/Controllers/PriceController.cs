using API.DTOs.PriceDTO;
using API.DTOs.ProductDTO;
using API.Services;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

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
        if(listPrice == null) return NotFound(new ResponseHandlers<PriceDTO>
        {
            Code = StatusCodes.Status404NotFound,
            Status = HttpStatusCode.NotFound.ToString(),
            Message = "Data Not Found"
        });

        return Ok(new ResponseHandlers<IEnumerable<PriceDTO>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Found",
            Data = listPrice
        });
    }

    [HttpPost("AddPrice/")]
    public IActionResult AddPrice(NewPriceDTO newPriceDTO)
    {
        var created = _priceService.Create(newPriceDTO);
        if(created != null)
        {
            return Ok(new ResponseHandlers<PriceDTO>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Successfully Created",
                Data = created
            });
        }
        return NotFound(new ResponseHandlers<ProductDTO>
        {
            Code = StatusCodes.Status404NotFound,
            Status = HttpStatusCode.NotFound.ToString(),
            Message = "Data Not Found"
        });
    }

    [HttpPut("UpdatePrice/")]
    public IActionResult UpdatePrice(PriceDTO price)
    {
        var updated = _priceService.Edit(price);
        if (updated == false) return NotFound(new ResponseHandlers<bool>
        {
            Code = StatusCodes.Status404NotFound,
            Status = HttpStatusCode.NotFound.ToString(),
            Message = "Data Not Found",
            Data = updated
        });

        return Ok(new ResponseHandlers<bool>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully Updated",
            Data = updated
        });
    }

    [HttpDelete("DeletePrice/{guid}")]
    public IActionResult DeletePrice(Guid guid) 
    {
        var deleted = _priceService.Delete(guid);
        if(deleted == -1) return BadRequest(new ResponseHandlers<PriceDTO>
        {
            Code = StatusCodes.Status400BadRequest,
            Status = HttpStatusCode.BadRequest.ToString(),
            Message = "Bad Connections, Data Failed to Delete"
        });
        if(deleted != 0) return NotFound(new ResponseHandlers<PriceDTO>
        {
            Code = StatusCodes.Status404NotFound,
            Status = HttpStatusCode.NotFound.ToString(),
            Message = "Data that want to delete is not found"
        });
        
        return Ok(new ResponseHandlers<int>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully delete the data",
            Data = deleted
        });
    }
}
