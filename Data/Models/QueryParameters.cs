namespace Data.Models
{
    public class QueryParameters
    {
        public int StartIndex { get; set; }
        
        public int PageSize 
        {
            get => pageSize;
            set => pageSize = value;
        }
        private int pageSize = 20;
    }
}