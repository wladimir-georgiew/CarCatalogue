using CarCatalogue.Models;
using CarCatalogue.Models.Response;
using CarCatalogue.Services.Contracts;

using Microsoft.AspNetCore.Mvc;

namespace CarCatalogue.Controllers
{
    [Route("car")]
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
                var car = await _carApiService.GetByIdAsync(id);
                var response = new BaseResponse<CarResponseModel>
                {
                    Content = car
                };

                return View(response);
            }
            catch (Exception ex)
            {
                var response = new BaseResponse<CarResponseModel>(false)
                {
                    ErrorMessage = ex.Message
                };

                return View(response);
            }
        }
    }
}