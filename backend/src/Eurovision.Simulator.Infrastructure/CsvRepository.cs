using System.Globalization;
using System.Linq.Expressions;
using CsvHelper;
using CsvHelper.Configuration;

namespace Eurovision.Simulator.Infrastructure;

public sealed class CsvRepository<T>(string csvFilePath, ICacheService cacheService) : ITableRepository<T>
    where T : class
{
    public Task<IReadOnlyCollection<T>> GetAll(CancellationToken cancellationToken) =>
        cacheService.Get<IReadOnlyCollection<T>>(
            csvFilePath,
            async () =>
            {
                using var reader = GetReader(csvFilePath);
                return await reader.GetRecordsAsync<T>(cancellationToken).ToListAsync(cancellationToken);
            }
        );

    public async Task<T> Get(Expression<Func<T, bool>> filter, CancellationToken cancellationToken) =>
        (await GetAll(cancellationToken)).Single(filter.Compile());

    private static CsvReader GetReader(string csvFilePath)
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
