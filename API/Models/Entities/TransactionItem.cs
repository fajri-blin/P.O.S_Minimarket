using API.Models;
using API.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model.Entities;

[Table("tb_m_transaction_item")]
public class TransactionItem : BaseEntity
{
    [Column("transaction_guid")]
    public Guid? TransactionGuid { get; set; }
    
    [Column("product_guid")]
    public Guid? ProductGuid { get; set; }
    
    [Column("price_guid")]
    public Guid? PriceGuid { get; set; }
    
    [Column("quantity")]
    public float Quantity { get; set; }
    
    [Column("subtotal", TypeName = "decimal(18,2)")]
    public decimal Subtotal { get; set; }

    //Cardinality
    public Transaction? Transaction { get; set; }
    public Product? Product { get; set; }
    public Price? Price { get; set; }
}
