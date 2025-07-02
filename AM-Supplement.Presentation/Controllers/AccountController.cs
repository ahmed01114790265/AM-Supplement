using AM_Supplement.Presentation.ViewsModel.AccountViewModel;
using AMSupplement.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using System.Threading.Tasks;

namespace AM_Supplement.Presentation.Controllers
{
    
    public class AccountController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        RoleManager<ApplicationRole> _roleManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [HttpGet]
      public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO userModel)
        {
            ApplicationUser user = new ApplicationUser();
            user.UserName = userModel.Email;
            user.Email = userModel.Email;
            var result = await _userManager.CreateAsync(user,userModel.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("LogIn");
            }
            else
            {
                ViewBag.Error = "Please input valid Emial or Password";
                return View(userModel);
            }
        }
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInDTO userModel)
        {

            var result = await _signInManager.PasswordSignInAsync( userModel.Email, userModel.Password, isPersistent: true, lockoutOnFailure: false);
            if(result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = "invalid login";
                return View(userModel);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("LogIn");
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO model)
        {
           
            var user = await _userManager.FindByEmailAsync(model.Email);
            bool Loggedin = await _userManager.CheckPasswordAsync(user, model.CurrentPassword);
            if(!Loggedin)
            {
                ViewBag.errorpassword = " passowrd is not valid";
                return View(model);
            }
            

         var result=   await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                return RedirectToAction("LogIn");
            }
            else
            {
                ViewBag.error = "new password dosnot succeed pleas try again"; 
                return View(model);
            }
        }
     
    }
}
