using Microsoft.AspNetCore.Http;
using Petstagram.Server.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Petstagram.Server.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ClaimsPrincipal user;
        

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this.user = httpContextAccessor.HttpContext?.User;
        }

        public string GetId()
            => this.user
                .GetId();//GetId jest moją funkcją z Infrastructure.Extensions


        public string GetUserName()
            => this.user
                .Identity
                .Name;
                
    }
}
