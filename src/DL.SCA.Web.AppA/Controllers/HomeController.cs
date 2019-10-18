using Microsoft.AspNetCore.Mvc;

namespace DL.SCA.Web.AppA.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}