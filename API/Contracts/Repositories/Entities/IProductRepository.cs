﻿using API.Contracts.Repositories;
using API.Model.Entities;

namespace API.Contracts.Repositories.Entities;

public interface IProductRepository : IGeneralRepository<Product>
{
    Product? GetByBarcode(string barcode);
    bool UniqueBarcode(string barcode);

}
