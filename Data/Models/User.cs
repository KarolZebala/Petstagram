using Microsoft.AspNetCore.Identity;
using Petstagram.Server.Data.Models.Base;
using System;
using System.Collections.Generic;

namespace Petstagram.Server.Data.Models
{
    public class User : IdentityUser, IEntity
    {
        public Profile Profile { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModyfiedOn { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public IEnumerable<Pet> Pets { get; } = new HashSet<Pet>();
    }
}
