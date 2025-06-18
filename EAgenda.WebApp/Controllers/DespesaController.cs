using Microsoft.AspNetCore.Mvc;

namespace EAgenda.WebApp.Controllers
{
    public class DespesaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
