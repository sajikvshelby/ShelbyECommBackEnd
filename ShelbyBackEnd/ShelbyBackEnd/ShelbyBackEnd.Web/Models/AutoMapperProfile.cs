using AutoMapper;
using ShelbyBackEnd.Infrastructure.Models;
using System.Reflection;

namespace ShelbyBackEnd.Web.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
            CreateMap<Select_Search_ProductsResult, Select_All_Products_ListResult>()
                // Add explicit members if names differ:
                // .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.id))
                ;
            // other maps...
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping") ??
                                 type.GetInterface("IMapFrom`1").GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}

