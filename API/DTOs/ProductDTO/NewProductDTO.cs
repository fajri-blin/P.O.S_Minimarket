using API.Model.Entities;

namespace API.DTOs.ProductDTO;

public class NewProductDTO
{
    public string BarcodeID { get; set; }
    public string Title { get; set; }

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
