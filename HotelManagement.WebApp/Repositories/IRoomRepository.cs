namespace HotelManagement.WebApp.Repositories;
public interface IRoomRepository
{
    Task<IEnumerable<Room>> GetAllAsync();
}
