using SolarWatch.Controllers;
using SolarWatch.Model;
using SolarWatch.Service.CoordinatesToSolar;
using SolarWatch.Service.CityToCoordinates;

namespace SolarWatchTest;

public class SolarControllerTest
{

    private SolarController _solarController;

    [SetUp]
    public void SetUp()
    {
        _solarController = new SolarController();
    }

    [Test]
    public async Task CorrectInputTest1()
    {
        var solarDat1 = await _solarController.GetSunrise("szeged", new DateTime(2023, 05, 20));
        
        Assert.That(solarDat1.City, Is.EqualTo("szeged"));
    }
}