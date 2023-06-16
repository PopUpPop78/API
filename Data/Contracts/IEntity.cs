using System.ComponentModel.DataAnnotations;

namespace Data.Contracts
{
    public interface IEntity
    {
        [Key]
        int Id { get; }

        // Audit fields
        string CreatedBy { get; set; }
        DateTime Created { get; set; }
        string UpdatedBy { get; set; }
        DateTime Updated { get; set; }
    }
}
