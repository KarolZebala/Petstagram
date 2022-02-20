using Petstagram.Server.Data;
using Petstagram.Server.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


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
