using Data.Expense.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CategoriesConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasData(
            new Category
            {
                Id = 1,
                Name = "Rent",
                Description = "Rent paid on non-commercial properties"
            },
            new Category
            {
                Id = 2,
                Name = "Child Support",
                Description = "Child support"
            },
            new Category
            {
                Id = 3,
                Name = "TeleComms",
                Description = "Mobile | TV | Internet"
            }
        );
    }
}