using CarCatalogue.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CarCatalogue.Controllers
{
    public class MakeController : Controller
    {
        private readonly ICarApiService _carApiService;

        public MakeController(ICarApiService carApiService)
        {
            _carApiService = carApiService;
        }

        public IActionResult Index(string make, int page = 1, string searchQuery = "")
        {
            var carMakeCollection = _carApiService.GetPaginatedAndFilteredCars(searchQuery, page, 6, make, true, false);

            return View(carMakeCollection);
        }
    }
}
