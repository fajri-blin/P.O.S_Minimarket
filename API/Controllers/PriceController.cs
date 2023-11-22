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
        if (listPrice == null) return NotFound(new ResponseHandler<PriceDTO>
        {
            Code = StatusCodes.Status404NotFound,
            Status = HttpStatusCode.NotFound.ToString(),
            Message = "Data Not Found"
        });

        return Ok(new ResponseHandler<IEnumerable<PriceDTO>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Found",
            Data = listPrice
        });
    }
    [HttpGet("{guid}")]
    public IActionResult GetPrice(Guid guid)
    {
        var price = _priceService.GetPrice(guid);
        if (price == null) return NotFound(new ResponseHandler<PriceDTO>
        {
            Code = StatusCodes.Status404NotFound,
            Status = HttpStatusCode.NotFound.ToString(),
            Message = "Data Not Found"
        });

        return Ok(new ResponseHandler<PriceDTO>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Found",
            Data = price
        });
    }

    [HttpPost("AddPrice/")]
    public IActionResult AddPrice(NewPriceDTO newPriceDTO)
    {
        var created = _priceService.Create(newPriceDTO);
        if(created != null)
        {
            return Ok(new ResponseHandler<PriceDTO>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Successfully Created",
                Data = created
            });
        }
        return NotFound(new ResponseHandler<ProductDTO>
        {
            Code = StatusCodes.Status404NotFound,
            Status = HttpStatusCode.NotFound.ToString(),
            Message = "Data Failed to created"
        });
    }

    [HttpPut("UpdatePrice/")]
    public IActionResult UpdatePrice(PriceDTO price)
    {
        var updated = _priceService.Edit(price);
        if (updated == false) return NotFound(new ResponseHandler<bool>
        {
            Code = StatusCodes.Status404NotFound,
            Status = HttpStatusCode.NotFound.ToString(),
            Message = "Data Failed to Updated",
            Data = updated
        });

        return Ok(new ResponseHandler<bool>
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
        if(deleted == -1) return BadRequest(new ResponseHandler<PriceDTO>
        {
            Code = StatusCodes.Status400BadRequest,
            Status = HttpStatusCode.BadRequest.ToString(),
            Message = "Bad Connections, Data Failed to Delete"
        });
        if(deleted != 0) return NotFound(new ResponseHandler<PriceDTO>
        {
            Code = StatusCodes.Status404NotFound,
            Status = HttpStatusCode.NotFound.ToString(),
            Message = "Data that want to delete is not found"
        });
        
        return Ok(new ResponseHandler<int>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully delete the data",
        });
    }
}
