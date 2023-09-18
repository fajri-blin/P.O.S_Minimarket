using API.Contract.Entities;
using API.Data;
using API.DTOs.PriceDTO;
using API.DTOs.ProductDTO;

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

    public IEnumerable<PriceDTO> GetAll() 
    {
        var listPrice = _priceRepository.GetAll();
        if (listPrice == null || !listPrice.Any()) return null;

        var dto = listPrice.Select(price => (PriceDTO)price);
        return dto;
    }
}
