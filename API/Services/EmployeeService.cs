using API.Contract.Entities;
using API.Data;
using API.DTOs.EmployeesDTO;
using API.DTOs.RolesDTO;
using API.Model.Entities;
using API.Repository.Entities;

namespace API.Services;

public class EmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly PosDbContext _posDbContext;

    public EmployeeService(IEmployeeRepository employeeRepository,
                           IRoleRepository  roleRepository,
                           PosDbContext posDbContext)
    {
        _employeeRepository = employeeRepository;
        _roleRepository = roleRepository;
        _posDbContext = posDbContext;
    }

    public EmployeeDTO? Create(NewEmployeeDTO newEmployeeDTO)
    {
        using(var transactions = _posDbContext.Database.BeginTransaction()) 
        {
            try
            {
                var getRole = _roleRepository.GetByName(nameof(newEmployeeDTO.RoleName));
                Role newRole = null;
                if (getRole == null)
                {
                    newRole = (Role) new NewRoleDTO
                    {
                        Name = nameof(newEmployeeDTO.RoleName)
                    };
                    newRole = _roleRepository.Create(newRole);
                }
                var newEmployee = (Employee) newEmployeeDTO;
                var roleToUse = getRole ?? newRole;
                newEmployee.RoleGuid = roleToUse.Guid;

                var createdEmployee = _employeeRepository.Create(newEmployee);
                transactions.Commit();
                return (EmployeeDTO?) createdEmployee;
            }
            catch
            {
                transactions.Rollback();
                return null;
            }
        }
    }
}
