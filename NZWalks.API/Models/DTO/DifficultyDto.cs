using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class DifficultyDto
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Name has to be minimum 3 character!")]
        [MaxLength(100, ErrorMessage = "Name has to be maximum 100 character!")]
        public string Name { get; set; }
    }
}
