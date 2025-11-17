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
    }
    
    public static bool AddAirplane()
    {
        var name = AirplaneNameInput();
        var manufactureYear = Helper.YearInput("izrade aviona");
        var categoriesDict=new Dictionary<Categories, int>();

        do
        {
            Airplane.CategoriesInputNew(categoriesDict);
        } while (categoriesDict.Count!=((int)Categories.Vip) && Helper.ConfirmationMessage("unijeti novu kategoriju"));

        if (Helper.ConfirmationMessage("dodati novi avion"))
        {
            _airplaneList.Add(new Airplane(name,manufactureYear, categoriesDict));
            return true;
        }

        return false;


    }

    public static Airplane GetLastElement()
    {
        return _airplaneList.Last();
    }
    public static string AirplaneNameInput()
    {
        while (true)
        {
            Console.Write("\nUnesi ime aviona: ");
            var inputPlane = Console.ReadLine()!.ToLower();
            var removed=Helper.RemoveWhiteSpace(inputPlane);
            var formattedInput = Helper.ReturnFormattedInput(inputPlane);
            if (ValidateAirplaneName(removed,formattedInput))
            {
                return formattedInput;
            }
            else Console.WriteLine("Pogrešan unos.\n");
        }

    }


    private static bool ValidateAirplaneName(string inputPlane,string formattedInputPlane)
    {

        if(Airplane.PlaneExists(formattedInputPlane))
        {
            Console.WriteLine("Ime već postoji u sustavu.Mora biti jedinstveno.");
            return false;               
        }
            
        return (!string.IsNullOrEmpty(inputPlane) && inputPlane.All(ch => char.IsLetter(ch) || char.IsDigit(ch)));  
    }
    private static bool PlaneExists(string inputName)
    {
        return _airplaneList.Any(plane => plane.Name== inputName);
    }
    private static void CategoriesInputNew(Dictionary<Categories, int> categoriesDict)
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
            Console.Write("\nBroj sjedećih mjesta.Mora biti veći od nule: ");
            if (int.TryParse(Console.ReadLine()!.Trim(), out var inputSeats) && inputSeats > 0)
            {
                return inputSeats;
            }
            else if(inputSeats<=0) Console.WriteLine("Pogrešan unos broja sjedećih mjesta.");
        }
    }
    public static void AirplaneOutput()
    {

        foreach (var airplane in _airplaneList)
        {
            airplane.FormattedAirplaneOutput();
        }
    }
    public void FormattedAirplaneOutput()
    {
        Console.WriteLine("\n-----------");
        Console.WriteLine("{0} - {1} - {2} - {3}",this.Id,this.Name,this.ManufactureYear,this.FlightCount);
        Console.Write("Kategorije: [");
        foreach (var category in this._categoriesDict.Keys)
        {
            Console.Write("{0} ",category);
        }
        Console.Write("]\n");
        Console.WriteLine("-----------\n");
    }
    
    public static void AirplaneSearch()
    { 
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\n----------------------");
            Console.WriteLine("1 - Pretraživanje aviona po id-u\n");
            Console.WriteLine("2 - Pretraživanja aviona po nazivu\n");
            Console.WriteLine("0 - Povratak na izbornik za avione");
            Console.WriteLine("----------------------\n");
            if (int.TryParse(Console.ReadLine(), out int inputMainMenu))
            {
                switch (inputMainMenu)
                {
                    case 0:
                        Console.WriteLine("Uspješan odabir.Povratak na izbornik za avione.\n");
                        Program.AirplaneMenu();
                        break;
                    case 1:
                        Console.WriteLine("Uspješan odabir.Pretraživanje po id-u.");
                        var searchedAirplaneById = SearchById();
                        if (searchedAirplaneById != null)
                        {
                            Console.WriteLine("Uspješan pronalazak aviona.");
                            searchedAirplaneById.FormattedAirplaneOutput();
                        }
                        else Console.WriteLine("Ne postoji avion s unesenim id-om.");
                        Helper.WaitingUser();
                        break;
                    case 2:
                        Console.WriteLine("Uspješan odabir.Pretraživanje po nazivu.\n");
                        var searchedAirplaneByName = SearchByName();
                        if (searchedAirplaneByName != null)
                        {
                            Console.WriteLine("Uspješan pronalazak aviona.");
                            searchedAirplaneByName.FormattedAirplaneOutput();
                        }
                        else Console.WriteLine("Ne postoji avion s unesenim nazivom.");                        
                        Helper.WaitingUser();
                        break;
                    default:
                        Console.WriteLine("Unos nije među ponuđenima.Unesi ponovno");
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nPogrešan tip podatka->unesi cijeli broj.");
            }
        }     
    }

    private static Airplane? SearchById()
    {
        do
        {
            Console.Write("Unesi id: ");
                if(!int.TryParse(Console.ReadLine()?.Trim(), out var inputId))
                    Console.WriteLine("Pogrešan format unosa.");
                else
                {
                    var exist = _airplaneList.Any(plane => plane.Id == inputId);
                    if (exist)
                        return _airplaneList.Find(plane => plane.Id == inputId);
                    else
                        Console.WriteLine("Avion s traženim id-om ne postoji");
                    
                }

        } while (Helper.ConfirmationMessage("ponovno unijeti id"));

        return null;
    } 
    
    private static Airplane? SearchByName()
    {
        string formattedInput;
        int inputId;
        do
        {
            Console.Write("Unesi ime: ");
            var inputPlane = Console.ReadLine()!.ToLower();
            formattedInput=Helper.ReturnFormattedInput(inputPlane);
            var exist = PlaneExists(formattedInput);
            if (exist)
                return _airplaneList.Find(plane => plane.Name == formattedInput);
            else
                Console.WriteLine("Ne postoji avion s unesenim imenom.");
        } while (Helper.ConfirmationMessage("ponovno unijeti ime"));

        return null;
    }

    public static bool IsAirplaneListEmpty()
    {
        return _airplaneList.Count == 0;
    }

    public static void DeleteAirplane()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\n----------------------");
            Console.WriteLine("1 - Brisanje aviona po id-u\n");
            Console.WriteLine("2 - Brisanje aviona po nazivu\n");
            Console.WriteLine("0 - Povratak na izbornik za avione");
            Console.WriteLine("----------------------\n");
            if (int.TryParse(Console.ReadLine(), out int inputMainMenu))
            {
                switch (inputMainMenu)
                {
                    case 0:
                        Console.WriteLine("Uspješan odabir.Povratak na izbornik za avione.\n");
                        Program.AirplaneMenu();
                        break;
                    case 1:
                        Console.WriteLine("Uspješan odabir.Brisanje po id-u.");
                        DeleteById();
                        Helper.WaitingUser();
                        break;
                    case 2:
                        Console.WriteLine("Uspješan odabir.Brisanje po nazivu.\n");
                        DeleteByName();                      
                        Helper.WaitingUser();
                        break;
                    default:
                        Console.WriteLine("Unos nije među ponuđenima.Unesi ponovno");
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nPogrešan tip podatka->unesi cijeli broj.");
            }
        }             
    }

    private static void DeleteById()
    {
        var planeForDeletion = SearchById();
        if (planeForDeletion == null)
        {
            Console.WriteLine("Ne postoji avion s unesenim id-om.");
            return;
        }

        if (Helper.ConfirmationMessage("obrisati avion"))
        {
            _airplaneList.RemoveAll(plane=>plane.Id == planeForDeletion.Id);
            Console.WriteLine("Uspješno brisanje aviona u trenutku: {0}",DateTime.Now);
        }
    }

    private static void DeleteByName()
    {
        var planeForDeletion = SearchByName();
        if (planeForDeletion == null)
        {
            Console.WriteLine("Ne postoji avion s unesenim imenom.");
            return;
        }

        if (Helper.ConfirmationMessage("obrisati avion"))
        {
            _airplaneList.RemoveAll(plane=>plane.Id == planeForDeletion.Id);
            Console.WriteLine("Uspješno brisanje aviona: {0} u trenutku: {1}",planeForDeletion.Name,DateTime.Now);
        }        
    }
}