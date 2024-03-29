﻿using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class WalksRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be maximum 100 character!")]
        public string Name { get; set; }
        [Required]
        [MaxLength(1000, ErrorMessage = "Description has to be maximum 1000 character!")]
        public string Description { get; set; }
        [Required]
        [Range(0 , 50)]
        public double LengthInKm { get; set; }
        [Required]
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
