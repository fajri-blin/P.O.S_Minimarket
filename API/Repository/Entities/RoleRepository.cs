using API.Contract.Entities;
using API.Data;
using API.Model.Entities;
using API.Models.Entities;

namespace API.Repository.Entities;

public class RoleRepository : GeneralRepository<Role>, IRoleRepository
{
    public RoleRepository(PosDbContext posDbContext) : base(posDbContext)
    {
    }
}
