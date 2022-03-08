using Petstagram.Server.Features.Search.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Petstagram.Server.Features.Search
{
    public interface ISearchService
    {
        Task<IEnumerable<ProfileSearchServiceModel>> ProfileSearch(string query);


    }
}
