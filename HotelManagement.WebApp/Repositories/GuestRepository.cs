using HotelManagement.WebApp.Data;

namespace HotelManagement.WebApp.Repositories;

public class GuestRepository : IGuestRepository
{
    private readonly HotelDbContext _context;

    public GuestRepository(HotelDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Guest>> GetAllAsync()
    {
        return await _context.Guests.ToListAsync();
    }
}
