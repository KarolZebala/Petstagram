using Microsoft.AspNetCore.Mvc;
using Petstagram.Server.Features.Follow;
using Petstagram.Server.Infrastructure.Services;
using System.Threading.Tasks;

namespace Petstagram.Server.Features.Profiles.Models
{
    using static Infrastructure.WebConstatnts;
    public class ProfileController : ApiController
    {
        private readonly IProfileService profile;
        private readonly IFollowService follow;
        private readonly ICurrentUserService currentUser;

        public ProfileController(
            IProfileService profile,
            ICurrentUserService currentUser,
            IFollowService follow = null)
        {
            this.profile = profile;
            this.currentUser = currentUser;
            this.follow = follow;
        }

        [HttpGet]
        public async Task<ActionResult<ProfileServiceModel>> Mine()
             => await this.profile.ByUser(currentUser.GetId(), allInformation: true);

        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<ProfileServiceModel>> Details(string id)
        {
            var includeAllInformtion = await this.follow.IsFollower(id, this.currentUser.GetId());//sprawdzenie czy obecnie zalogowany user
                                                                                                  //followuje profil od podanym id
            if (!includeAllInformtion)
            {
                var userIsPrivate = !await this.profile.isPrivate(id);

            }

            return await this.profile.ByUser(id, includeAllInformtion);
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateProfileRequestModel model)
        {
            var userId = this.currentUser.GetId();

            var result = await this.profile.Update(
                userId,
                model.Email,
                model.UserName,
                model.Biography,
                model.Gender,
                model.WebSite,
                model.ProfilePhotoUrl,
                model.IsPrivate,
                model.Name);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
