using System.ComponentModel.DataAnnotations;

namespace StaySphere.API.DTOs;

public class CreateHomestayDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Location { get; set; } = string.Empty;

    [Required]
    [Range(100, 100000)]
    public decimal PricePerNight { get; set; }

    public bool IsAvailable { get; set; }

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
}