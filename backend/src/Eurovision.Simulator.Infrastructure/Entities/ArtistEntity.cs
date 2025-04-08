using CsvHelper.Configuration.Attributes;

namespace Eurovision.Simulator.Infrastructure.Entities;

public sealed record ArtistEntity(string Name, [Name("Country Code")] string CountryCode);
