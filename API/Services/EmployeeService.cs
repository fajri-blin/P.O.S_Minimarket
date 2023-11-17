using API.Contracts.Repositories.Entities;
using API.Data;
using API.DTOs.EmployeesDTO;
using API.DTOs.RolesDTO;
using API.Model.Entities;
using API.Repository.Entities;
using API.Utilities.Enums;

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

    public IEnumerable<EmployeeDTO>? Get() 
    {
        var employeeList = _employeeRepository.GetAll();
        if(employeeList == null || !employeeList.Any())
        {
            return null;
        }

        var dto = employeeList.Select(employee => (EmployeeDTO)employee);
        return dto;

    }

    public EmployeeDTO? Create(NewEmployeeDTO newEmployeeDTO)
    {
        using(var transactions = _posDbContext.Database.BeginTransaction()) 
        {
            try
            {
                //check existing role in employee's input
                var getRole = _roleRepository.GetByName(nameof(newEmployeeDTO.RoleName));
                Role newRole = null;
                
                if (getRole == null)
                {
                    newRole = (Role)new NewRoleDTO
                    {
                        Name = Enum.GetName(typeof(EmployeeEnum), newEmployeeDTO.RoleName)
                    };
                    newRole = _roleRepository.Create(newRole);
                }
                var roleToUse = getRole ?? newRole;

                var newEmployee = (Employee) newEmployeeDTO;
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

    public bool Edit(EmployeeDTO employeeDTO)
    {
        using(var transaction = _posDbContext.Database.BeginTransaction())
        {
            try
            {
                //get employee
                var getEmployee = _employeeRepository.GetByGuid(employeeDTO.Guid);
                if (getEmployee == null) return false;

                //set new employee
                var newEmployee = (Employee)employeeDTO;
                newEmployee.RoleGuid = getEmployee.RoleGuid;

                // update the employee
                var updateEmployee = _employeeRepository.Update(newEmployee);
                if (!updateEmployee) return false;
                transaction.Commit();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public int Delete(Guid guid)
    {
        using(var transactions = _posDbContext.Database.BeginTransaction())
        {
            try
            {
                var employeeEntity = _employeeRepository.GetByGuid(guid);
                if (employeeEntity == null) return 0;

                var isDelete = _employeeRepository.Delete(employeeEntity);
                if (isDelete == false) return 0;
                transactions.Commit();
                return 1;

            }
            catch
            {
                transactions.Rollback();
                return -1;
            }
        }
        
    }
}
