using API.Model.Entities;
using API.Utilities.Enums;

namespace API.DTOs.EmployeesDTO;

public class NewEmployeeDTO
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public EmployeeEnum RoleName { get; set; }

    public static explicit operator Employee(NewEmployeeDTO dto)
    {
        return new Employee
        {
            Guid = Guid.NewGuid(),
            FirstName = dto.Firstname,
            LastName = dto.Lastname,
            UserName = dto.Username,
            Password = dto.Password
        };
    }
}
