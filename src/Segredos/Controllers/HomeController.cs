using Microsoft.AspNetCore.Mvc;

namespace Segredos.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}