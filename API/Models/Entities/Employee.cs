using API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model.Entities;

[Table("tb_m_employee")]
public class Employee : BaseEntity
{
    [Column("firstname", TypeName ="nvarchar(200)")]
    public string FirstName { get; set; } = string.Empty;
    
    [Column("lastname", TypeName ="nvarchar(200)")]
    public string? LastName { get; set; }

    [Column("username", TypeName ="nvarchar(100)")]
    public string UserName { get; set; } = string.Empty;

    [Column("password")]
    public string Password { get; set; } = string.Empty;

    [Column("role_guid")]
    public Guid? RoleGuid { get; set; }


    //Cardinality
    public Role? Role { get; set; }
    public ICollection<Transaction>? Transactions { get; set; }
}
