using CarCatalogue.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CarCatalogue.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarApiService _carApiService;

        public HomeController(ICarApiService carApiService)
        {
            _carApiService = carApiService;
        }

        public IActionResult Index()
        {
            var featuredCars = _carApiService.GetMostRecentCars(3, true, true);

            return View(featuredCars);
        }
    }
}