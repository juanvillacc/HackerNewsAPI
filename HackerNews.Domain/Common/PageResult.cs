namespace HackerNews.Domain.Common
{
    public class PageResult<T>
    {
        public List<T> Page { get; set; }

        public long ResultsCount { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
