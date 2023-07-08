using CarCatalogue.Common;
using CarCatalogue.Common.Constants;
using CarCatalogue.Data.Entities;
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

        public AdministrationController(ICarApiService carApiService)
        {
            _carApiService = carApiService;
        }

        public async Task<IActionResult> CreateCar(CarRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                // TODO ===========================================
                // Refactor by adding BaseReponse model to the view
                // TODO ===========================================
                return View(GetPaginatedAndQueriedCars());
            }

            try
            {
                await _carApiService.AddAsync(request);
            }
            catch (Exception)
            {
                throw;
            }

            return View(GetPaginatedAndQueriedCars());
        }

        // TODO ==================================================================================
        // Refactor by adding the logic to the CarApi service layer and return BaseResponse model.
        // Also refactor the _Pagination and ListAllCars views. ==================================
        // TODO ==================================================================================
        public IActionResult GetPaginatedAndQueriedCars(int page = 1, string searchQuery = "")
        {
            // Prevents from getting an error when clicking the search button with empty input ( ?searchQuery= )
            if (searchQuery is null)
            {
                searchQuery = string.Empty;
            }

            var isQueryNull = string.IsNullOrEmpty(searchQuery);
            var searchQueryLower = !isQueryNull ? searchQuery.ToLower() : searchQuery;

            Func<Car, bool> filterByStartsWith = (x) => !isQueryNull ?
            x.Model.ToLower().StartsWith(searchQueryLower) ||
            x.Make.ToLower().StartsWith(searchQueryLower) : true;

            Func<Car, bool> filterByContains = (x) => !isQueryNull ?
            x.Model.ToLower().Contains(searchQueryLower) ||
            x.Make.ToLower().Contains(searchQueryLower) : true;

            var cars = _carApiService
                .GetAll()
                .Where(searchQuery.Length > 5 ? filterByStartsWith : filterByContains)
                .OrderByDescending(x => x.CreatedOn);

            var paginatedCars = PaginationList<Car>.Create(cars, page, 12);

            var viewModel = new PaginatedCarsResponseModel
            {
                PageIndex = paginatedCars.PageIndex,
                ThreeNextPages = paginatedCars.ThreeNextPages,
                ThreePreviousPages = paginatedCars.ThreePreviousPages,
                TotalPages = paginatedCars.TotalPages,
                HasNextPage = paginatedCars.HasNextPage,
                HasPreviousPage = paginatedCars.HasPreviousPage,
                // The point of selecting here to change the return type to CarResponseModel instead of doing it directly at the paginatedCars is becase if we do it there we will be executing the .Select on all of the db records, but when we do it here we execute it only on the records we need (after the pagination).
                Items = paginatedCars.Select(car => new CarResponseModel
                {
                    Id = car.Id,
                    Make = car.Make,
                    Model = car.Model,
                    Acceleration = car.Acceleration,
                    Horsepower = car.Horsepower,
                    Weight = car.Weight,
                    Year = car.Year,
                    ImageUrl = car.ImageUrl,
                }),
            };

            return View("~/Views/Administration/ListAllCars.cshtml", viewModel);
        }
    }
}
