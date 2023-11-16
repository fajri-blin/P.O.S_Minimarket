﻿using API.Model.Entities;

namespace API.Contract.Entities;

public interface IProductRepository : IGeneralRepository<Product>
{
    Product? GetByBarcode(string barcode);
}
