namespace Data.Models
{
    public class PagedResults<T>
    {
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int RecordCount { get; set; }
        public IList<T> Items { get; set; }
    }
}