using AutoMapper;
using models = Data.Expense.Models;
using Data.Expense.ViewModels.Create;
using Data.Expense.ViewModels.Read;

namespace Data.Mapping
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            // CreateMap<ClassA, ClassAView>().ReverseMap();
            CreateMap<models.Currency, ReadCurrency>().ReverseMap();
            CreateMap<models.Currency, CreateCurrency>().ReverseMap();
            CreateMap<models.Currency, UpdateCurrency>().ReverseMap();

            CreateMap<models.Category, CreateCategory >().ReverseMap();
            CreateMap<models.Expense, CreateExpense>().ReverseMap();


        }
    }
}
