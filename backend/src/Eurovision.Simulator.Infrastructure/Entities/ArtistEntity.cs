using CsvHelper.Configuration.Attributes;

namespace Eurovision.Simulator.Infrastructure.Entities;

public sealed class ArtistEntity
{
    public required string Name { get; set; }

    [Name("Country Code")]
    public required string CountryCode { get; set; }
}
