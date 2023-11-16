using API.DTOs.TransactionsItemDTO;
using API.Model.Entities;

namespace API.DTOs.TransactionsDTO;

public class TransactionDTO
{
    public Guid Guid { get; set; }
    public Guid? EmployeeGuid { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal? TotalAmmount { get; set; }
    public ICollection<TransactionItemDTO>? TransactionItemsDTO { get; set; }

    public static explicit operator TransactionDTO(Transaction transaction)
    {
        return new TransactionDTO
        {
            Guid = transaction.Guid,
            EmployeeGuid = transaction.EmployeeGuid,
            TransactionDate = transaction.TransactionsDate,
            TotalAmmount = transaction.TotalAmmount
        };
    }
    public static explicit operator Transaction(TransactionDTO transactionDTO)
    {
        return new Transaction
        {
            Guid = transactionDTO.Guid,
            EmployeeGuid = transactionDTO.EmployeeGuid,
            TransactionsDate = transactionDTO.TransactionDate,
            TotalAmmount = transactionDTO.TotalAmmount
        };
    }
}
