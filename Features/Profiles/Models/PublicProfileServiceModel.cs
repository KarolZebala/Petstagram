using Petstagram.Server.Features.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petstagram.Server.Features.Profiles.Models
{
    public class PublicProfileServiceModel : ProfileServiceModel
    {
        public string WebSite { get; set; }

        public string Biography { get; set; }

        public string Gender { get; set; }
    }
}
