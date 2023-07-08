using CarCatalogue.Models.Response;
using CarCatalogue.Services.Contracts;

namespace CarCatalogue.Services
{
    public class CarApiService : ICarApiService
    {
        private readonly ILogger<ICarApiService> _logger;
        private readonly ICarStorageService _carStorageService;

        public CarApiService(ILogger<ICarApiService> logger, ICarStorageService carStorageService)
        {
            _logger = logger;
            _carStorageService = carStorageService;
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
                Year = car.Year,
                ImageUrl = car.ImageUrl,
            };
        }
    }
}
