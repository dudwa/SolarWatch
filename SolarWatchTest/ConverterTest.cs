using SolarWatch.Model;
using SolarWatch.Service.CoordinatesToSolar;
using SolarWatch.Service.CityToCoordinates;

namespace SolarWatchTest;

public class ConverterTest
{

    private CityToCoordinatesConverter cityToCoord;
    private CoordinateToSolarConverter coordToSolar;
    [SetUp]
    public void SetUp()
    {
        cityToCoord = new CityToCoordinatesConverter();
        coordToSolar = new CoordinateToSolarConverter();
    }
    [Test]
    public async Task CorrectInputTest1()
    {
        var correctInput = await cityToCoord.ConvertCityToCoordinates("budapest");
        Assert.That(correctInput, Is.EqualTo(new Coordinate(47.4979937, 19.0403594)));
    }
    
    [Test]
    public async Task IncorrectInputTest1()
    {
        var incorrectInput = await cityToCoord.ConvertCityToCoordinates("dsjfkaldsf");
        Assert.That(incorrectInput, Is.EqualTo(new Coordinate(-360, 0)));
    }
    
    [Test]
    public async Task CorrectInputTest2()
    {
        var correctInput = await coordToSolar.ConvertCoordinatesToSolar(
            new Coordinate(47.4979937, 19.0403594),
            DateTime.Today,
            "sunrise"
        );

        string last2Chars = correctInput.Substring(correctInput.Length - 2);
        Assert.That(last2Chars, Is.EqualTo("AM"));
    }
    
    [Test]
    public async Task IncorrectInputTest2()
    {
        var incorrectInput = await coordToSolar.ConvertCoordinatesToSolar(
            new Coordinate(-30000, 1563),
            DateTime.Today, 
            "asdf"
        );
        
        Assert.That(incorrectInput, Is.EqualTo("{}"));
    }
  
}