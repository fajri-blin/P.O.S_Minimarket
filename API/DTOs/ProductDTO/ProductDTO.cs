using API.Model.Entities;

namespace API.DTOs.ProductDTO;

public class ProductDTO
{
    public Guid Guid { get; set; }
    public string BarcodeId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;


    public static explicit operator ProductDTO(Product product)
    {
        return new ProductDTO
        {
            Guid = product.Guid,
            BarcodeId = product.BarcodeID,
            Title = product.Title,
        };
    }

    public static explicit operator Product(ProductDTO productDTO)
    {
        return new Product
        {
            Guid = productDTO.Guid,
            BarcodeID = productDTO.BarcodeId,
            Title = productDTO.Title,
        };
    }
}
