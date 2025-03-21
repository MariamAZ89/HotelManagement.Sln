namespace HotelManagement.WebApp.Areas.Admin.Controllers;


[Area("Admin")]
[Authorize(Roles = "Admin, Manager")]
public class RoomController : Controller
{
    private readonly IRoomRepository _roomRepository;

    public RoomController(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<IActionResult> Index()
    {
        var rooms = await _roomRepository.GetAllAsync();
        var viewModel = rooms.Select(r => new RoomViewModel
        {
            Id = r.Id,
            Number = r.Number,
            Type = r.Type,
            Price = r.Price
        }).ToList();

        return View(viewModel);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(RoomViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var room = new Room
        {
            Number = model.Number,
            Type = model.Type,
            Price = model.Price
        };

        await _roomRepository.AddAsync(room);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var room = await _roomRepository.GetByIdAsync(id);
        if (room == null) return NotFound();

        var viewModel = new RoomViewModel
        {
            Id = room.Id,
            Number = room.Number,
            Type = room.Type,
            Price = room.Price
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(RoomViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var room = new Room
        {
            Id = model.Id,
            Number = model.Number,
            Type = model.Type,
            Price = model.Price
        };

        await _roomRepository.UpdateAsync(room);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var room = await _roomRepository.GetByIdAsync(id);
        if (room == null) return NotFound();

        var viewModel = new RoomViewModel
        {
            Id = room.Id,
            Number = room.Number,
            Type = room.Type,
            Price = room.Price
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _roomRepository.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
