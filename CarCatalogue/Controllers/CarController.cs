using CarCatalogue.Common.Constants;
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

        /// <summary>
        /// Gets a car by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="view">If set to false returns the json object instead of a view</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [Route("{id}/{view}")]
        public async Task<IActionResult> Car(int id, bool view = true)
        {
            try
            {
                var carResponse = await _carApiService.GetByIdAsync(id);

                if (!view)
                {
                    return Ok(carResponse);
                }

                return View(carResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// <summary>
        /// Gets the 12 most recently updated/added cars for sole purpose of ajax updates.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("recent")]
        public IActionResult GetMostRecentCars()
        {
            try
            {
                var cars = _carApiService.GetMostRecentCars(12);

                return Ok(cars);
            }
            catch (Exception _)
            {
                return BadRequest(ErrorMessages.GENERIC_ERROR_MESSAGE);
            }
        }
    }
}