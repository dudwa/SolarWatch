using SolarWatch.Data;

namespace SolarWatch.Service.Repository;

public class CityRepository : ICityRepository
{
    public IEnumerable<City> GetAll()
    {
        using var dbContext = new SolarWatchAPIContext();
        return dbContext.Cities.ToList();
    }

    public City? GetByName(string name)
    {
        using var dbContext = new SolarWatchAPIContext();
        return dbContext.Cities.FirstOrDefault(c => c.Name == name);
    }

    public void Add(City city)
    {
        using var dbContext = new SolarWatchAPIContext();
        dbContext.Add(city);
        dbContext.SaveChanges();
    }

    public void Delete(City city)
    {
        using var dbContext = new SolarWatchAPIContext();
        dbContext.Remove(city);
        dbContext.SaveChanges();
    }

    public void Update(City city)
    {  
        using var dbContext = new SolarWatchAPIContext();
        dbContext.Update(city);
        dbContext.SaveChanges();
    }
}