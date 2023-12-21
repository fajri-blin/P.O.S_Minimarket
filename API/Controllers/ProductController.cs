using API.DTOs.ProductDTO;
using API.Services;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var list = _productService.Get();
        if (list == null) return NotFound(new ResponseHandler<ProductDTO>
        {
            Code = StatusCodes.Status404NotFound,
            Status = HttpStatusCode.NotFound.ToString(),
            Message = "Data Not Found"
        });

        return Ok(new ResponseHandler<IEnumerable<ProductDTO>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Found",
            Data = list
        });
    }
    
    [HttpGet("Barcode/{barcodeID}")]
    public IActionResult Get(string barcodeID)
    {
        var product = _productService.Get(barcodeID);
        if (product == null) return NotFound(new ResponseHandler<ProductDTO>
        {
            Code = StatusCodes.Status404NotFound,
            Status = HttpStatusCode.NotFound.ToString(),
            Message = "Data not found"
        });

        return Ok(new ResponseHandler<ProductDTO>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Found",
            Data = product
        });
    }

    [HttpGet("{guid}/")]
    public IActionResult Get(Guid guid)
    {
        var product = _productService.Get(guid);
        if (product == null) return NotFound(new ResponseHandler<ProductDTO>
        {
            Code = StatusCodes.Status404NotFound,
            Status = HttpStatusCode.NotFound.ToString(),
            Message = "Data not found"
        });

        return Ok(new ResponseHandler<ProductDTO>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data found",
            Data = product
        });
    }

    [HttpPost("Create")]
    public IActionResult Create(NewProductDTO productDTO)
    {
        var created = _productService.Create(productDTO);
        if (created == 0) return BadRequest(new ResponseHandler<ProductDTO>
        {
            Code = StatusCodes.Status400BadRequest,
            Status = HttpStatusCode.BadRequest.ToString(),
            Message = "Bad Connections, Data Failed to Add"
        });

        return Ok(new ResponseHandler<ProductDTO>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully Added",
        });
    }

    [HttpPut("Update")]
    public IActionResult Update(ProductDTO productDTO)
    {
        var isUpdated = _productService.Edit(productDTO);
        switch(isUpdated)
        {
            case 0: return BadRequest(new ResponseHandler<ProductDTO>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Bad Connections, Data Failed to Update"
            });
            case -1: return NotFound(new ResponseHandler<ProductDTO>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data that want to update is not found"
            });
            case 1: return Ok(new ResponseHandler<ProductDTO>{
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Successfully Updated",
            });
            default: return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<ProductDTO>{
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Internal Server Error"
            });
        }
    }

    [HttpDelete("Delete")]
    public IActionResult Delete(Guid guid) 
    {
        var delete = _productService.Delete(guid);
        switch (delete)
        {
            case -1: return BadRequest(new ResponseHandler<ProductDTO>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Bad Connections, Data Failed to Delete"
            });
            case 0: return NotFound(new ResponseHandler<ProductDTO>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data that want to delete is not found"
            });
        }
        return Ok(new ResponseHandler<ProductDTO>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully deleted the data",
        });
    }
}
