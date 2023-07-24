using CarCatalogue.Models.Response;
using CarCatalogue.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace CarCatalogue.Controllers
{
    public class MakeController : Controller
    {
        private readonly ICarApiService _carApiService;
        private readonly IDistributedCache _distributedCache;

        public MakeController(ICarApiService carApiService, IDistributedCache distributedCache)
        {
            _carApiService = carApiService;
            _distributedCache = distributedCache;
        }

        public IActionResult Index(string make, int page = 1, string searchQuery = "")
        {
            var key = $"cars-{make}-{page}-{searchQuery}";
            string? value = _distributedCache.GetString(key);

            PaginatedCarsResponseModel? carMakeCollection;

            if (!string.IsNullOrEmpty(value))
            {
                carMakeCollection = JsonConvert.DeserializeObject<PaginatedCarsResponseModel>(value);
            }
            else
            {
                carMakeCollection = _carApiService.GetPaginatedAndFilteredCars(searchQuery, page, 6, make, true, false);

                _distributedCache.SetString(key, JsonConvert.SerializeObject(carMakeCollection), new DistributedCacheEntryOptions()
                {
                    SlidingExpiration = new TimeSpan(1, 0, 0)
                });
            }

            return View(carMakeCollection);
        }
    }
}
