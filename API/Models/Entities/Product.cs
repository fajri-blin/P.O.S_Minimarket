using API.Models;
using API.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model.Entities;

[Table("tb_m_product")]
public class Product : BaseEntity
{
    [Column("barcode_id", TypeName ="nvarchar(255)")]
    public string BarcodeID { get; set; } = string.Empty;

    [Column("title", TypeName ="nvarchar(150)")]
    public string Title { get; set; } = string.Empty;

    //Cardinality
    public ICollection<Price>? Prices { get; set; } 
    public ICollection<TransactionItem>? TransactionItems { get; set; }
}
