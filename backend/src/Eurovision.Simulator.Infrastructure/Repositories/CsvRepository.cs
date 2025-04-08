using System.Globalization;
using System.Linq.Expressions;
using CsvHelper;
using CsvHelper.Configuration;
using Eurovision.Simulator.Infrastructure.Services;

namespace Eurovision.Simulator.Infrastructure.Repositories;

public sealed class CsvRepository<T>(string csvFilePath, ICacheService cacheService) : ITableRepository<T>
    where T : class
{
    public Task<IReadOnlyCollection<T>> GetAll(CancellationToken cancellationToken) =>
        cacheService.Get<IReadOnlyCollection<T>>(
            csvFilePath,
            async () =>
            {
                using var reader = GetCsvReader(csvFilePath);
                return await reader.GetRecordsAsync<T>(cancellationToken).ToListAsync(cancellationToken);
            }
        );

    public async Task<T> Get(Expression<Func<T, bool>> filter, CancellationToken cancellationToken) =>
        (await GetAll(cancellationToken)).Single(filter.Compile());

    private static CsvReader GetCsvReader(string csvFilePath)
    {
        var reader = new StreamReader(csvFilePath);
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
            HasHeaderRecord = true
        };

        return new CsvReader(reader, config);
    }
}
