namespace HotelManagement.WebApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin, Manager")]
public class ReservationController : Controller
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IGuestRepository _guestRepository;
    private readonly IRoomRepository _roomRepository;

    public ReservationController(
        IReservationRepository reservationRepository,
        IGuestRepository guestRepository,
        IRoomRepository roomRepository)
    {
        _reservationRepository = reservationRepository;
        _guestRepository = guestRepository;
        _roomRepository = roomRepository;
    }

    [Authorize(Roles = "Admin, Manager, Staff")]
    public async Task<IActionResult> Index()
    {
        var reservations = await _reservationRepository.GetAllAsync();
        var viewModel = reservations.Select(r => new ReservationViewModel
        {
            Id = r.Id,
            BookingGuestId = r.GuestId,
            GuestName = r.Guest!.Name,
            RoomId = r.RoomId,
            RoomNumber = r.Room!.Number,
            StartData = r.StartData,
            EndData = r.EndData,
            Status = r.Status
        });
        return View(viewModel);
    }

    [Authorize(Roles = "Admin, Manager")]
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _reservationRepository.DeleteAsync(id);
        return Json(new { success = true, message = "Reservation deleted successfully" });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ReservationCreateViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            // check if the room is available
            var isRoomAvailable = await _reservationRepository.IsRoomAvailableAsync(
                viewModel.RoomId, viewModel.StartData!.Value, viewModel.EndData!.Value);
            if (!isRoomAvailable)
            {
                ModelState.AddModelError(nameof(viewModel.RoomId), "Room is not available for the selected dates.");
            }
            else
            {
                var reservation = new Reservation
                {
                    GuestId = viewModel.BookingGuestId,
                    RoomId = viewModel.RoomId,
                    StartData = viewModel.StartData!.Value,
                    EndData = viewModel.EndData!.Value,
                    Status = viewModel.Status!.Value
                };

                await _reservationRepository.AddAsync(reservation);
                return RedirectToAction("Index");
            }
        }

        viewModel.Guests = await _guestRepository.GetAllAsync();
        viewModel.Rooms = await _roomRepository.GetAllAsync();
        return View(viewModel);
    }
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var viewModel = new ReservationCreateViewModel
        {
            Guests = await _guestRepository.GetAllAsync(),
            Rooms = await _roomRepository.GetAllAsync()
        };
        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        // Retrieve the reservation by ID
        var reservation = await _reservationRepository.GetByIdAsync(id);
        if (reservation == null)
        {
            return NotFound();
        }

        // Populate the view model with reservation data and dropdown lists
        var viewModel = new ReservationUpdateViewModel
        {
            Id = reservation.Id,
            BookingGuestId = reservation.GuestId,
            RoomId = reservation.RoomId,
            StartData = reservation.StartData,
            EndData = reservation.EndData,
            Status = reservation.Status,
            Guests = await _guestRepository.GetAllAsync(),
            Rooms = await _roomRepository.GetAllAsync()
        };
        return View(viewModel);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(ReservationUpdateViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var reservation = await _reservationRepository.GetByIdAsync(viewModel.Id);
            if (reservation == null)
            {
                return NotFound();
            }

            reservation.GuestId = viewModel.BookingGuestId;
            reservation.RoomId = viewModel.RoomId;
            reservation.StartData = viewModel.StartData;
            reservation.EndData = viewModel.EndData;
            reservation.Status = viewModel.Status;

            await _reservationRepository.UpdateAsync(reservation);
            return RedirectToAction(nameof(Index));
        }

        viewModel.Guests = await _guestRepository.GetAllAsync();
        viewModel.Rooms = await _roomRepository.GetAllAsync();
        return View(viewModel);
    }
}
