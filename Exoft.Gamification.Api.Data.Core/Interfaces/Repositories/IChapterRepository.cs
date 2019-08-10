﻿using Exoft.Gamification.Api.Data.Core.Entities;

namespace Exoft.Gamification.Api.Data.Core.Interfaces.Repositories
{
    public interface IChapterRepository : IRepository<Chapter>
    {
        int GetMaxOrderId();
    }
}
