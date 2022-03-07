namespace Petstagram.Server.Features.Identity 
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Petstagram.Server.Data.Models;
    using System.Threading.Tasks;
    using Models;
    using Microsoft.AspNetCore.Authorization;
    

    
    public class IdentityController : ApiController
    {
        private readonly UserManager<User> userManager;
        private readonly IIdentityService identity;
        private readonly AppSettings appSettings;

        public IdentityController(
            UserManager<User> userManager,
            IOptions<AppSettings> appSettings,
            IIdentityService service)
        {
            this.userManager = userManager;
            this.identity = service;
            this.appSettings = appSettings.Value;
        }

        

        [HttpPost]
        [Route(nameof(Register))]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterUserRequestModel model)
        {
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email
            };
            var result = await this.userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpPost]
        [Route(nameof(Login))]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponeModel>> Login(LoginUserRequestModel model)
        {
            var user = await this.userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return Unauthorized();
            }

            var passwordValid = await this.userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid)
            {
                return Unauthorized();
            }

            var encryptedToken = identity.GenerateJwtToken(
                user.Id,
                user.UserName,
                this.appSettings.Secret);

            return new LoginResponeModel
            {
                Token = encryptedToken
            };

            
        }

        
    }
}
