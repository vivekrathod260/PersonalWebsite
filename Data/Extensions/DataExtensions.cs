using Data.Configuration;
using Data.Repositories;

namespace Microsoft.Extensions.DependencyInjection;

public static class DataExtensions
{
    public static IServiceCollection AddDataServices(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
    {
        services.Configure<FirebaseSettings>(configuration.GetSection(FirebaseSettings.SectionName));
        services.AddSingleton<IFirestoreRepository, FirestoreRepository>();
        return services;
    }
}
