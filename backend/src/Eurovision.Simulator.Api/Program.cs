using Eurovision.Simulator.Api.Services;
using Eurovision.Simulator.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();
builder.Services
    .AddScoped<IArtistService, ArtistService>()
    .AddScoped<IContestService, ContestService>()
    .AddScoped<ICountryService, CountryService>()
    .RegisterInfrastructureServices();

var app = builder.Build();

app.MapPost(
    "/start",
    async ( /* parameters here */) =>
    {
        var contest = app.Services.GetRequiredService<IContestService>();
        await contest.Run(CancellationToken.None);
        Results.Ok("Let the Eurovision song contest begin!");
    }
);

app.UseHttpsRedirection();
app.Run();
