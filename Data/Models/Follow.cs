using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Petstagram.Server.Data.Models
{
    public class Follow
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public string FollowerId { get; set; }

        [Required]
        public User Follower { get; set; }

        public bool IsApproved { get; set; }
    }
}
