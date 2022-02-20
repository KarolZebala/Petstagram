using System.ComponentModel.DataAnnotations;

namespace Petstagram.Server.Features.Pets.Models
{
    using static Data.Validation.Pet;
    public class UpdatePetRequestModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLenght)]
        public string Description { get; set; }
    }
}
