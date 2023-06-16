using Data.Contracts;

namespace Data.Models
{
    public class ModelBase : IEntity
    {
        public int Id { get; set; }

        // Audit fields
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime Updated { get; set; }
    }
}
