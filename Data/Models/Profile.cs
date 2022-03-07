using System.ComponentModel.DataAnnotations;

namespace Petstagram.Server.Data.Models
{
    using static Validation.User;

    /// <summary>
    /// This class has detail information about user account
    /// </summary>
    public class Profile
    {
        [Key]
        [Required]
        public string UserId { get; set; }

        [MaxLength(MaxNameLenght)]
        public string Name { get; set; }

        public string ProfilePhotoUrl { get; set; }

        public string WebSite { get; set; }

        [MaxLength(MaxBiographyLenght)]
        public string Biography { get; set; }

        public Gender Gender { get; set; }

        public bool IsPrivate { get; set; }
    }
}
