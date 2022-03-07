using Microsoft.EntityFrameworkCore;
using Petstagram.Server.Data;
using Petstagram.Server.Data.Models;
using Petstagram.Server.Features.Profiles.Models;
using Petstagram.Server.Infrastructure.Services;
using System.Linq;
using System.Threading.Tasks;

namespace Petstagram.Server.Features.Profiles
{
    public class ProfileService : IProfileService
    {
        private readonly PetstagramDbContext dbContext;

        public ProfileService(PetstagramDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ProfileServiceModel> ByUser(string userId, bool allInformation)
        {
            var profile = await dbContext
                .Users
                .Where(u => u.Id == userId && u.Profile.Name != null)//trzeba najpierw zauktualizować imie 
                .Select(u => allInformation
                    ? new PublicProfileServiceModel
                    {
                        Name = u.Profile.Name,
                        Biography = u.Profile.Biography,
                        Gender = u.Profile.Gender.ToString(),
                        WebSite = u.Profile.WebSite,
                        IsPrivate = u.Profile.IsPrivate,
                        ProfilePhotoUrl = u.Profile.ProfilePhotoUrl

                    }
                    : new ProfileServiceModel
                    {
                        Name = u.Profile.Name,
                        IsPrivate = u.Profile.IsPrivate,
                        ProfilePhotoUrl = u.Profile.ProfilePhotoUrl

                    })
                .FirstOrDefaultAsync();
            return profile;
        }

        public async Task<bool> isPrivate(string userId)
            => await this.dbContext
                .Profiles
                .Where(p => p.UserId == userId)
                .Select(p => p.IsPrivate)
                .FirstOrDefaultAsync();

        public async Task<Result> Update(
            string userId,
            string email,
            string userName,
            string biography,
            Gender gender,
            string webSite,
            string profilePhotoUrl,
            bool isPrivate,
            string name)
        {
            var user = await this.dbContext
                .Users
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return "User does not exist";
            }

            if (user.Profile == null)
            {
                user.Profile = new Profile();
            }

            var emailResult = await this.ChangeProfileEmail(user, userId, email);
            if (emailResult.Failure)
            {
                return emailResult;
            }

            var userNameResult = await this.ChangeUserName(user, userId, userName);
            if (userNameResult.Failure)
            {
                return userNameResult;
            }

            //można też dać te właściwości w ify i wtedy będzie odrobine bardziej wydajne
            //bo będziemy zmieniać tylko te właściwości które faktycznie się zmieniły
            user.Profile.Name = name;
            user.Profile.Biography = biography;
            user.Profile.Gender = gender;
            user.Profile.WebSite = webSite;
            user.Profile.ProfilePhotoUrl = profilePhotoUrl;
            user.Profile.IsPrivate = isPrivate;

            await this.dbContext.SaveChangesAsync();

            return true;
        }

        private async Task<Result> ChangeProfileEmail(User user, string userId, string email)
        {
            if (!string.IsNullOrWhiteSpace(email) && user.Email != email)
            {
                var emailExist = await this.dbContext
                    .Users
                    .AnyAsync(u => u.Id != userId && u.Email == email);

                if (emailExist)
                {
                    return "The provided e-mail already exist";
                }

                user.Email = email;
            }
            return true;
        }

        private async Task<Result> ChangeUserName(User user, string userId, string userName)
        {
            if (!string.IsNullOrWhiteSpace(userName))
            {
                var userNameExist = await this.dbContext
                    .Users
                    .AnyAsync(u => u.Id != userId && u.UserName == userName);

                if (userNameExist)
                {
                    return "The provided userName already exist";
                }

                user.UserName = userName;
            }

            return true;
        }
    }
}
