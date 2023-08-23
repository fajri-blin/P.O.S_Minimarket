﻿using API.Model.Entities;

namespace API.Contract.Entities;

public interface IRoleRepository : IGeneralRepository<Role>
{
    Role? GetByName(string name);
}
