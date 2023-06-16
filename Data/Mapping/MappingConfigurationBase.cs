using AutoMapper;
using Data.Users;

namespace Data.Mapping
{
    public class MappingConfigurationBase : Profile
    {
        public MappingConfigurationBase()
        {
            // CreateMap<ClassA, ClassAView>().ReverseMap();
            CreateMap<CoreUser, CreateUser>().ReverseMap();
        }
    }
}
