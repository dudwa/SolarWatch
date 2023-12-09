using System.Text.Json;
using SolarWatch.Model;

namespace SolarWatch.Service.CoordinatesToSolar;

public class CoordinateToSolarConverter
{
    private HttpClient client = new();

    public async Task<string> ConvertCoordinatesToSolar(Coordinate coordinate, DateTime date, string sunsetOrSunrise)
    {
        try
        {
            string url = $"https://api.sunrise-sunset.org/json?lat={coordinate.Lat}&lng={coordinate.Lon}&date={date.Year}-{date.Month}-{date.Day}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                using (JsonDocument doc = JsonDocument.Parse(content))
                {
                    var root = doc.RootElement;
                    return root.GetProperty("results").GetProperty(sunsetOrSunrise).GetString();
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return "{}";
    }
}