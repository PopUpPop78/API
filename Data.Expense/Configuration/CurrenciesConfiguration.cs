using Data.Expense.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CurrenciesConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.HasData(
            new Currency
            {
                Id = 1,
                Name = "Swiss Franc",
                Code = "CHF"
            },
            new Currency
            {
                Id = 2,
                Name = "British Pound",
                Code = "GBP"
            }
        );
    }
}