using Petstagram.Server.Features.Search.Models;
using Petstagram.Server.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petstagram.Server.Features.Search
{
    public interface ISearchService
    {
        Task<IEnumerable<ProfileSearchServiceModel>> ProfileSearch(string query);

        
    }
}
