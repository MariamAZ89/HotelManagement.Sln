namespace HotelManagement.WebApp.Models;

public class Reservation
{
    public int Id { get; set; }
    public int GuestId { get; set; }
    public Guest? Guest { get; set; }
    public int RoomId { get; set; }
    public Room? Room { get; set; }
    public DateTime StartData { get; set; }
    public DateTime EndData { get; set; }
    public ReservationStatus Status { get; set; } = ReservationStatus.Unknown;
}
