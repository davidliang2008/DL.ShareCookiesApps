using Microsoft.AspNetCore.Mvc;

namespace DL.SCA.Web.AppB.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}