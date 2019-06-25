﻿using Exoft.Gamification.Api.Data.Core.Entities;
using Exoft.Gamification.Api.Data.Core.Helpers;
using Exoft.Gamification.Api.Data.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Exoft.Gamification.Api.Data.Repositories
{
    public class UserAchievementRepository : Repository<UserAchievement>, IUserAchievementRepository
    {
        public UserAchievementRepository(UsersDbContext context) : base(context)
        {
        }

        public async Task<ReturnPagingInfo<UserAchievement>> GetAllAchievementsByUserAsync(PagingInfo pagingInfo, Guid UserId)
        {
            var list = IncludeAll()
                .Where(o => o.User.Id == UserId)
                .Select(i => i)
                .OrderByDescending(i => i.AddedTime);

            var items = list
                .Skip((pagingInfo.CurrentPage - 1) * pagingInfo.PageSize)
                .Take(pagingInfo.PageSize)
                .ToList();

            int listCount = await list.CountAsync();

            var result = new ReturnPagingInfo<UserAchievement>()
            {
                CurrentPage = pagingInfo.CurrentPage,
                PageSize = items.Count,
                TotalItems = listCount,
                TotalPages = (int)Math.Ceiling((double)listCount / pagingInfo.PageSize),
                Data = items
            };

            return result;
        }
        
        public async Task<int> GetCountAchievementsByUserAsync(Guid userId)
        {
            return await IncludeAll()
                .Where(o => o.User.Id == userId)
                .CountAsync();
        }

        public async Task<UserAchievement> GetSingleUserAchievementAsync(Guid userAchievementId)
        {
            return await IncludeAll()
                .Where(o => o.Id == userAchievementId)
                .Select(i => i)
                .SingleOrDefaultAsync();
        }

        protected override IQueryable<UserAchievement> IncludeAll()
        {
            return DbSet
                .Include(i => i.Achievement)
                .Include(i => i.User);
        }
    }
}
