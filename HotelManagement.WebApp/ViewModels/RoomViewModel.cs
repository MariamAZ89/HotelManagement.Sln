namespace HotelManagement.WebApp.ViewModels;
public class RoomViewModel
{
    public int Id { get; set; }
    public string Number { get; set; } = null!;
    public RoomType Type { get; set; } = RoomType.None;
    public decimal Price { get; set; }
}
