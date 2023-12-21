using API.Model.Entities;
using System.Data.Common;

namespace API.DTOs.TransactionsItemDTO;

public class NewTransactionItemDTO
{
    public Guid? ProductGuid { get; set; }
    public Guid? PriceGuid { get; set; }
    public float Quantity { get; set; }
    public decimal Subtotal { get; set; }

    public static explicit operator TransactionItem(NewTransactionItemDTO transactionItem)
    {
        return new TransactionItem
        {
            Guid = Guid.NewGuid(),
            ProductGuid = transactionItem.ProductGuid,
            PriceGuid = transactionItem.PriceGuid,
            Quantity = transactionItem.Quantity,
            Subtotal = transactionItem.Subtotal,
        };
    }
}
