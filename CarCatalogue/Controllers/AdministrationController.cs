using CarCatalogue.Common.Constants;
using CarCatalogue.Models.Request;
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

        /// <summary>
        /// Creates a car in the database
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Updates a car in the database
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IActionResult> UpdateCar(CarRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _carApiService.UpdateAsync(request);
                return Redirect(nameof(GetPaginatedAndQueriedCars));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ErrorMessages.GENERIC_ERROR_MESSAGE);
            }
        }

        /// <summary>
        /// Deletes a car in the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteCar(int id)
        {
            try
            {
                await _carApiService.Remove(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets a paginated list of cars, filtered, if filter query is present, if it's not, it gets the 12 most recently added records.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="searchQuery"></param>
        /// <returns></returns>
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
