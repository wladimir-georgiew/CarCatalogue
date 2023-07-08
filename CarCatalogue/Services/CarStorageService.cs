﻿using CarCatalogue.Data;
using CarCatalogue.Data.Entities;
using CarCatalogue.Services.Contracts;

using Microsoft.EntityFrameworkCore;

namespace CarCatalogue.Services
{
    public class CarStorageService : ICarStorageService
    {
        private readonly ApplicationDbContext _context;

        public CarStorageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Car?> GetByIdAsync(int id) => await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);

        public IEnumerable<Car> GetAll() => _context.Cars;

        public async Task AddAsync(Car car) => await _context.Cars.AddAsync(car);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
