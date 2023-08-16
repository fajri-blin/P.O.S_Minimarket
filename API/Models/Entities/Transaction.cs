using API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model.Entities;

[Table("tb_tr_transaction")]
public class Transaction : BaseEntity
{
    [Column("employee_guid")]
    public Guid? EmployeeGuid { get; set; }
    
    [Column("transaction_date")]
    public DateTime TransactionsDate { get; set; }
    

    [Column("total_ammount", TypeName ="decimal(18,2)")]
    public decimal? TotalAmmount { get; set; }

    //Cardinality
    public ICollection<TransactionItem>? TransactionItems { get; set; }
    public Employee? Employee { get; set; }
}
