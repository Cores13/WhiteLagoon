﻿using WhiteLagoon.Domain.Entities;

namespace WhiteLagoon.Application.Interfaces
{
    public interface IVillaRepository: IRepository<Villa>
    {
        void Update(Villa entity);
    }
}
