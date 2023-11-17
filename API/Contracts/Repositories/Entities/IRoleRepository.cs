using API.Contracts.Repositories;
using API.Model.Entities;

namespace API.Contracts.Repositories.Entities;

public interface IRoleRepository : IGeneralRepository<Role>
{
    Role? GetByName(string name);
}
