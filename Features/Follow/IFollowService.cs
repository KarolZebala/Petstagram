using Petstagram.Server.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petstagram.Server.Features.Follow
{
    public interface IFollowService
    {
        Task<Result> Follow(string userId, string followerId);
        Task<bool> IsFollower(string userId, string followerId);
    }
}
