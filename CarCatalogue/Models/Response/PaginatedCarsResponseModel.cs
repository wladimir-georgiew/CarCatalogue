using CarCatalogue.Common.Contracts;

namespace CarCatalogue.Models.Response
{
    public class PaginatedCarsResponseModel : IPagination
    {
        public int PageIndex { get; set; }

        public int TotalPages { get; set; }

        public int[] ThreePreviousPages { get; set; }

        public int[] ThreeNextPages { get; set; }

        public bool HasPreviousPage { get; set; }

        public bool HasNextPage { get; set; }

        public IEnumerable<CarResponseModel>? Items { get; set; }
    }
}
