namespace CarCatalogue.Common.Contracts
{
    public interface IPagination
    {
        public int PageIndex { get; set; }

        public int TotalPages { get; set; }

        public int[] ThreePreviousPages { get; set; }

        public int[] ThreeNextPages { get; set; }

        public bool HasPreviousPage { get; set; }

        public bool HasNextPage { get; set; }
    }
}
