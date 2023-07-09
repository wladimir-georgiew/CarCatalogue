using CarCatalogue.Data.Entities;

namespace CarCatalogue.Services.Contracts
{
    public interface ICarStorageService
    {
        Task<Car?> GetByIdAsync(int id);
        IEnumerable<Car> GetAll();
        IEnumerable<Car> GetAllWithoutDeleted();
        Task AddAsync(Car car);
        void Update(Car car);
        void HardDelete(Car car);
        Task SaveChangesAsync();
    }
}
