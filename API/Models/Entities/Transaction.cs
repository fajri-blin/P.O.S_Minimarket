using API.Models;

namespace API.Model.Entities;

public class Transaction : BaseEntity
{
    public DateTime TransactionsDate { get; set; }
    public decimal TotalAmmount { get; set; }
    public Guid EmployeeGuid { get; set; }

}
