using Microsoft.EntityFrameworkCore;
using Petstagram.Server.Data;
using Petstagram.Server.Data.Models;
using Petstagram.Server.Features.Pets.Models;
using Petstagram.Server.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petstagram.Server.Features.Pets
{
    public class PetsService : IPetsService
    {
        private readonly PetstagramDbContext dbContext;

        public PetsService(PetstagramDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public object User { get; private set; }

        public async Task<int> Create(string imageUrl, string description, string userId)
        {


            var pet = new Pet
            {
                Description = description,
                ImageUrl = imageUrl,
                UserId = userId
            };

            dbContext.Add(pet);
            await dbContext.SaveChangesAsync();

            return pet.Id;
        }

        public async Task<IEnumerable<PetListingServiceModel>> ByUser(string userId)
            => await this.dbContext
                    .Pets
                    .Where(c => c.UserId == userId)
                    .OrderByDescending(c => c.CreatedOn)
                    .Select(c => new PetListingServiceModel
                    {
                        Id = c.Id,
                        ImageUrl = c.ImageUrl,
                        Description = c.Description
                    })
                    .ToListAsync();

        public async Task<PetDetailsServiceModel> Details(int id)
            => await this.dbContext
                    .Pets
                    .Where(c => c.Id == id)
                    .Select(c => new PetDetailsServiceModel
                    {
                        Id = c.Id,
                        UserId =c.UserId,
                        Description=c.Description,
                        UserName=c.User.UserName,
                        ImageUrl=c.ImageUrl

                    })
                    .FirstOrDefaultAsync();

        public async Task<Result> Update(int id, string description, string userId)
        {
            var pet = await this.ByIdAndByUserId(id, userId);

            if (pet == null)
            {
                return false;
            }

            pet.Description = description;
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Result> Delete(int id, string userId)
        {
            var pet = await this.ByIdAndByUserId(id, userId);

            if (pet == null)
            {
                return false;
            }

            this.dbContext.Remove(pet);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        private async Task<Pet> ByIdAndByUserId(int id, string userId)
            => await this.dbContext
                .Pets
                .Where(c => c.UserId == userId && c.Id == id)
                .FirstOrDefaultAsync();
    }
}
