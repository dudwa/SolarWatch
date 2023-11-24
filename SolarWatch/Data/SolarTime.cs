namespace SolarWatch.Data;

public class SolarTime
{
    public uint Id { get; init; }
    public uint CityId { get; init; }
    public string SunRise { get; init; }
    public string SunSet { get; init; }
    public DateTime Date { get; init; }
}