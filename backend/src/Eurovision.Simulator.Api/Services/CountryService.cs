using Eurovision.Simulator.Api.Models;
using Eurovision.Simulator.Infrastructure;
using Eurovision.Simulator.Infrastructure.Entities;
using Eurovision.Simulator.Infrastructure.Repositories;

namespace Eurovision.Simulator.Api.Services;

public interface ICountryService
{
    Task<IReadOnlyCollection<Country>> GetAllCountries(CancellationToken cancellationToken);
}

public class CountryService(ITableRepository<CountryEntity> repository) : ICountryService
{
    public async Task<IReadOnlyCollection<Country>> GetAllCountries(CancellationToken cancellationToken) =>
    [
        ..(await repository.GetAll(cancellationToken)).Select(a => new Country(a.Name, a.Code))
    ];
}
