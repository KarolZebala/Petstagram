using Microsoft.EntityFrameworkCore;
using Petstagram.Server.Data;
using Petstagram.Server.Infrastructure.Services;
using System;
using Petstagram.Server.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Petstagram.Server.Features.Follow
{
    public class FollowService : IFollowService
    {
        private readonly PetstagramDbContext dbContext;

        public FollowService(PetstagramDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Result> Follow(string userId, string followerId)
        {
            var userAlreadyFollowed = await this.dbContext
                .Follows
                .AnyAsync(p => p.UserId == userId && p.FollowerId == followerId);

            if (userAlreadyFollowed)
            {
                return "This user is already followed";
            }

            var publicProfile = await this.dbContext
                .Profiles
                .Where(p => p.UserId ==userId)
                .Select(p => !p.IsPrivate)
                .FirstOrDefaultAsync();

            this.dbContext
                .Follows
                .Add(new Data.Models.Follow
                {
                    UserId = userId,
                    FollowerId = followerId,
                    IsApproved = publicProfile
                });

            await this.dbContext.SaveChangesAsync();


            return true;
        }

        public async Task<bool> IsFollower(string userId, string followerId)
           => await this.dbContext
               .Follows
               .AnyAsync(f => f.UserId == userId &&
                            f.FollowerId == followerId &&
                            f.IsApproved);  
    }
}
