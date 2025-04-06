using Eurovision.Simulator.Api.Countries;

namespace Eurovision.Simulator.Api.Contests;

public interface IContestRepository
{
    Task<IReadOnlyCollection<Country>> GetParticipatingCountries(int year, CancellationToken cancellationToken);
}

public class ContestRepository : IContestRepository
{
    public Task<IReadOnlyCollection<Country>> GetParticipatingCountries(int year, CancellationToken cancellationToken) =>
        throw new NotImplementedException();
}
