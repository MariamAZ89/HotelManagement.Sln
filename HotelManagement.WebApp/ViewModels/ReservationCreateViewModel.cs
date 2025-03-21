using System.ComponentModel.DataAnnotations;

namespace HotelManagement.WebApp.ViewModels;

public class ReservationCreateViewModel : IValidatableObject
{
    [Required(ErrorMessage = "Please select a guest.")]
    public int BookingGuestId { get; set; }

    [Required(ErrorMessage = "Please select a room.")]
    public int RoomId { get; set; }

    [Required(ErrorMessage = "Start date is required.")]
    public DateTime? StartData { get; set; }

    [Required(ErrorMessage = "End date is required.")]
    public DateTime? EndData { get; set; }

    [Required(ErrorMessage = "Status is required.")]
    public ReservationStatus? Status { get; set; }

    public IEnumerable<Guest>? Guests { get; set; }
    public IEnumerable<Room>? Rooms { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndData <= StartData)
        {
            yield return new ValidationResult("End date must be after start date.", new[] { nameof(EndData) });
        }
    }
}