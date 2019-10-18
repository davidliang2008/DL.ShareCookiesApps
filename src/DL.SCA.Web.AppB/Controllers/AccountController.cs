using DL.SCA.Security.Entities;
using DL.SCA.Web.AppB.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DL.SCA.Web.AppB.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            var vm = new LoginViewModel
            {
                Email = "david.liang@outlook.com",
                ReturnUrl = returnUrl
            };
            return View(vm);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                return LocalRedirect(model.ReturnUrl);
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "The email does not exist.");
                    return View(model);
                }

                var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                if (signInResult.Succeeded)
                {
                    return LocalRedirect(model.ReturnUrl);
                }

                ModelState.AddModelError("", "The information you entered does not match our records, please try again.");
                return View(model);
            }

            ModelState.AddModelError("", "There is something wrong when you login. Please try again later.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("index", "home", new { area = "" });
        }
    }
}