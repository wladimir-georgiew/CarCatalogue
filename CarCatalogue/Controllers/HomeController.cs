using Microsoft.AspNetCore.Mvc;

namespace CarCatalogue.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}