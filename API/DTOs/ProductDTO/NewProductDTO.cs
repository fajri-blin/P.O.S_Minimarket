using API.Model.Entities;

namespace API.DTOs.ProductDTO;

public class NewProductDTO
{
    public string BarcodeID { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;

    public static explicit operator Product(NewProductDTO product)
    {
        return new Product
        {
            Guid = Guid.NewGuid(),
            BarcodeID = product.BarcodeID,
            Title = product.Title,
        };
    }
}
