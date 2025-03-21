namespace HotelManagement.WebApp.Models;

public class ApplicationRole : IdentityRole
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}