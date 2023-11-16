using API.Model.Entities;

namespace API.DTOs.TransactionsItemDTO;

public class TransactionItemDTO
{
    public Guid Guid { get; set; }
    public Guid? TransactionGuid { get; set; }
    public Guid? ProductGuid { get; set; }
    public Guid? PriceGuid { get; set; }
    public float Quantity { get; set; }
    public decimal SubTotal { get; set; }

    public static explicit operator TransactionItemDTO(TransactionItem transactionItem)
    {
        return new TransactionItemDTO
        {
            Guid = transactionItem.Guid,
            TransactionGuid = transactionItem.TransactionGuid,
            ProductGuid = transactionItem.ProductGuid,
            PriceGuid = transactionItem.PriceGuid,
            Quantity = transactionItem.Quantity,
            SubTotal = transactionItem.Subtotal
        };
    }

    public static explicit operator TransactionItem(TransactionItemDTO transactionItem)
    {
        return new TransactionItem
        {
            Guid = transactionItem.Guid,
            TransactionGuid = transactionItem.TransactionGuid,
            ProductGuid = transactionItem.ProductGuid,
            PriceGuid = transactionItem.PriceGuid,
            Quantity = transactionItem.Quantity,
            Subtotal = transactionItem.SubTotal
        };
    }
}
