using API.Model.Entities;

namespace API.DTOs.ProductDTO;

public class ProductDTO
{
    public Guid Guid { get; set; }
    public string BarcodeId { get; set; }
    public string Title { get; set; }


    public static explicit operator ProductDTO(Product product)
    {
        return new ProductDTO
        {
            Guid = product.Guid,
            BarcodeId = product.BarcodeID,
            Title = product.Title,
        };
    }
}
