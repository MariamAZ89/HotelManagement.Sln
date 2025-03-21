namespace HotelManagement.WebApp.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Reservation", new { area = "Admin" });
        }
        ModelState.AddModelError("", "Invalid login attempt.");
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }
}
