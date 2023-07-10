using CarCatalogue.Common;
using CarCatalogue.Data.Entities;
using CarCatalogue.Extensions;
using CarCatalogue.Models.Request;
using CarCatalogue.Models.Response;
using CarCatalogue.Services.Contracts;

namespace CarCatalogue.Services
{
    public class CarApiService : ICarApiService
    {
        private readonly ILogger<ICarApiService> _logger;
        private readonly ICarStorageService _carStorageService;
        private readonly IDropboxService _dropboxService;

        public CarApiService(
            ILogger<ICarApiService> logger,
            ICarStorageService carStorageService,
            IDropboxService dropboxService)
        {
            _logger = logger;
            _carStorageService = carStorageService;
            _dropboxService = dropboxService;
        }

        public async Task<CarResponseModel?> GetByIdAsync(int id)
        {
            var car = await _carStorageService.GetByIdAsync(id);

            if (car == null)
            {
                _logger.LogWarning($"Car with id {id} not found in the database.");
                throw new Exception("Car not found.");
            }

            return new CarResponseModel
            {
                Id = car.Id,
                Make = car.Make,
                Model = car.Model,
                Acceleration = car.Acceleration,
                Horsepower = car.Horsepower,
                Weight = car.Weight,
                Year = car.Year.Value.Year,
                ImageUrl = car.ImageUrl,
            };
        }

        public async Task AddAsync(CarRequestModel request)
        {
            var imageUrl = await this.UploadImageToDropbox(request);

            var car = new Car()
            {
                Make = request.Make,
                Model = request.Model,
                Weight= request.Weight,
                Acceleration= request.Acceleration,
                Horsepower= request.Horsepower,
                Year = request.Year!.ToDateTimeYear(),
                ImageUrl = imageUrl,
                CreatedOrModifiedOn = DateTime.Now,
            };

            await _carStorageService.AddAsync(car);
            await _carStorageService.SaveChangesAsync();
        }

        public async Task UpdateAsync(CarRequestModel request)
        {
            var car = await _carStorageService.GetByIdAsync(request.Id);
            car.Make = request.Make;
            car.Model = request.Model;
            car.Weight = request.Weight;
            car.Acceleration = request.Acceleration;
            car.Horsepower = request.Horsepower;
            car.Year = request.Year!.ToDateTimeYear();
            car.CreatedOrModifiedOn = DateTime.Now;

            if (request.Image != null)
            {
                var t = request.Image.GetExtension();
                car.ImageUrl = await this.UploadImageToDropbox(request);
            }

            await _carStorageService.SaveChangesAsync();
        }

        public IEnumerable<Car> GetAll() => _carStorageService.GetAllWithoutDeleted();

        public PaginatedCarsResponseModel? GetPaginatedAndFilteredCars(string searchQuery, int page, int itemsPerPage = 12, string? makeFilter = null, bool prioritiseCarsWithImages = false, bool includeMakeFilter = true)
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

            var cars = this
                .GetAll()
                .Where(x => makeFilter != null
                            ? x.Make.ToLower() == makeFilter.ToLower()
                            : true)
                .Where(searchQuery.Length > 5 ? filterByStartsWith : filterByContains)
                .OrderByDescending(car => prioritiseCarsWithImages
                                            ? (!string.IsNullOrEmpty(car.ImageUrl) &&
                                               !car.ImageUrl.StartsWith("/default-images"))
                                            : true)
                .ThenByDescending(x => x.CreatedOrModifiedOn);

            var paginatedCars = PaginationList<Car>.Create(cars, page, itemsPerPage);

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
                    Year = car.Year.Value.Year,
                    ImageUrl = car.ImageUrl,
                }),
            };

            return viewModel;
        }

        public IEnumerable<CarResponseModel>? GetMostRecentCars(int count, bool prioritiseCarsWithImages = false, bool distinctByCar = false)
        {
            return _carStorageService.GetAllWithoutDeleted()
                .OrderByDescending(car => prioritiseCarsWithImages
                                            ? (!string.IsNullOrEmpty(car.ImageUrl) &&
                                               !car.ImageUrl.StartsWith("/default-images"))
                                            : true)
                .ThenByDescending(car => car.CreatedOrModifiedOn)
                .DistinctBy(car => distinctByCar
                                     ? car.Make
                                     : car.Id.ToString())
                .Take(count)
                .Select(car => new CarResponseModel
                {
                    Id = car.Id,
                    Make = car.Make,
                    Model = car.Model,
                    Acceleration = car.Acceleration,
                    Horsepower = car.Horsepower,
                    Weight = car.Weight,
                    Year = car.Year.Value.Year,
                    ImageUrl = car.ImageUrl,
                })
                .ToList();
        }

        public async Task Remove(int id)
        {
            var car = await _carStorageService.GetByIdAsync(id);

            if (car == null)
            {
                _logger.LogWarning($"Car with id {id} not found in the database.");
                throw new Exception("Car not found.");
            }

            car.DeletedOn = DateTime.Now;

            await _carStorageService.SaveChangesAsync();
        }

        private async Task<string?> UploadImageToDropbox(CarRequestModel request)
        {
            return await _dropboxService.UploadAsync("images", $"{request.Make}_{request.Model}.{request.Image.GetExtension()}", request.Image);
        }
    }
}
