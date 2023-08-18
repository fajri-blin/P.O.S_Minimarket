using API.Contract.Entities;
using API.Data;
using API.Model.Entities;
using API.Models.Entities;

namespace API.Repository.Entities;

public class UnitRepository : GeneralRepository<Unit>, IUnitRepository
{
    public UnitRepository(PosDbContext posDbContext) : base(posDbContext)
    {
    }
}
