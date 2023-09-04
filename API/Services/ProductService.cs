using API.Contract.Entities;
using API.Data;

namespace API.Services;

public class ProductService
{
    private readonly IProductRepository _productRepository;
    private readonly PosDbContext _posDbContext;

    public ProductService(IProductRepository productRepository, PosDbContext posDbContext)
    {
        _productRepository = productRepository;
        _posDbContext = posDbContext;
    }
}
