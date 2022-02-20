using Petstagram.Server.Data.Models;
using Petstagram.Server.Features.Profiles.Models;
using Petstagram.Server.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petstagram.Server.Features.Profiles
{
    public interface IProfileService
    {
         Task<ProfileServiceModel> ByUser(string userId, bool allInformation = false);

        Task<Result> Update(
           string userId,
           string email,
           string userName,
           string biography,
           Gender gender,
           string webSite,
           string profilePhotoUrl,
           bool isPrivate,
           string name);

        Task<bool> isPrivate(string userId);
    }
}
