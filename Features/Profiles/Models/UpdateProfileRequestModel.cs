using Petstagram.Server.Data;
using Petstagram.Server.Data.Models;
using System.ComponentModel.DataAnnotations;


namespace Petstagram.Server.Features.Profiles.Models
{
    using static Validation.User;
    public class UpdateProfileRequestModel
    {
        public string Email { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string ProfilePhotoUrl { get; set; }

        public string WebSite { get; set; }

        [MaxLength(MaxBiographyLenght)]
        public string Biography { get; set; }

        public Gender Gender { get; set; }

        public bool IsPrivate { get; set; }
    }
}
