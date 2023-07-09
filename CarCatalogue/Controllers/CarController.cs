using CarCatalogue.Services.Contracts;

using Microsoft.AspNetCore.Mvc;

namespace CarCatalogue.Controllers
{
    [Route("[controller]")]
    public class CarController : Controller
    {
        private readonly ICarApiService _carApiService;

        public CarController(ICarApiService carService)
        {
            _carApiService = carService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Car(int id)
        {
            try
            {
                var carResponse = await _carApiService.GetByIdAsync(id);
                return View(carResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}