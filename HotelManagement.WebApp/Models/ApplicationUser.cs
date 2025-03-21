namespace HotelManagement.WebApp.Models;

public class ApplicationUser: IdentityUser
{
    public string Position { get; set; } = null!;
}