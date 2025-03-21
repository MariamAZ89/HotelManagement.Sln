namespace HotelManagement.WebApp.ViewModels;

public class ReservationViewModel
{
    public int Id { get; set; }
    public int BookingGuestId { get; set; }
    public string GuestName { get; set; }
    public int RoomId { get; set; }
    public string RoomNumber { get; set; }
    public DateTime StartData { get; set; }
    public DateTime EndData { get; set; }
    public ReservationStatus Status { get; set; }
}
