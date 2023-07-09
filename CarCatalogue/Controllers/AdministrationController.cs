using CarCatalogue.Common.Constants;
using CarCatalogue.Models.Request;
using CarCatalogue.Models.Response;
using CarCatalogue.Services.Contracts;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarCatalogue.Controllers
{
    [Authorize(Roles = Roles.ADMIN)]
    public class AdministrationController : Controller
    {
        private readonly ICarApiService _carApiService;
        private readonly ILogger<AdministrationController> _logger;

        public AdministrationController(
            ICarApiService carApiService,
            ILogger<AdministrationController> logger)
        {
            _carApiService = carApiService;
            _logger = logger;
        }

        public async Task<IActionResult> CreateCar(CarRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _carApiService.AddAsync(request);
                return Redirect(nameof(GetPaginatedAndQueriedCars));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ErrorMessages.GENERIC_ERROR_MESSAGE);
            }
        }

        public IActionResult GetPaginatedAndQueriedCars(int page = 1, string searchQuery = "")
        {
            var viewPath = "~/Views/Administration/ListAllCars.cshtml";

            try
            {
                var viewModel = _carApiService.GetPaginatedAndFilteredCars(searchQuery, page);
                return View(viewPath, viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ErrorMessages.GENERIC_ERROR_MESSAGE);
            }
        }
    }
}
