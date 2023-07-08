using CarCatalogue.Common;
using CarCatalogue.Common.Constants;
using CarCatalogue.Data.Entities;
using CarCatalogue.Models.Response;
using CarCatalogue.Services.Contracts;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarCatalogue.Controllers
{
    [Authorize(Roles = Roles.ADMIN)]
    public class AdministrationController : Controller
    {
        private readonly ICarStorageService _carStorageService;

        public AdministrationController(ICarStorageService carStorageService)
        {
            _carStorageService = carStorageService;
        }

        public IActionResult CreateCar()
        {
            return View();
        }

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

            var productModels = _carStorageService
                .GetAll()
                .Where(searchQuery.Length > 5 ? filterByStartsWith : filterByContains);

            var paginatedCars = PaginationList<Car>.Create(productModels, page, 12);

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
