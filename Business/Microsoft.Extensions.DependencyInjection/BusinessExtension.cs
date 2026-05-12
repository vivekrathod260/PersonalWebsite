using Business.Services;
using Business.Services.Implementation;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class BusinessExtension
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
                // .AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
