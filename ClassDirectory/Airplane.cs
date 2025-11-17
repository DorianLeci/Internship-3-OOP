namespace Internship_3_OOP.ClassDirectory;

public enum Categories
{
    Economy,
    PremiumEconomy,
    Business,
    VIP
}
public class Airplane
{
    public int Id { get; }
    public string Name { get; }
    public int ManufactureYear { get; }
    public int FlightNumber { get; }
    private Dictionary<Categories, int> _categoriesDict;

    public Airplane(string name, int manufactureYear, int flightNumber,
        Dictionary<Categories, int> categoriesDict)
    {
        this.Id = Helper.IdGenerator();
        this.Name = name;
        this.ManufactureYear = manufactureYear;
        this.FlightNumber = flightNumber;
        this._categoriesDict = categoriesDict;
    }
}