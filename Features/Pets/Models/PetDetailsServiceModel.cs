namespace Petstagram.Server.Features.Pets.Models
{
    public class PetDetailsServiceModel : PetListingServiceModel
    {
 
        public string Description { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
