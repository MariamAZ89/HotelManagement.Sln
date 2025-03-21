namespace HotelManagement.WebApp.Models;

public class Room
{
    public int Id { get; set; }
    public string Number { get; set; } = null!;
    public RoomType Type { get; set; } = RoomType.None;
    public decimal Price { get; set; }
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
