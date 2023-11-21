using API.Model.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Entities;

[Table("tb_tr_price")]
public class Price : BaseEntity
{
    [Column("product_guid")]
    public Guid? ProductGuid { get; set; }

    [Column("price_guid")]
    public Guid? UnitGuid { get; set; }

    [Column("amount", TypeName ="decimal(18,2)")]
    public decimal Amount { get; set; }

    //Cardinality
    public Product? Product { get; set; }
    public Unit? Unit { get; set; }
    public ICollection<TransactionItem>? TransactionItems { get; set;}
}
