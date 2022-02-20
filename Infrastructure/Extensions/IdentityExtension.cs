using System.Linq;
using System.Security.Claims;

namespace Petstagram.Server.Infrastructure.Extensions
{
    public static class IdentityExtension
    {
        public static string GetId(this ClaimsPrincipal user)
            => user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
            ?.Value;
    }
}
