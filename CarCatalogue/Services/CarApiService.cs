using CarCatalogue.Data.Entities;
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
                Year = car.Year,
                ImageUrl = car.ImageUrl,
            };
        }

        public async Task AddAsync(CarRequestModel request)
        {
            var imageUrl = await _dropboxService.UploadAsync("images", $"{request.Make}_{request.Model}", request.Image);

            var car = new Car()
            {
                Make = request.Make,
                Model = request.Model,
                Weight= request.Weight,
                Acceleration= request.Acceleration,
                Horsepower= request.Horsepower,
                Year = new DateTime(Convert.ToInt32(request.Year)),
                ImageUrl = imageUrl,
                CreatedOn = DateTime.Now,
            };

            await _carStorageService.AddAsync(car);
            await _carStorageService.SaveChangesAsync();
        }

        public IEnumerable<Car> GetAll() => _carStorageService.GetAll();
    }
}
