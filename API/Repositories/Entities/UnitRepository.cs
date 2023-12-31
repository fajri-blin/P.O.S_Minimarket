﻿using API.Contracts.Repositories.Entities;
using API.Data;
using API.Model.Entities;
using API.Models.Entities;

namespace API.Repository.Entities;

public class UnitRepository : GeneralRepository<Unit>, IUnitRepository
{
    public UnitRepository(PosDbContext posDbContext) : base(posDbContext)
    {
    }
    //Get Unit by Name
    public Unit? GetByName(string name)
    {
        return _posDbContext.Set<Unit>().FirstOrDefault(unit => unit.Name.ToLower() == name.ToLower());
    }

    public bool IsExits(string name)
    {
        return _posDbContext.Set<Unit>().Any(unit => unit.Name.ToLower() == name.ToLower());
    }
}
