using API.Contracts.Repositories.Entities;
using API.Data;
using API.Model.Entities;

namespace API.Repository.Entities;

public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(PosDbContext posDbContext) : base(posDbContext) { }


}
