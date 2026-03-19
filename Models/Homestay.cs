namespace StaySphere.API.Models;

public class Homestay
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public decimal PricePerNight { get; set; }
    public bool IsAvailable { get; set; }
    public string Description { get; set; } = string.Empty;
}