using API.Models;

namespace API.Model.Entities;

public class TransactionItem : BaseEntity
{
    public Guid TransactionGuid { get; set; }
    public Guid ProductGuid { get; set; }
    public Guid PriceGuid { get; set; }
    public decimal Quantity { get; set; }
    public decimal Subtotal { get; set; }
}
