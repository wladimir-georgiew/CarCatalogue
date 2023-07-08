using CarCatalogue.Models.Response;

namespace CarCatalogue.Services.Contracts
{
    public interface ICarApiService
    {
        Task<CarResponseModel?> GetByIdAsync(int id);
    }
}
