using CarCatalogue.Data.Entities;

namespace CarCatalogue.Services.Contracts
{
    public interface ICarStorageService
    {
        Task<Car?> GetByIdAsync(int id);
        IEnumerable<Car> GetAll();
        Task AddAsync(Car car);
        Task SaveChangesAsync();
    }
}
