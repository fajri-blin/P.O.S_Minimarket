using API.Contract.Entities;
using API.Data;
using API.Model.Entities;
using API.Models.Entities;

namespace API.Repository.Entities;

public class ProductRepository : GeneralRepository<Product>, IProductRepository
{
    public ProductRepository(PosDbContext posDbContext) : base(posDbContext)
    {
    }
}
