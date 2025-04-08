using Eurovision.Simulator.Infrastructure.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Eurovision.Simulator.Infrastructure;

public static class RegistrationExtensions
{
    public static void RegisterInfrastructureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMemoryCache()
            .AddSingleton(TimeProvider.System)
            .AddSingleton<ICacheService, CacheService>()
            .AddCsvRepository<ArtistEntity>("countries.csv")
            .AddCsvRepository<CountryEntity>("participants_2025.csv");
    }

    private static IServiceCollection AddCsvRepository<T>(this IServiceCollection serviceCollection, string csvFilePath)
        where T : class =>
        serviceCollection.AddScoped<ITableRepository<T>, CsvRepository<T>>(
            sb => new CsvRepository<T>(csvFilePath, sb.GetRequiredService<ICacheService>())
        );
}
