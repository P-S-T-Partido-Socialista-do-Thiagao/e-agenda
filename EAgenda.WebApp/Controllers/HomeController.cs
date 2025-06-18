using Microsoft.AspNetCore.Mvc;

namespace EAgenda.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
