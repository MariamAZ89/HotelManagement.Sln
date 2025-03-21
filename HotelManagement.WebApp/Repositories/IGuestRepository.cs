namespace HotelManagement.WebApp.Repositories;

public interface IGuestRepository
{
    Task<IEnumerable<Guest>> GetAllAsync();
}
