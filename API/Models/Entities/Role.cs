using API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model.Entities;

[Table("tb_m_role")]
public class Role : BaseEntity
{
    [Column("name", TypeName ="nvarchar(50)")]
    public string Name { get; set; }
    
    //Cardinality

    public ICollection<Employee>? Employees { get; set; }
}
