using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO.ForgotPassword
{
    public class ForgotPasswordRequestDto
    {
        [Required]
        public string Email { get; set; }
    }
}
