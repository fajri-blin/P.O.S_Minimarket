using API.Models;

namespace API.Model.Entities;

public class Employee : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public Guid RoleGuid { get; set; }
}
