using API.Models;
using API.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model.Entities;

[Table("tb_m_unit")]
public class Unit : BaseEntity
{
    public string Name { get; set; }

    //Cardinality
    public ICollection<Price>? Prices { get; set; }
}
