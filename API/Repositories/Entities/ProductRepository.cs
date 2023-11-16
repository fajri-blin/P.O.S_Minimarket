using API.Contract.Entities;
using API.Data;
using API.Model.Entities;
using API.Models.Entities;
using System.Security.Cryptography.X509Certificates;

namespace API.Repository.Entities;

public class ProductRepository : GeneralRepository<Product>, IProductRepository
{
    public ProductRepository(PosDbContext posDbContext) : base(posDbContext)
    {
    }

    public Product GetByBarcode(string barcode)
    {
        return _posDbContext.Set<Product>().FirstOrDefault(product => product.BarcodeID == barcode);
    }
}
