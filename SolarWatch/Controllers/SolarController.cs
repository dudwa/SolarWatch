using Microsoft.AspNetCore.Mvc;
using SolarWatch.Model;
using SolarWatch.Service.CityToCoordinates;
using SolarWatch.Service.CoordinatesToSolar;

namespace SolarWatch.Controllers;

[ApiController]
[Route("[controller]")]
public class SolarController : ControllerBase
{
    
    [HttpGet("GetSunrise/",Name = "/GetSunrise")]
    public async Task<SolarData> GetSunrise(string city, DateTime date)
    {
        return await GetSolarData(city, date, "sunrise");
    }
    
    [HttpGet("GetSunset/",Name = "/GetSunset")]
    public async Task<SolarData> GetSunset(string city, DateTime date)
    {
        return await GetSolarData(city, date, "sunset");
    }

    private async Task<SolarData> GetSolarData(string city, DateTime date, string sunsetOrSunrise)
    {
        var coordConverter = new CityToCoordinatesConverter();
        var solarConverter = new CoordinateToSolarConverter();

        var coordinates = await coordConverter.ConvertCityToCoordinates(city);

        if (coordinates.Lat == -360)
        {
            coordinates = new Coordinate(46.253, 20.14824);
            city = "szeged";
        }

        var sunriseTime = await solarConverter.ConvertCoordinatesToSolar(coordinates, date, sunsetOrSunrise);

        if (sunriseTime == "{}")
        {
            date = DateTime.Today;
        }

        sunriseTime = await solarConverter.ConvertCoordinatesToSolar(coordinates, date, sunsetOrSunrise);

        var solarData = new SolarData(city.ToLower(), new Coordinate(coordinates.Lat, coordinates.Lon), sunsetOrSunrise,
            date.ToString("yyyy-MM-dd"), sunriseTime);

        return solarData;

    }
}