namespace Petstagram.Server.Features.Pets.Models
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Petstagram.Server.Infrastructure;
    using Petstagram.Server.Infrastructure.Extensions;
    using Petstagram.Server.Infrastructure.Services;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using static Infrastructure.WebConstatnts;


    public class PetsController : ApiController
    {
        private readonly IPetsService pet;
        private readonly ICurrentUserService currentUser;

        public PetsController(
            IPetsService service
            ,ICurrentUserService currentUserService)
        {
            this.pet = service;
            this.currentUser = currentUserService;
        }


        [HttpPut]
        [Route(Id)]
        public async Task<ActionResult> Update(int id, UpdatePetRequestModel model)
        {
            var userId = this.currentUser.GetId();

            var updated = await this.pet.Update(
                id,
                model.Description,
                userId);

            if (!updated.Succeeded)
            {
                return BadRequest();
            }

            return Ok();
        }

        //zwraca wszystkie zwięrzęta z mojego profilu
        [Authorize] //tak naprawdę to Authorize można dać na poziomie ApiController, bo się wszędzie powtarza
        [HttpGet]
        public async Task<IEnumerable<PetListingServiceModel>> Mine()
        {
            var userId = this.currentUser.GetId();

            var cats = await this.pet.ByUser(userId);// zeby moc tego używać to potrzebujemy tokenu i Claimów,
                                                         //więc musi być Authorize

            return cats;
        }

        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<PetDetailsServiceModel>> Details(int id)
        {
            var pet = await this.pet.Details(id);

            //to sprawdzenie zostało przeniesione do ModelOrNotFounActionFilter
            /*if (pet == null)
                return NotFound();*/

            return pet;
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(CreatePetRequestModel model)
        {
            var userId = this.currentUser.GetId();

            var petId = await pet.Create(
                model.ImageUrl,
                model.Description,
                userId);

            return Created(nameof(this.Create), petId);
        }

        [HttpDelete]
        [Route(Id)]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = this.currentUser.GetId();

            var deleted = await this.pet.Delete(id, userId);

            if (!deleted.Succeeded)
                return BadRequest();

            return Ok();
        }
    }
}
