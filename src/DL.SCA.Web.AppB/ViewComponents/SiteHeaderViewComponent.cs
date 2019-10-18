using DL.SCA.Security.Entities;
using DL.SCA.Web.AppB.ViewComponents.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DL.SCA.Web.AppB.ViewComponents
{
    public class SiteHeaderViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public SiteHeaderViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var vm = new SiteHeaderViewModel
            {
                LoggedInUserDisplayName = await GetLoggedInUserDisplayName()
            };

            return View(vm);
        }

        private async Task<string> GetLoggedInUserDisplayName()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return "Visitor";
            }

            var loggedInUser = await _userManager.FindByNameAsync(User.Identity.Name);

            return loggedInUser == null
                ? User.Identity.Name
                : loggedInUser.DisplayName;
        }
    }
}
