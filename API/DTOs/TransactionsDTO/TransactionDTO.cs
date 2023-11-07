using API.Model.Entities;

namespace API.DTOs.TransactionsDTO;

public class TransactionDTO
{
    public Guid Guid { get; set; }
    public Guid? EmployeeGuid { get; set; }
    public DateTime? TransactionDate { get; set; }
    public decimal TotalAmmount { get; set; }

    public static explicit operator TransactionDTO(Transaction transaction)
    {
        return new TransactionDTO
        {
            Guid = transaction.Guid,
            EmployeeGuid = transaction.EmployeeGuid,
            TransactionDate = transaction.TransactionsDate
        };
    }
}
