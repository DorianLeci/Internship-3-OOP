namespace Internship_3_OOP.ClassDirectory;

public enum Categories
{
    Economy,
    Standard,
    Business,
    Vip
}
public class Airplane
{
    public int Id { get; }
    public string Name { get; }
    public int ManufactureYear { get; }
    public int FlightCount { get; }
    private Dictionary<Categories, int> _categoriesDict;
    private static List<Airplane> _airplaneList = new List<Airplane>();

    public DateTime creationTime { get; }
    public DateTime updateTime { get; }

    public Airplane(string name, int manufactureYear, Dictionary<Categories, int> categoriesDict)
    {
        this.Id = Helper.IdGenerator();
        this.Name = name;
        this.ManufactureYear = manufactureYear;
        this._categoriesDict = categoriesDict;
        this.FlightCount = 0;
        this.creationTime = DateTime.Now;
        this.updateTime = DateTime.Now;
        _airplaneList.Add(this);
    }
    

    public static void CategoriesInputNew(Dictionary<Categories, int> categoriesDict)
    {
        while (true)
        {
            Console.Write("\nKategorija leta: ");
            var inputCategory = Console.ReadLine()!.ToLower().Trim();

            if (CategoriesCheckNew(inputCategory,categoriesDict))
            {
                Console.WriteLine("Uspješan unos kategorije.\n");
                return;
            }
            else Console.WriteLine("Pogrešan unos kategorije.");
          
        }
    }
    private static bool CategoriesCheckNew(string inputCategory,Dictionary<Categories, int> categoriesDict)
    {
        if (Enum.TryParse<Categories>(inputCategory, true, out var categoryEnumVar)
            && Enum.IsDefined(typeof(Categories), categoryEnumVar) && !categoriesDict.ContainsKey(categoryEnumVar))
        {
            Console.WriteLine("Kategorija: {0}",categoryEnumVar);
            var seats=NumberOfSeatsInput();
            categoriesDict.Add(categoryEnumVar, seats);
            return true;
        }
        else if(categoriesDict.ContainsKey(categoryEnumVar))
            Console.WriteLine("Kategorija {0} je već unesena.",categoryEnumVar);
        return false;

    }

    private static int NumberOfSeatsInput()
    {
        while (true)
        {
            Console.Write("\nBroj sjedećih mjesta: ");
            if (int.TryParse(Console.ReadLine()!.Trim(), out var inputSeats) && inputSeats > 0)
            {
                return inputSeats;
            }
            else if(inputSeats<=0) Console.WriteLine("Pogrešan unos sjedala.Broj mora biti veći od nula.");
            else Console.WriteLine("Pogrešan format unosa.");           
        }
    }

    public void FormattedAirplaneOutput()
    {
        Console.WriteLine("{0} - {1} - {2} - {3}",this.Id,this.Name,this.ManufactureYear,this.FlightCount);
        Console.Write("Kategorije: [");
        foreach (var category in this._categoriesDict.Keys)
        {
            Console.Write("{0} ",category);
        }
        Console.Write("]\n");
    }
}