using Data;
using Data.Expense.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class ExpenseContext : CoreContext<CoreUser>
{
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Category> Categories { get; set; } 
    
    public ExpenseContext(DbContextOptions options, IConfiguration configuration) : base(options, configuration)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ExpensesConfiguration());
        modelBuilder.ApplyConfiguration(new CurrenciesConfiguration());
        modelBuilder.ApplyConfiguration(new CategoriesConfiguration());

    }
}