using System.Text.Json;
using SolarWatch.Model;

namespace SolarWatch.Service.CityToCoordinates;

public class CityToCoordinatesConverter
{
    private HttpClient client = new();

    public async Task<Coordinate> ConvertCityToCoordinates(string city)
    {
        try
        {
            string url = $"http://api.openweathermap.org/geo/1.0/direct?q={city}&limit=1&appid={ApiKey.key}";
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                using (JsonDocument doc = JsonDocument.Parse(content))
                {
                    var root = doc.RootElement;
                    if (root.EnumerateArray().Any())
                    {
                        var firstElement = root.EnumerateArray().First();
                        return new Coordinate(firstElement.GetProperty("lat").GetDouble(),
                            firstElement.GetProperty("lon").GetDouble());
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return new Coordinate(-360, 0);
    }
}