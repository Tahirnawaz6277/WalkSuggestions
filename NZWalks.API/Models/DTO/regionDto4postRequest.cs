using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class regionDto4postRequest
    {
        [Required]
        [MinLength(3, ErrorMessage ="Code has to be minimum 3 character!")]
        [MaxLength(6, ErrorMessage = "Code has to be maximum 6 character!")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be maximum 100 character!")]
        public string Name { get; set; }
        
        public string? RegionImageUrl { get; set; }
    }
}
