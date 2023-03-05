using BuberDinner.Api.Common.Mapping;

namespace BuberDinner.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresintation(this IServiceCollection services)
        {

            services.AddMapping();
            return services;
        }
    }
}