using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenrepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenrepository)
        {
            this.userManager = userManager;
            this.tokenrepository = tokenrepository;
        }

        [HttpPost]
        [Route("Register")]

        public async Task<IActionResult> Register([FromBody] RegisterPostRequestDto registerdto)
        {

            var identityUser = new IdentityUser
            {
                UserName = registerdto.Username,
                Email = registerdto.Username
            };
            var identityResult = await userManager.CreateAsync(identityUser, registerdto.Password);

            if (identityResult.Succeeded)
            {
                if (registerdto.Roles != null && registerdto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerdto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User succesfully registered now please login");
                    };
                }

            }

            return BadRequest("Something went wrong:");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto logindto)
        {
            var user = await userManager.FindByEmailAsync(logindto.Username);

            if (user != null)
            {
                var result = await userManager.CheckPasswordAsync(user, logindto.Password);
                if (result)
                {
                  var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        var jwtToken = tokenrepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };
                        return Ok(response);
                    }

                }
            }

            return BadRequest("Invalid Credential");


        }
    }
}
