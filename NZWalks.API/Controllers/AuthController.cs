using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Models.DTO.Email;
using NZWalks.API.Models.DTO.ForgotPassword;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenrepository;
        private readonly IEmail _emailservice;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenrepository , IEmail emailservice)
        {
            this.userManager = userManager;
            this.tokenrepository = tokenrepository;
           _emailservice = emailservice;
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

        // forgot password api
        [HttpPost]
        [Route("forgot_password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequestDto email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userManager.FindByEmailAsync(email.Email);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);

            if (resetToken == null)
            {
                return BadRequest("token not created ");
            }

            // here to send the resetToken via email to the user , here is logic of email
            var emailDto = new EmailDTO
            {
                to = email.Email,
                subject = "Password Reset",
                body = $"Dear User,<br><br>You requested to reset your password. Here is your reset token: <b>{resetToken}</b>"
            };
            _emailservice.SendEmail(emailDto);

            return Ok("Reset Token has been sent to your email ");
        }


        // reset password api
        [HttpPost]
        [Route("reset_password")]
        public async Task<IActionResult> ResetPassword(ResetPassword usermail)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Euser = await userManager.FindByEmailAsync(usermail.Email);
            if (Euser == null)
            {
                return BadRequest("user not found");
            }
            var resetToken = await userManager.ResetPasswordAsync(Euser, usermail.ResetToken, usermail.NewPassword);
            if (resetToken.Succeeded)
            {
                return Ok("Password Reset Sucessfully");
            }
            return BadRequest("Failed to Reset Password!");

        }
    }
}
