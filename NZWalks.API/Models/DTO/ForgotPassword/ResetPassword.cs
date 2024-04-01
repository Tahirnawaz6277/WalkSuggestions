using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO.ForgotPassword
{
    public class ResetPassword : IValidatableObject
    {
        [Required]
        public string Email { get; set; }

        public string ResetToken { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        [Compare(nameof(NewPassword), ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NewPassword != ConfirmPassword)
            {
                yield return new ValidationResult("The new password and confirmation password do not match.", new[] { nameof(ConfirmPassword) });
            }
        }
    }
}
