namespace Petstagram.Server.Data.Models
{
    using Petstagram.Server.Data.Models.Base;
    using System;
    using System.ComponentModel.DataAnnotations;
    using static Validation.Pet;
    public class Pet : DeletableEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLenght)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

       

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
