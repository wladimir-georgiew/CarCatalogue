using CarCatalogue.Common.Contracts;

namespace CarCatalogue.Common
{
    public class PaginationList<T> : List<T>, IPagination
    {
        public int PageIndex { get; set; }

        public int TotalPages { get; set; }

        public int[] ThreePreviousPages { get; set; }

        public int[] ThreeNextPages { get; set; }

        public PaginationList(List<T> items, int count, int pageIndex, int pageSize)
        {
            this.PageIndex = pageIndex;
            this.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.ThreePreviousPages = GetThreePreviousPageIndex(pageIndex);
            this.ThreeNextPages = GetThreeNextPageIndex(pageIndex, TotalPages);
            this.AddRange(items);
        }

        public bool HasPreviousPage
        {
            get { return this.PageIndex > 1; }
            set { }
        }

        public bool HasNextPage
        {
            get { return this.PageIndex < this.TotalPages; }
            set { }
        }

        public static PaginationList<T> Create(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginationList<T>(items, count, pageIndex, pageSize);
        }

        private int[] GetThreePreviousPageIndex(int currentPageNumber)
        {
            var pages = new int[3]
            {
                currentPageNumber - 3,
                currentPageNumber - 2,
                currentPageNumber - 1
            }
            .Where(x => x > 0)
            .ToArray();

            return pages;
        }

        private int[] GetThreeNextPageIndex(int currentPageNumber, int totalPageCount)
        {
            var pages = new int[3]
            {
                currentPageNumber + 1,
                currentPageNumber + 2,
                currentPageNumber + 3
            }
            .Where(x => x <= totalPageCount)
            .ToArray();

            return pages;
        }
    }
}
