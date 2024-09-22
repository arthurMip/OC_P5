using ExpressVoitures.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExpressVoitures.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(SignInManager<IdentityUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public IActionResult Index()
    {
        if (User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Car");
        }

        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        return RedirectToAction("Index", "Car");
    }

    public IActionResult Register()
    {
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index");
    }
}
