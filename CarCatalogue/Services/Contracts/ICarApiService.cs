using CarCatalogue.Data.Entities;
using CarCatalogue.Models.Request;
using CarCatalogue.Models.Response;

namespace CarCatalogue.Services.Contracts
{
    public interface ICarApiService
    {
        Task<CarResponseModel?> GetByIdAsync(int id);
        Task AddAsync(CarRequestModel request);
        Task UpdateAsync(CarRequestModel request);
        IEnumerable<Car> GetAll();
        PaginatedCarsResponseModel? GetPaginatedAndFilteredCars(string searchQuery, int page, int itemsPerPage = 12, string? makeFilter = null, bool prioritiseCarsWithImages = false, bool includeMakeFilter = true);
        IEnumerable<CarResponseModel>? GetMostRecentCars(int count, bool prioritiseCarsWithImages = false, bool distinctByCar = false);
        Task Remove(int id);
    }
}
