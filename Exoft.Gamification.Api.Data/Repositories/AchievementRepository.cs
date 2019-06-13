﻿using Exoft.Gamification.Api.Data.Core.Entities;
using Exoft.Gamification.Api.Data.Core.Helpers;
using Exoft.Gamification.Api.Data.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exoft.Gamification.Api.Data.Repositories
{
    public class AchievementRepository : Repository<Achievement>, IAchievementRepository
    {
        public AchievementRepository(UsersDbContext context) : base(context)
        {
        }

        public async Task<ReturnPagingInfo<Achievement>> GetPagedAchievementByUserAsync(PagingInfo pagingInfo, Guid UserId)
        {
            var list = await Context.UserAchievements
                .Where(o => o.User.Id == UserId)
                .Select(i => i.Achievement)
                .ToListAsync();

            var items = list
                .Skip((pagingInfo.CurrentPage - 1) * pagingInfo.PageSize)
                .Take(pagingInfo.PageSize)
                .ToList();

            var result = new ReturnPagingInfo<Achievement>()
            {
                CurrentPage = pagingInfo.CurrentPage,
                PageSize = items.Count,
                TotalItems = list.Count,
                TotalPages = (int)Math.Ceiling((double)list.Count / pagingInfo.PageSize),
                Data = items
            };

            return result;
        }

        protected override IQueryable<Achievement> IncludeAll()
        {
            return DbSet
                .Include(i => i.Icon)
                .OrderByDescending(i => i.XP);
        }

        public async Task<Achievement> IsUserHaveAchievementAsync(Guid userId, Guid achievementId)
        {
            return await Context.UserAchievements
                .Where(o => o.User.Id == userId && o.Achievement.Id == achievementId)
                .Select(i => i.Achievement)
                .SingleOrDefaultAsync();
        }
    }
}
