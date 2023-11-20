using API.Contracts.Repositories.Entities;
using API.Data;
using API.Model.Entities;

namespace API.Repository.Entities;

public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(PosDbContext posDbContext) : base(posDbContext) { }

    public bool IsUniqueUsername(string username)
    {
        return _posDbContext.Set<Employee>().Any(employee => employee.UserName == username);
    }

}
