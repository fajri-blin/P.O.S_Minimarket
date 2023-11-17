using API.Contracts.Repositories.Entities;
using API.Data;
using API.Model.Entities;
using API.Models.Entities;

namespace API.Repository.Entities;

public class PriceRepository : GeneralRepository<Price>, IPriceRepository
{
    public PriceRepository(PosDbContext posDbContext) : base(posDbContext) { }

}
