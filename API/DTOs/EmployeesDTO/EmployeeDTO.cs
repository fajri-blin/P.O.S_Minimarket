using API.Model.Entities;

namespace API.DTOs.EmployeesDTO;

public class EmployeeDTO
{
    public Guid Guid { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public Guid? RoleGuid { get; set; }

    public static explicit operator Employee(EmployeeDTO dto)
    {
        return new Employee
        {
            Guid = dto.Guid,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            UserName = dto.UserName,
            Password = dto.Password,
        };
    }

    public static explicit operator EmployeeDTO(Employee employee)
    {
        return new EmployeeDTO
        {
            Guid = employee.Guid,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            UserName = employee.UserName,
            Password = employee.Password,
        };
    }

}
