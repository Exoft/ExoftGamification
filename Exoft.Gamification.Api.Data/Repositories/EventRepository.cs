﻿using Exoft.Gamification.Api.Data.Core.Entities;
using Exoft.Gamification.Api.Data.Core.Helpers;
using Exoft.Gamification.Api.Data.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Exoft.Gamification.Api.Data.Repositories
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(UsersDbContext context) : base(context)
        {
        }

        public override async Task<ReturnPagingInfo<Event>> GetAllDataAsync(PagingInfo pagingInfo)
        {
            var items = await IncludeAll()
                .OrderByDescending(s => s.CreatedTime)
                .Skip((pagingInfo.CurrentPage - 1) * pagingInfo.PageSize)
                .Take(pagingInfo.PageSize)
                .ToListAsync();

            int allItemsCount = await IncludeAll().CountAsync();

            var result = new ReturnPagingInfo<Event>()
            {
                CurrentPage = pagingInfo.CurrentPage,
                PageSize = items.Count,
                TotalItems = allItemsCount,
                TotalPages = (int)Math.Ceiling((double)allItemsCount / pagingInfo.PageSize),
                Data = items
            };

            return result;
        }

        protected override IQueryable<Event> IncludeAll()
        {
            return DbSet
                .Include(s => s.User);
        }
    }
}