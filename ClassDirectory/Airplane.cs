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
    public int FlightCount { get; set; }
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
        } while (categoriesDict.Count<=((int)Categories.Vip) && Helper.ConfirmationMessage("unijeti novu kategoriju"));

        if (!Helper.ConfirmationMessage("dodati novi avion"))
            return false;
        
        _airplaneList.Add(new Airplane(name,manufactureYear, categoriesDict));
        return true;
    }

    public static Airplane? GetLastElement()
    {
        return (_airplaneList.Count > 0) ? _airplaneList.Last() : null;
    }
    private static string AirplaneNameInput()
    {
        while (true)
        {
            Console.Write("\nUnesi ime aviona: ");
            var inputPlane = Console.ReadLine()!.ToLower();
            var removed=Helper.RemoveWhiteSpace(inputPlane);
            if (!ValidateAirplaneName(removed))
            {
                Console.WriteLine("Pogrešan format unosa.\n");
                continue;
            }
            
            var formattedInput = Helper.ReturnFormattedInput(inputPlane);
            if (!Airplane.PlaneExists(formattedInput)) return formattedInput;
            
            Console.WriteLine("Ime već postoji u sustavu.Mora biti jedinstveno.");

        }

    }


    private static bool ValidateAirplaneName(string inputPlane)
    {
        return (!string.IsNullOrEmpty(inputPlane) && inputPlane.All(ch => char.IsLetter(ch) || char.IsDigit(ch)));  
    }
    private static bool PlaneExists(string inputName)
    {
        return (_airplaneList.Count>0) && _airplaneList.Any(plane => plane.Name== inputName);
    }
    private static void CategoriesInputNew(Dictionary<Categories, int> categoriesDict)
    {
            CategoryPrint();
            Console.Write("\nKategorija leta (unesi broj kategorije): ");

            if (!CategoriesFormatCheck(out var inputCategory))
            {
                Console.WriteLine("\nPogrešan format unosa.\n");
                return;
            }

            if (!IsDefinedInEnum(inputCategory))
            {
                Console.WriteLine("\nTa kategorija nije navedena.\n");
                return;
            }

            if (IsCategoryAdded(categoriesDict,inputCategory))
            {
                Console.WriteLine("\nKategorija je već unesena.\n");
                return;
            }
            
            Console.WriteLine("\nUnesena Kategorija: {0}",inputCategory);
            var seats=NumberOfSeatsInput();
            categoriesDict.Add(inputCategory, seats);
            
    }

    private static void CategoryPrint()
    {
        var i = 0;
        Console.WriteLine("\nIspis mogućih kategorija leta\n");
        foreach (var cat in Enum.GetValues(typeof(Categories)))
        {
            Console.WriteLine("{0} - {1}",i,cat);
            i++;
        }
    }

    private static bool CategoriesFormatCheck(out Categories inputCategory)
    {
        var input = Console.ReadKey().KeyChar;
        return Enum.TryParse<Categories>(input.ToString(),true, out inputCategory);
    }

    private static bool IsDefinedInEnum(Categories inputCategory)
    {
        return Enum.IsDefined(typeof(Categories), inputCategory);
    }

    private static bool IsCategoryAdded(Dictionary<Categories, int> categoriesDict,Categories inputCategory)
    {
        return categoriesDict.ContainsKey(inputCategory);
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
            var searchRes=FormatAndSearchByName();
            if (searchRes == -1) continue;

            return _airplaneList.Find(plane => plane.Id == searchRes);

        } while (Helper.ConfirmationMessage("ponovno unijeti ime"));

        return null;
    }

    public static int FormatAndSearchByName()
    {
        Console.Write("Unesi ime: ");
        var inputPlane = Console.ReadLine()!.ToLower();
        var formattedInput=Helper.ReturnFormattedInput(inputPlane);
        var exist = PlaneExists(formattedInput);
        if (exist)
            return _airplaneList.FindIndex(plane => plane.Name == formattedInput);
        else
        {
            Console.WriteLine("Ne postoji avion s unesenim imenom.");
            return -1;
        }
 
    }
    public static bool IsAirplaneListEmpty()
    {
        return _airplaneList.Count == 0;
    }

    public static void DeleteAirplane()
    {
        while (true)
        {
            if (IsAirplaneListEmpty())
            {
                Console.WriteLine("\nLista aviona je prazna.Ne možeš više brisati.Povratak na izbornik za avione nakon pritiska tipke.");
                Helper.WaitingUser();
                Program.AirplaneMenu();
            }
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

    public static Airplane ListAt(int index)
    {
        return _airplaneList[index];
    }
}