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

    [HttpPost]
    public IActionResult Create(NewProductDTO productDTO)
    {
        var created = _productService.Create(productDTO);
        if (created == null) return BadRequest();

        return Ok(created);
    }
}
