using API.Contracts.Repositories;
using API.Model.Entities;

namespace API.Contracts.Repositories.Entities;

public interface IUnitRepository : IGeneralRepository<Unit>
{
    Unit? GetByName(string name);
}
