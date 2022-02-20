using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Petstagram.Server.Features.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petstagram.Server.Features.Search
{
    public class SearchController : ApiController
    {
        private readonly ISearchService search;

        public SearchController(ISearchService search)
        {
            this.search = search;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route(nameof(Profiles))]//Search/Profiles?query
        public async Task<IEnumerable<ProfileSearchServiceModel>> Profiles(string query)
            => await this.search.ProfileSearch(query);
    }
}
