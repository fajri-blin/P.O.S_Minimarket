using API.Model.Entities;

namespace API.Contract.Entities;

public interface IUnitRepository : IGeneralRepository<Unit>
{
    Unit? GetByName(string name);
}
