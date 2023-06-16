using MODELS = Data.Expense.Models;
using Data.Mapping;
using Data.Expense.DTOs;

namespace Data.Expense.Mapping
{
    public class MappingConfiguration : MappingConfigurationBase
    {
        public MappingConfiguration()
        {
            // CreateMap<ClassA, ClassAView>().ReverseMap();
            CreateMap<MODELS.Currency, CurrencyDto>().ReverseMap();
            CreateMap<MODELS.Category, CategoryDto>().ReverseMap();
            CreateMap<MODELS.Expense, ExpenseDto>().ReverseMap();
        }
    }
}
