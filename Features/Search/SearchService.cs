using Microsoft.EntityFrameworkCore;
using Petstagram.Server.Data;
using Petstagram.Server.Features.Search.Models;
using Petstagram.Server.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petstagram.Server.Features.Search
{
    public class SearchService : ISearchService
    {
        private readonly PetstagramDbContext dbContext;

        public SearchService(PetstagramDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

       

        public async Task<IEnumerable<ProfileSearchServiceModel>> ProfileSearch(string query)
            => await this.dbContext
                .Users
                .Where(u => u.UserName.ToLower().Contains(query) ||
                     u.Profile.Name.ToLower().Contains(query))
                .Select(u => new ProfileSearchServiceModel
                {
                    UserId = u.Id,
                    UserName = u.UserName,
                    PhotoUrl = u.Profile.ProfilePhotoUrl
                })
                .ToListAsync();
            
    }
}
