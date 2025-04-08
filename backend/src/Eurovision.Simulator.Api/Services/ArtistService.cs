using Eurovision.Simulator.Api.Models;
using Eurovision.Simulator.Infrastructure;
using Eurovision.Simulator.Infrastructure.Entities;
using Eurovision.Simulator.Infrastructure.Repositories;

namespace Eurovision.Simulator.Api.Services;

public interface IArtistService
{
    Task<IReadOnlyCollection<Artist>> GetAllParticipants(CancellationToken cancellationToken);
}

public class ArtistService(ITableRepository<ArtistEntity> repository) : IArtistService
{
    public async Task<IReadOnlyCollection<Artist>> GetAllParticipants(CancellationToken cancellationToken) =>
    [
        ..(await repository.GetAll(cancellationToken)).Select(a => new Artist(a.Name, a.CountryCode))
    ];
}
