using API.Contract.Entities;
using API.Data;

namespace API.Services;

public class PriceService
{
    private readonly IPriceRepository _priceRepository;
    private readonly IProductRepository _productRepository;
    private readonly PosDbContext _posDbContext;

    public PriceService(IPriceRepository priceRepository, IProductRepository productRepository, PosDbContext posDbContext)
    {
        _priceRepository = priceRepository;
        _productRepository = productRepository;
        _posDbContext = posDbContext;
    }
}
