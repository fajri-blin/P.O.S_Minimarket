using API.Contracts.Repositories;
using API.Model.Entities;

namespace API.Contracts.Repositories.Entities;

public interface IEmployeeRepository : IGeneralRepository<Employee>
{
    public bool IsUniqueUsername(string username);
}
