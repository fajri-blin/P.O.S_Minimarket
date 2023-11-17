using API.Contracts.Repositories.Entities;
using API.Data;
using API.Model.Entities;
using API.Models.Entities;

namespace API.Repository.Entities;

public class RoleRepository : GeneralRepository<Role>, IRoleRepository
{
    public RoleRepository(PosDbContext posDbContext) : base(posDbContext) { }

    public Role? GetByName(string name)
    {
        return _posDbContext.Set<Role>().FirstOrDefault(role => role.Name.ToLower() == name.ToLower());
    }
}
