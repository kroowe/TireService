using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TireServiceApi.Automapper;

namespace TireServiceApi.Extensions
{
    public static class AutoMapperServiceCollectionExtensions
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
