

namespace Internship_3_OOP.ClassDirectory;

public enum Categories
{
    Economy,
    Standard,
    Business,
    Vip
}
public class Airplane:IHasName
{
    public int Id { get; }
    public string Name { get; }
    public int ManufactureYear { get; }
    public int FlightCount { get; set; }
    private Dictionary<Categories, int> _categoriesDict;
    private static List<Airplane> _airplaneList = new List<Airplane>();

    public DateTime CreationTime { get; }
    public DateTime UpdateTime { get; }

    public Airplane(string name, int manufactureYear, Dictionary<Categories, int> categoriesDict)
    {
        this.Id = Helper.IdGenerator();
        this.Name = name;
        this.ManufactureYear = manufactureYear;
        this._categoriesDict = categoriesDict;
        this.FlightCount = 0;
        this.CreationTime = DateTime.Now;
        this.UpdateTime = DateTime.Now;
    }

    public Dictionary<Categories,int> GetCategoriesAndCapacity()
    {
        return new Dictionary<Categories,int>(_categoriesDict);
    }
    public void AddToList()
    {
        _airplaneList.Add(this);
    }
    public static bool AddAirplane()
    {
        Console.Clear();
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
    public static string AirplaneNameInput()
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
            if (!Helper.ObjectExists(formattedInput,_airplaneList)) return formattedInput;
            
            Console.WriteLine("Ime već postoji u sustavu.Mora biti jedinstveno.");

        }

    }


    private static bool ValidateAirplaneName(string inputPlane)
    {
        return (!string.IsNullOrEmpty(inputPlane) && inputPlane.All(ch => char.IsLetter(ch) || char.IsDigit(ch)));  
    }
    private static void CategoriesInputNew(Dictionary<Categories, int> categoriesDict)
    {
            CategoryPrint();
            Console.Write("\nKategorija leta (unesi broj kategorije): ");

            if (!Helper.EnumFormatCheck(out Categories inputCategory))
            {
                Console.WriteLine("\nPogrešan format unosa.\n");
                return;
            }

            if (!Helper.IsDefinedInEnum(inputCategory))
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

    public static void AirplaneOutputMenu()
    {
        while(true){
            
            if (IsAirplaneListEmpty())
            {
                Helper.MessagePrintAndSleep("\nLista letova je prazna.Ne možeš ispisivati letove.\n");
                return;
            }
            Console.Clear();
            Console.WriteLine("\n----------------------");
            Console.WriteLine("1 - Ispis aviona po vremenu dodavanja(silazno)\n");
            Console.WriteLine("2 - Ispis aviona po vremenu dodavanja(uzlazno)\n");
            Console.WriteLine("3 - Ispis aviona po godini proizvodnje(silazno)\n");
            Console.WriteLine("4 - Ispis aviona po godini proizvodnje(uzlazno)\n");
            Console.WriteLine("5 - Ispis aviona po broju odrađenih letova(silazno)\n");
            Console.WriteLine("6 - Ispis aviona po broju odrađenih letova(uzlazno)\n");
            Console.WriteLine("0 - Povratak na izbornik za avione.");
            Console.WriteLine("----------------------\n");
            Console.Write("\nUnos :");
            var input=Console.ReadKey().KeyChar;
            switch (input)
            {
                case '0':
                    Helper.MessagePrintAndSleep("\nUspješan odabir.Povratak na izbornik za avione.\n");
                    Program.AirplaneMenu();
                    break;
                case '1':
                    Helper.MessagePrintAndSleep("\nUspješan odabir.Ispis aviona po vremenu dodavanja(silazno).");
                    OutputByParameter(false,SortKey.CreationTime);
                    Helper.WaitingUser();
                    break;
                case '2':
                    Helper.MessagePrintAndSleep("\nUspješan odabir.Ispis aviona po vremenu dodavanja(uzlazno).\n");
                    OutputByParameter(true,SortKey.CreationTime);                           
                    Helper.WaitingUser();
                    break;
                case '3':
                    Helper.MessagePrintAndSleep("\nUspješan odabir.Ispis aviona po godini proizvodnje(silazno).\n");
                    OutputByParameter(false,SortKey.ManufactureYear);                           
                    Helper.WaitingUser();
                    break;
                case '4':
                    Helper.MessagePrintAndSleep("\nUspješan odabir.Ispis aviona po godini proizvodnje(uzlazno).\n");
                    OutputByParameter(true,SortKey.ManufactureYear);                           
                    Helper.WaitingUser();
                    break;
                case '5':
                    Helper.MessagePrintAndSleep("\nUspješan odabir.Ispis aviona po broju odrađenih letova(silazno).\n");
                    OutputByParameter(false,SortKey.FlightCount);                           
                    Helper.WaitingUser();
                    break;
                case '6':
                    Helper.MessagePrintAndSleep("\nUspješan odabir.Ispis aviona po broju odrađenih letova(uzlazno).\n");
                    OutputByParameter(true,SortKey.FlightCount);                           
                    Helper.WaitingUser();
                    break;
                default:
                    Helper.MessagePrintAndSleep("\nUnos nije ponuđenima.Unesi ponovno.\n");
                    break;
            }
        }    
    }
    private enum SortKey
    {
        CreationTime,
        ManufactureYear,
        FlightCount
    }
    private static void OutputByParameter(bool isAscending,SortKey key)
    {
        var planeList = key switch
        {
            SortKey.CreationTime => (isAscending)
                ? _airplaneList.OrderBy(plane => plane.CreationTime).ToList()
                : _airplaneList.OrderByDescending(plane => plane.CreationTime).ToList(),
            
            SortKey.ManufactureYear => (isAscending)
                ? _airplaneList.OrderBy(plane => plane.ManufactureYear).ToList()
                : _airplaneList.OrderByDescending(plane => plane.ManufactureYear).ToList(),
            
            SortKey.FlightCount => (isAscending)
                ? _airplaneList.OrderBy(plane =>plane.FlightCount).ToList()
                : _airplaneList.OrderByDescending(plane => plane.FlightCount).ToList(),
            
            _ => []
        };
        
        
        if (key == SortKey.CreationTime)
        {
            AirplaneOutputWithDate(planeList);
            return;
        }
        AirplaneOutput(planeList);        
      

    }

    private static void AirplaneOutputWithDate(List<Airplane> planeList)
    {
        Helper.SleepAndClear();
        Console.WriteLine("\nIspis svih aviona\n");
        
        var planeInfo = planeList.Select(plane =>
        {
            var categories = plane._categoriesDict.Select(kvPair => $"{kvPair.Key} - {kvPair.Value}");
            var joinCategories=string.Join(", ", categories);
            
            return $"\n-----------\n{plane.CreationTime:yyyy-MM-dd HH:mm} - {plane.Id} - {plane.Name} - {plane.ManufactureYear} - {plane.FlightCount}\n[{joinCategories}]";
        });
        
        var finalString = string.Join("\n-----------\n", planeInfo);
        Console.WriteLine(finalString);        
    }
    
    public static void AirplaneOutput(List<Airplane>? airplaneList=null)
    {
        airplaneList??= _airplaneList;
        Helper.SleepAndClear();
        Console.WriteLine("\nIspis svih aviona.\n");
        foreach (var airplane in airplaneList)
        {
            airplane.FormattedAirplaneOutput();
        }
    }
    public void FormattedAirplaneOutput()
    {
        Console.WriteLine("\n-----------");
        Console.WriteLine("{0} - {1} - {2} - {3}",this.Id,this.Name,this.ManufactureYear,this.FlightCount);
        
        var enumerable = this._categoriesDict.Select(kvPair => $"{kvPair.Key} - {kvPair.Value}");
        var joinString=string.Join(", ", enumerable);

        Console.WriteLine($"[{joinString}]");
        Console.WriteLine("-----------\n");
    }
    
    public static void AirplaneSearch()
    { 
        while (true)
        {
            if (IsAirplaneListEmpty())
            {
                Helper.MessagePrintAndSleep("\nLista aviona je prazna.Ne možeš pretraživati.Povratak na izbornik za avione nakon pritiska tipke.");
                Helper.WaitingUser();
                return;
            }
            Console.Clear();
            Console.WriteLine("\n----------------------");
            Console.WriteLine("1 - Pretraživanje aviona po id-u\n");
            Console.WriteLine("2 - Pretraživanja aviona po nazivu\n");
            Console.WriteLine("0 - Povratak na izbornik za avione");
            Console.WriteLine("----------------------\n");
            Console.Write("\nUnos :");
            var input=Console.ReadKey().KeyChar;
                switch (input)
                {
                    case '0':
                        Helper.MessagePrintAndSleep("\nUspješan odabir.Povratak na izbornik za avione.\n");
                        Program.AirplaneMenu();
                        break;
                    case '1':
                        Helper.MessagePrintAndSleep("\nUspješan odabir.Pretraživanje po id-u.");
                        var searchedAirplaneById = SearchById();
                        if (searchedAirplaneById == null)
                        {
                            Helper.WaitingUser();
                            break;
                        }
                        Helper.MessagePrintAndSleep("\nUspješan pronalazak aviona.");
                        searchedAirplaneById.FormattedAirplaneOutput();
                        Helper.WaitingUser();
                        break;
                    case '2':
                        Helper.MessagePrintAndSleep("\nUspješan odabir.Pretraživanje po nazivu.\n");
                        var searchedAirplaneByName = SearchByName();
                        if (searchedAirplaneByName == null)
                        {
                            Helper.WaitingUser();
                            break;
                        }
                        
                        Helper.MessagePrintAndSleep("\nUspješan pronalazak aviona.");
                        searchedAirplaneByName.FormattedAirplaneOutput();
                        Helper.WaitingUser();
                        break;
                    default:
                        Helper.MessagePrintAndSleep("\nUnos nije ponuđenima.Unesi ponovno.\n");
                        break;
                }
        }     
    }

    private static void IdAndNameOfPlanes()
    {
        Console.WriteLine("Lista trenutno dostupnih aviona.");
        foreach (var plane in _airplaneList)
        {
            Console.WriteLine("{0} - {1}", plane.Id, plane.Name);
        }
    }
    private static Airplane? SearchById()
    {
        Console.Clear();
        IdAndNameOfPlanes();
        do
        {
            Console.Write("\nUnesi id: ");
            if (!Helper.IsIntegerValid(out var inputId))
            {
                Console.WriteLine("Pogrešan format unosa.");
                continue;               
            }

            var exist = _airplaneList.Any(plane => plane.Id == inputId);
            if (!exist)
            {
                Console.WriteLine("Avion s traženim id-om ne postoji");
                continue;
            }
            return _airplaneList.Find(plane=>plane.Id == inputId);
                    
        } while (Helper.ConfirmationMessage("ponovno unijeti id"));

        return null;
    }

    public static  int LinkFlightAndAirplane()
    {
      return Helper.FormatAndSearchByName(_airplaneList);  
    }
    private static Airplane? SearchByName()
    {
        Console.Clear();
        IdAndNameOfPlanes();
        do
        {
            var searchIndex=Helper.FormatAndSearchByName(_airplaneList);
            if (searchIndex == -1)
            {
                Console.WriteLine("\nAvion s tim imenom nije pronađen.\n");
                continue;
            }

            return _airplaneList[searchIndex];

        } while (Helper.ConfirmationMessage("ponovno unijeti ime"));

        return null;
    }
    
    public static bool IsAirplaneListEmpty()
    {
        return  _airplaneList.Count == 0;
    }

    public static void DeleteAirplane()
    {
        while (true)
        {
            Console.Clear();
            if (IsAirplaneListEmpty())
            {
                Console.WriteLine("\nLista aviona je prazna.Ne možeš više brisati.Povratak na izbornik za avione nakon pritiska tipke.");
                Helper.WaitingUser();
                Program.AirplaneMenu();
            }
            Console.WriteLine("\n----------------------");
            Console.WriteLine("1 - Brisanje aviona po id-u\n");
            Console.WriteLine("2 - Brisanje aviona po nazivu\n");
            Console.WriteLine("0 - Povratak na izbornik za avione");
            Console.WriteLine("----------------------\n");
            Console.Write("\nUnos: ");
            var input = Console.ReadKey().KeyChar;
                switch (input)
                {
                    case '0':
                        Helper.MessagePrintAndSleep("\nUspješan odabir.Povratak na izbornik za avione.\n");
                        Program.AirplaneMenu();
                        break;
                    case '1':
                        Helper.MessagePrintAndSleep("\nUspješan odabir.Brisanje po id-u.");
                        DeleteById();
                        Helper.WaitingUser();
                        break;
                    case '2':
                        Helper.MessagePrintAndSleep("\nUspješan odabir.Brisanje po nazivu.\n");
                        DeleteByName();                      
                        Helper.WaitingUser();
                        break;
                    default:
                        Console.WriteLine("\nUnos nije među ponuđenima.Unesi ponovno");
                        Thread.Sleep(1000);
                        break;
                }
        }             
    }

    private static void DeleteById()
    {
        Helper.SleepAndClear();
        var planeForDeletion = SearchById();
        if (planeForDeletion == null)
            return;
        
        var flights = Flight.FindFlightsConnectedToPlane(planeForDeletion.Id);    
        if (!Helper.ConfirmationMessage($"obrisati {planeForDeletion.Name} avion i sve letove ({flights}) vezane uz njega"))
            return;
        
        _airplaneList.RemoveAll(plane=>plane.Id == planeForDeletion.Id);
        RemoveOtherFlightReferences(planeForDeletion.Id);
        
        Console.WriteLine("\nUspješno brisanje aviona {0} u trenutku: {1}",planeForDeletion.Name,DateTime.Now);
    }

    private static void DeleteByName()
    {
        Helper.SleepAndClear();
        var planeForDeletion = SearchByName();
        if (planeForDeletion == null)
        {
            Console.WriteLine("\nOdustao si od brisanja.");
            return;
        }

        var flights = Flight.FindFlightsConnectedToPlane(planeForDeletion.Id);
        if (!Helper.ConfirmationMessage($"obrisati {planeForDeletion.Name} avion i sve letove ({flights}) vezane uz njega")) return;
        
        _airplaneList.RemoveAll(plane=>plane.Name == planeForDeletion.Name);
        RemoveOtherFlightReferences(planeForDeletion.Id);
        
        Console.WriteLine("\nUspješno brisanje aviona: {0} u trenutku: {1}",planeForDeletion.Name,DateTime.Now);
    }

    private static void RemoveOtherFlightReferences(int planeId)
    {
        Flight.RemoveFlightsWithPlaneId(planeId);
        Passenger.RemoveFlightsWithPlaneId(planeId);       
    }
    
    public static Airplane ListAt(int index)
    {
        return _airplaneList[index];
    }
    
}