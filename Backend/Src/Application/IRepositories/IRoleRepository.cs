﻿using System.Collections;

namespace Application.IRepositories;

public interface IRoleRepository
{
    Task<IEnumerable<RoleEntity>> GetAllAsync();
    Task<RoleEntity?> GetByNameAsync(string name);
}