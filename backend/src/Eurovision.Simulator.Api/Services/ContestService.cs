namespace Eurovision.Simulator.Api.Services;

public interface IContestService
{
    Task Run(CancellationToken cancellationToken);
}

public sealed class ContestService(
    ILogger<ContestService> logger,
    IArtistService artistService,
    ICountryService countryService
) : IContestService
{
    public async Task Run(CancellationToken cancellationToken)
    {
        var participants = await artistService.GetAllParticipants(cancellationToken);
        var countries = await countryService.GetAllCountries(cancellationToken);
        logger.LogInformation("All participants are: {Participants}", participants);
        logger.LogInformation("All countries are: {Countries}", countries);
    }
}
