using AM_Supplement.Presentation.ViewsModel.AccountViewModel;
using AM_Supplement.Shared.Localization;
using AMSupplement.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IStringLocalizer<SharedResource> _localizer;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<ApplicationRole> roleManager,
        IStringLocalizer<SharedResource> localizer)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _localizer = localizer;
    }

    // GET: Register page
    [HttpGet]
    public IActionResult Register() => View();

    // POST: Register
    [HttpPost]
    public async Task<IActionResult> Register(RegisterDTO model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            TempData["Message"] = _localizer["RegistrationSuccess"].Value;
            return RedirectToAction("Index","Product");
        }

        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error.Description);

        return View(model);
    }

    // GET: Login page
    [HttpGet]
    public IActionResult LogIn() => View();

    // POST: Login
    [HttpPost]
    public async Task<IActionResult> LogIn(LogInDTO model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await _signInManager.PasswordSignInAsync(
            model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);

        if (result.Succeeded)
            return RedirectToAction("Index", "Product");

        ModelState.AddModelError(string.Empty, _localizer["InvalidLogin"]);
        return View(model);
    }

    // POST: Logout
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        TempData["Message"] = _localizer["LogoutSuccess"].Value;
        return RedirectToAction("LogIn");
    }

    // GET: Change password page
    [HttpGet]
    [Authorize]
    public IActionResult ChangePassword() => View();

    // POST: Change password
    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordDTO model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, _localizer["UserNotFound"]);
            return View(model);
        }

        var passwordCheck = await _userManager.CheckPasswordAsync(user, model.CurrentPassword);
        if (!passwordCheck)
        {
            ModelState.AddModelError(string.Empty, _localizer["InvalidCurrentPassword"].Value);
            return View(model);
        }

        var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        if (result.Succeeded)
        {
            await _signInManager.RefreshSignInAsync(user);
            TempData["Message"] = _localizer["PasswordChangeSuccess"].Value;
            return RedirectToAction("LogIn");
        }

        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error.Description);

        return View(model);
    }
}

