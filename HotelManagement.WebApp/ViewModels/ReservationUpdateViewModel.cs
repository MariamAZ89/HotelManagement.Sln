using System.ComponentModel.DataAnnotations;

namespace HotelManagement.WebApp.ViewModels
{
    public class ReservationUpdateViewModel
    {
        public int Id { get; set; }
        [Required]
        public int BookingGuestId { get; set; }
        [Required]
        public int RoomId { get; set; }
        [Required]
        public DateTime StartData { get; set; }
        [Required]
        public DateTime EndData { get; set; }
        [Required]
        public ReservationStatus Status { get; set; }
        public IEnumerable<Guest>? Guests { get; set; }
        public IEnumerable<Room>? Rooms { get; set; }
    }
}
