using TechChallenge.Application.Interfaces;
using TechChallenge.Application.Services;

namespace TechChallenge.Api.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IContactService, ContactService>();

            return services;
        }
    }
}
