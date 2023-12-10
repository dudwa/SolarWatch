using System.Net;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using SolarWatch.Model;

namespace SolarWatchIntegrationTest;

[TestClass]
public class IntegrationTest
{
    private HttpClient client;
    private TestServer server;


    [TestInitialize]
    public void T()
    {
        server = new TestServer(new WebHostBuilder()
            .UseStartup<Startup>());
        client = server.CreateClient();
    }

    [TestMethod]
    public async Task GetSunriseIncorrectCityDefaultsToSzeged()
    {
        string city = "sZasdas";
        string date = "2023-05-20";
        string endpoint = $"/Solar/getsunrise/{city}/{date}";

        var response = await client.GetAsync(endpoint);

        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();
        Assert.IsTrue(responseJson.ToLower().Contains("szeged"));
    }

    [TestMethod]
    public async Task GetSunriseCorrectCityCorrectDate()
    {
        string city = "Budapest";
        string date = "2023-09-21";
        string endpoint = $"/Solar/getsunrise/{city}/{date}";

        var response = await client.GetAsync(endpoint);

        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();

        SolarData solarData = JsonConvert.DeserializeObject<SolarData>(responseJson);

        Assert.AreEqual(solarData.Day, date);
        Assert.AreEqual(solarData.Type, "sunrise");
    }
    
    [TestMethod]
    public async Task GetSunriseInCorrectDateReturnBadRequest()
    {
        string city = "Budapest";
        string date = "kettőnégy";
        string endpoint = $"/Solar/getsunrise/{city}/{date}";

        var response = await client.GetAsync(endpoint);

        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        client.Dispose();
        server.Dispose();
    }
}