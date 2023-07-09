﻿using CarCatalogue.Data.Entities;
using CarCatalogue.Models.Request;
using CarCatalogue.Models.Response;

namespace CarCatalogue.Services.Contracts
{
    public interface ICarApiService
    {
        Task<CarResponseModel?> GetByIdAsync(int id);
        Task AddAsync(CarRequestModel request);
        IEnumerable<Car> GetAll();
        PaginatedCarsResponseModel? GetPaginatedAndFilteredCars(string searchQuery, int page);
    }
}
