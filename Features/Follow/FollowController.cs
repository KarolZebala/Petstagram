using Microsoft.AspNetCore.Mvc;
using Petstagram.Server.Features.Follow.Models;
using Petstagram.Server.Infrastructure.Services;
using System.Threading.Tasks;

namespace Petstagram.Server.Features.Follow
{
    public class FollowController : ApiController
    {
        private readonly ICurrentUserService currentUser;
        private readonly IFollowService follows;

        public FollowController(
            ICurrentUserService currentUser,
            IFollowService follows)
        {
            this.currentUser = currentUser;
            this.follows = follows;
        }

        [HttpPost]
        public async Task<ActionResult> Follow(FollowRequestModel model)
        {
            var result = await this.follows.Follow(
                model.UserId,
                this.currentUser.GetId());

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
