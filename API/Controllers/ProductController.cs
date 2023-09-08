using API.DTOs.ProductDTO;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

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
        if (list == null) return NotFound();

        return Ok(list);
    }

    [HttpPost]
    public IActionResult Create(NewProductDTO productDTO)
    {
        var created = _productService.Create(productDTO);
        if (created == null) return BadRequest();

        return Ok(created);
    }

    [HttpDelete("Delete")]
    public IActionResult Delete(Guid guid) 
    {
        var delete = _productService.Delete(guid);
        switch (delete)
        {
            case -1: return BadRequest();
            case 0: return NotFound();
        }
        return Ok(delete);
    }
}
