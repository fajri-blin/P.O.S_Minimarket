using API.Contracts.Repositories.Entities;
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

    public IEnumerable<ProductDTO>? Get()
    {
        var list = _productRepository.GetAll();
        if (list == null || !list.Any()) return null;

        var dto=list.Select(product => (ProductDTO)product);
        return dto;
    }

    public ProductDTO? Get(string barcodeID)
    {
        var product = _productRepository.GetByBarcode(barcodeID);
        if (product == null) return null;

        var dto = (ProductDTO)product;
        return dto;
    }

    public ProductDTO? Get(Guid guid)
    {
        var product = _productRepository.GetByGuid(guid);
        if (product == null) return null;
        
        var dto = (ProductDTO)product;
        return dto;
    }

    public int Delete(Guid guid)
    {
        var getEntity = _productRepository.GetByGuid(guid);
        if (getEntity == null) return -1;

        var delete = _productRepository.Delete(getEntity);
        if (!delete) return 0;

        return 1;
    }

    public int Edit(ProductDTO product)
    {
        using(var transaction = _posDbContext.Database.BeginTransaction())
        {
            try
            {
                var IsExits = _productRepository.IsExits(product.Guid);
                if(!IsExits) return -1;

                var edit = _productRepository.Update((Product)product);
                if(!edit) return 0;

                transaction.Commit();
                return 1;

            }catch
            {
                transaction.Rollback();
                return 0;
            }
        }
    }

    public int Create(NewProductDTO newProductDTO)
    {
        using(var transactions = _posDbContext.Database.BeginTransaction())
        {
            try
            {
                var created = _productRepository.Create((Product)newProductDTO);
                if (created == null) return 0;
                transactions.Commit();
                return 1;
            }
            catch
            {
                transactions.Rollback();
                return 1;
            }
        }
    }
}
