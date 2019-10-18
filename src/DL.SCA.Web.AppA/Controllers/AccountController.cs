using DL.SCA.Security.Entities;
using DL.SCA.Web.AppA.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace DL.SCA.Web.AppA.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly LinkGenerator _linkGenerator;

        public AccountController(UserManager<AppUser>  userManager,
            SignInManager<AppUser> signInManager,
            LinkGenerator linkGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _linkGenerator = linkGenerator;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            var vm = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(vm);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
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
                    return Redirect(Url.IsLocalUrl(model.ReturnUrl)
                        ? model.ReturnUrl
                        : _linkGenerator.GetUriByAction("index", "home", new { area = "" },
                            Request.Scheme, Request.Host));
                }

                ModelState.AddModelError("", "The information you entered does not match our records, please try again.");
                return View(model);
            }

            ModelState.AddModelError("", "There is something wrong when you login. Please try again later.");
            return View(model);
        }
    }
}