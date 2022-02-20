using Petstagram.Server.Features.Pets.Models;
using Petstagram.Server.Infrastructure.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Petstagram.Server.Features.Pets
{
    public interface IPetsService
    {
        Task<int> Create(string imageUrl, string description, string userId);

        Task<IEnumerable<PetListingServiceModel>> ByUser(string userId);

        Task<PetDetailsServiceModel> Details(int id);

        Task<Result> Update(int id, string description, string userId);

        Task<Result> Delete(int id, string userId);
    }
}
