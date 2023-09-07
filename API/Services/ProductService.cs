using API.Contract.Entities;
using API.Data;
using API.DTOs.ProductDTO;
using API.Model.Entities;

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

    public ProductDTO? Create(NewProductDTO newProductDTO)
    {
        using(var transactions = _posDbContext.Database.BeginTransaction())
        {
            try
            {
                var created = _productRepository.Create((Product)newProductDTO);
                if (created == null) return null;
                transactions.Commit();
                return (ProductDTO)created;
            }
            catch
            {
                return null;
                transactions.Rollback();
            }
        }
    }
}
