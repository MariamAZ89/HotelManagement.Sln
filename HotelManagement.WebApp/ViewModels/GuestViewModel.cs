using System.ComponentModel.DataAnnotations;

namespace HotelManagement.WebApp.ViewModels;

public class GuestViewModel
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = null!;

    [EmailAddress]
    public string? Email { get; set; }

    [Required, Phone]
    public string PhoneNumber { get; set; } = null!;
}
