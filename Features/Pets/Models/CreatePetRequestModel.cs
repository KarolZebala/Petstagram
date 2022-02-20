namespace Petstagram.Server.Features.Pets.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Data.Validation.Pet;

    public class CreatePetRequestModel
    {
        
        [MaxLength(MaxDescriptionLenght)]
        public string Description { get; set; }


        [Required]
        public string ImageUrl { get; set; }
    }
}
