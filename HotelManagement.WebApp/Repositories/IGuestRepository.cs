namespace HotelManagement.WebApp.Repositories;

public interface IGuestRepository
{
    Task<IEnumerable<Guest>> GetAllAsync();
    Task<Guest?> GetByIdAsync(int id);
    Task AddAsync(Guest guest);
    Task UpdateAsync(Guest guest);
    Task DeleteAsync(int id);
}

