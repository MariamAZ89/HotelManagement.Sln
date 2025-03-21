namespace HotelManagement.WebApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin, Manager")]
public class GuestController : Controller
{
    private readonly IGuestRepository _guestRepository;

    public GuestController(IGuestRepository guestRepository)
    {
        _guestRepository = guestRepository;
    }

    public async Task<IActionResult> Index()
    {
        var guests = await _guestRepository.GetAllAsync();
        var viewModel = guests.Select(g => new GuestViewModel
        {
            Id = g.Id,
            Name = g.Name,
            Email = g.Email,
            PhoneNumber = g.Phone
        }).ToList();

        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(GuestViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var guest = new Guest
        {
            Name = model.Name,
            Email = model.Email,
            Phone = model.PhoneNumber
        };

        await _guestRepository.AddAsync(guest);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var guest = await _guestRepository.GetByIdAsync(id);
        if (guest == null) return NotFound();

        var model = new GuestViewModel
        {
            Id = guest.Id,
            Name = guest.Name,
            Email = guest.Email,
            PhoneNumber = guest.Phone
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(GuestViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var guest = new Guest
        {
            Id = model.Id,
            Name = model.Name,
            Email = model.Email,
            Phone = model.PhoneNumber
        };

        await _guestRepository.UpdateAsync(guest);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var guest = await _guestRepository.GetByIdAsync(id);
        if (guest == null) return NotFound();

        var model = new GuestViewModel
        {
            Id = guest.Id,
            Name = guest.Name,
            Email = guest.Email,
            PhoneNumber = guest.Phone
        };

        return View(model);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var guest = await _guestRepository.GetByIdAsync(id);
        if (guest == null) return NotFound();

        var model = new GuestViewModel
        {
            Id = guest.Id,
            Name = guest.Name,
            Email = guest.Email,
            PhoneNumber = guest.Phone
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _guestRepository.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}

