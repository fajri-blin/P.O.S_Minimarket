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
        if (list == null) return NotFound(new ResponseHandlers<ProductDTO>
        {
            Code = StatusCodes.Status404NotFound,
            Status = HttpStatusCode.NotFound.ToString(),
            Message = "Data Not Found"
        });

        return Ok(new ResponseHandlers<IEnumerable<ProductDTO>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data Found",
            Data = list
        });
    }

    [HttpPost]
    public IActionResult Create(NewProductDTO productDTO)
    {
        var created = _productService.Create(productDTO);
        if (created == null) return BadRequest(new ResponseHandlers<ProductDTO>
        {
            Code = StatusCodes.Status400BadRequest,
            Status = HttpStatusCode.BadRequest.ToString(),
            Message = "Bad Connections, Data Failed to Add"
        });

        return Ok(new ResponseHandlers<ProductDTO>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully Added",
            Data = created
        });
    }

    [HttpDelete("Delete")]
    public IActionResult Delete(Guid guid) 
    {
        var delete = _productService.Delete(guid);
        switch (delete)
        {
            case -1: return BadRequest(new ResponseHandlers<ProductDTO>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Bad Connections, Data Failed to Delete"
            });
            case 0: return NotFound(new ResponseHandlers<ProductDTO>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data that want to delete is not found"
            });
        }
        return Ok(new ResponseHandlers<int>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully delete the data",
            Data = delete
        });
    }
}
