using System.Globalization;
using System.Text.RegularExpressions;

namespace Internship_3_OOP.ClassDirectory;

public class 
    Flight: IHasName
{
    public int Id { get; }
    public string Name { get; }
    public DateTime DepartureDate { get; set; }
    public DateTime ArrivalDate { get; set; }
    public double Distance { get; }
    public TimeSpan FlightTime { get; set; }
    public DateTime CreationTime { get; }
    public DateTime UpdateTime { get; set; }
    public Airplane Airplane { get; }
    public Crew FlightCrew { get; set; }
    private static List<Flight> _flightList = new List<Flight>();

    private Dictionary<Categories, int> _capacityCount;

    public Flight(string name, DateTime departureDate,DateTime arrivalDate,double distance,TimeSpan flightTime,Airplane airplane,Crew flightCrew)
    {
        this.Id = Helper.IdGenerator();
        this.Name = name;
        this.DepartureDate = departureDate;
        this.ArrivalDate = arrivalDate;
        this.Distance = distance;
        this.FlightTime = flightTime;
        this.Airplane = airplane;
        this.FlightCrew = flightCrew;
        this.CreationTime = DateTime.Now;
        this.UpdateTime = DateTime.Now;

        this._capacityCount = Airplane.GetCategoriesAndCapacity();
    }

    public void AddToList()
    {
        _flightList.Add(this);
    }

    public void DecrementCategoryCapacity(Categories category)
    {
        this._capacityCount[category]--;
    }
    public void IncrementCategoryCapacity(Categories category)
    {
        this._capacityCount[category]++;
    }

    public List<Categories> GetCategories()
    {
        return _capacityCount.Keys.ToList();
    }
    
    public static void AddFlight()
    {
        Helper.SleepAndClear();
        var name = FlightNameInput();
        var distance = FlightDistanceInput();
        var departureDate = DepartureDateInput();
        var arrivalDate = ArrivalDateInput(departureDate);
        var flightTime = (arrivalDate - departureDate).Duration();
        var airplane = ChooseAirplane();
        var crew = ChooseCrew(departureDate,arrivalDate);
        airplane.FlightCount++;

        if (!Helper.ConfirmationMessage("dodati novi let"))
            return;

        Console.WriteLine("\nUspješno dodavanje novog leta {0} koji je povezan s avionom {1} i posadom {2} - {3}:",name,airplane.Name,crew.Id,crew.CrewName);
         _flightList.Add(new Flight(name, departureDate, arrivalDate,distance, flightTime, airplane,crew));

    }

    public static void FlightFormattedOutput(bool isUserReserving,List<Flight> flightList)
    {
        
        foreach(var flight in flightList)
        {
            Console.WriteLine("\n--------------");
            flight.OutputForOneFlight(isUserReserving);
            Console.WriteLine("--------------\n");
        }
        
    }

    private void OutputForOneFlight(bool isUserReserving)
    {
        var depDateTimeString = this.DepartureDate.ToString("yyyy-MM-dd HH:mm");
        var arrDateTimeString=this.ArrivalDate.ToString("yyyy-MM-dd HH:mm");
        Console.WriteLine("{0} - {1} - {2} - {3} - {4} -{5} - {6} - {7} - {8}\n",this.Id,this.Name,depDateTimeString,arrDateTimeString,
            this.Distance,this.FlightTime,this.Airplane.Name,this.FlightCrew.Id,this.FlightCrew.CrewName);   
        
        if(isUserReserving)
            OutputCapacityForOneFlight();
    }

    public void OutputCapacityForOneFlight()
    {
        
        Console.WriteLine("\nIspis dostupnih slobodnih mjesta po svakoj kategoriji(id kategorije - ime kategorije - broj dostupnih sjedala).");        
        
        var filtratedDictByValue = _capacityCount.Where(kvPair => kvPair.Value > 0);
        var printString =string.Join(", ",filtratedDictByValue.OrderBy(kvPair=>(int)kvPair.Key).
            Select(kvPair=>$"{(int)kvPair.Key} - {kvPair.Key}  - {kvPair.Value}"));
        
        Console.WriteLine($"\n[{printString}]\n");

    }
    private static string FlightNameInput()
    {
        Helper.SleepAndClear();
        Console.WriteLine("\nUnos imena leta.\n");
        while (true)
        {
            Console.Write("\nUnesi ime leta (npr. AA789 ili AB7894): ");
            var inputName = Console.ReadLine()!.Trim().ToUpper();
            inputName=Helper.RemoveWhiteSpace(inputName);
            
            if (!Regex.IsMatch(inputName, @"^[A-Z]{2}[0-9]{3,4}$"))
            {
                Console.WriteLine("Pogrešan format imena.\n");
                continue;
            }

            if (_flightList.Count <= 0 || !_flightList.Any(flight => flight.Name == inputName)) return inputName;
            
            Console.WriteLine("Već postoji let s istim imenom.Mora biti jedinstveno.\n");
            
        }
    }

    private static DateTime DepartureDateInput()
    {
        Helper.SleepAndClear();
        Console.WriteLine("\nUnos datuma i vremena polaska.\n");
        while (true)
        {
            Console.Write("\nUnesi kada let polazi.(YYYY-MM-DD HH:mm): ");
            if (!FlightFormatCheck(out var inputDateTime))
            {
                Console.WriteLine("\nPogrešan format.\n");
                continue;
            }

            if (FlightDateTimeCheck(inputDateTime)) return inputDateTime;
            Console.WriteLine("\nPolazak novog leta mora biti barem 24h od trenutnog vremena.\n");

        }
    }
    private static DateTime ArrivalDateInput(DateTime departureDateTime)
    {
        Helper.SleepAndClear();
        Console.WriteLine("\nUnos datuma i vremena dolaska.\n");
        while (true)
        {
            Console.Write("\nUnesi kada let završava.(YYYY-MM-DD HH:mm) ");
            if (!FlightFormatCheck(out var inputDateTime))
            {
                Console.WriteLine("\nPogrešan format datuma.Pokušaj ponovno.\n");
                continue;               
            }

            if (!CheckArrivalTime(inputDateTime, departureDateTime))
            {
                Console.WriteLine("\nVrijeme sletanja ne smije biti prije vremena polijetanja.\n");
                continue;
            }

            if (!CheckFlightDuration(inputDateTime, departureDateTime))
            {
                Console.WriteLine("\nLet ne smije trajati duže od 24 sata.\n");
                continue;
            }
            
            return inputDateTime;
        }
    }
    private static bool FlightFormatCheck(out DateTime inputDateTime)
    {
        var today = DateTime.Now;
        var input = Console.ReadLine()!.Trim();

        return DateTime.TryParseExact(input, "yyyy-MM-dd HH:mm",
            CultureInfo.InvariantCulture, DateTimeStyles.None, out inputDateTime);
    }

    private static bool FlightDateTimeCheck(DateTime depDateTime)
    {
        return depDateTime>DateTime.Now && (depDateTime-DateTime.Now).Duration().TotalHours>=24;
    }
    private static bool CheckArrivalTime(DateTime arrivalDateTime,DateTime departureDateTime)
    {
        return (arrivalDateTime >departureDateTime);
    }

    private static bool CheckFlightDuration(DateTime arrivalDateTime, DateTime departureDateTime)
    {
        return (departureDateTime - arrivalDateTime).Duration().TotalHours<=24;
    }
    private static Airplane ChooseAirplane()
    {
        Airplane.AirplaneOutput();
    
        while (true)
        {
            Console.WriteLine("Pridruži let avionu");
            var searchIndex = Airplane.LinkFlightAndAirplane();
            if (searchIndex != -1) return Airplane.ListAt(searchIndex);
            
            Console.WriteLine("Ne postoji avion s unesenim imenom.\n");
        }
    }
    private static Crew ChooseCrew(DateTime depDateTime,DateTime arrDateTime,bool isCrewChanged=false,Crew ? oldCrew=null)
    {
        Helper.SleepAndClear();
        Console.WriteLine("\nPridruživanje posade letu (prikazuju se samo posade koje nisu zauzete u trenutku ovog leta).\n");
        
        var foundCrews=Crew.FindAvailableCrews(depDateTime, arrDateTime,_flightList,isCrewChanged,oldCrew);
        if (foundCrews.Count == 0)
        {
            Helper.MessagePrintAndSleep("\nNema niti jedne dostupne posade.Moraš unijeti novu.\n");
            Helper.WaitingUser();
            Crew.CreateNewCrew(false);
        }
        
        foreach (var crew in foundCrews)
        {
            crew.GeneralCrewInfo();
        }
        
        while (true)
        {
            Console.Write("\nUnesi posadu koju želiš pridružiti letu(broj posade): ");
            if (!Helper.IsIntegerValid(out var inputId))
            {
                Console.WriteLine("\nPogrešan format unosa.\n");
                continue;
            }
            
            var index=IsCrewFound(inputId,foundCrews);
            if (index == -1)
            {
                Console.WriteLine("\nU listi dostupnih posada ne postoji uneseni id.\n");
                continue;
            }
            return foundCrews[index];
            
        }
    }

    private static int IsCrewFound(int inputId,List<Crew> foundCrews)
    {
        return foundCrews.FindIndex(crew => crew.Id == inputId);
    }
    private static double FlightDistanceInput()
    {
        Helper.SleepAndClear();
        Console.WriteLine("\nUnos prijeđene udaljenosti.\n");
        while (true)
        {
            Console.Write("Unesi prijeđenu udaljenost u km: ");
            if (!Helper.DoubleInputCheck(out double input))
            {
                Console.WriteLine("Pogrešan format unosa.\n");
                continue;
            }

            if (input <= 0)
            {
                Console.WriteLine("Prijeđeni put mora biti veći od 0 km.\n");
                continue;
            }
            return input;
        }
    }

    public static void FlightSearch()
    {
        while (true)
        {
            if (IsFlightListEmpty())
            {
                Helper.MessagePrintAndSleep("\nLista letova je prazna.Ne možeš pretraživati.Povratak na izbornik za letove nakon pritiska tipke.");
                Helper.WaitingUser();
                return;
            }
            Console.Clear();
            Console.WriteLine("\n----------------------");
            Console.WriteLine("1 - Pretraživanje letova po id-u\n");
            Console.WriteLine("2 - Pretraživanja letova po nazivu\n");
            Console.WriteLine("0 - Povratak na izbornik za letove");
            Console.WriteLine("----------------------\n");
            Console.Write("\nUnos :");
            var input=Console.ReadKey().KeyChar;
                switch (input)
                {
                    case '0':
                        Helper.MessagePrintAndSleep("\nUspješan odabir.Povratak na izbornik za letove.\n");
                        Program.FlightMenu();
                        break;
                    case '1':
                        Helper.MessagePrintAndSleep("\nUspješan odabir.Pretraživanje po id-u.");
                        var searchedFlightById = SearchById(_flightList);
                        if (searchedFlightById == null)
                        {
                            Helper.WaitingUser();
                            break;
                        }
                        Helper.MessagePrintAndSleep("\nUspješan pronalazak leta.");
                        searchedFlightById.OutputForOneFlight(false);
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
                        
                        Helper.MessagePrintAndSleep("\nUspješan pronalazak leta.");
                        searchedAirplaneByName.OutputForOneFlight(false);
                        Helper.WaitingUser();
                        break;
                    default:
                        Helper.MessagePrintAndSleep("\nUnos nije ponuđenima.Unesi ponovno.\n");
                        break;
                }
        }             
    }

    private static bool IsFlightListEmpty()
    {
        return _flightList.Count == 0;
    }
    public static Flight? SearchById(List<Flight> flightList,bool isUserReserving=false)
    {
        Helper.SleepAndClear();
        AllAvailableFlights(isUserReserving,flightList);
        
        do
        {
            Console.Write("\nUnesi id: ");
            if (!Helper.IsIntegerValid(out var inputId))
            {
                Console.WriteLine("Pogrešan format unosa.");
                continue;               
            }
            
            var exist = flightList.Any(plane => plane.Id == inputId);
            if (!exist)
            {
                Console.WriteLine("Let s traženim id-om ne postoji");
                continue;
            }
            return flightList.Find(flight => flight.Id == inputId);
                    
        } while (Helper.ConfirmationMessage("ponovno unijeti id"));

        return null;        
    }

    private static Flight? SearchByName()
    {
        Helper.SleepAndClear();
        AllAvailableFlights(false);
        do
        {
            var searchIndex=Helper.FormatAndSearchByName(_flightList);
            if (searchIndex == -1)
            {
                Console.WriteLine("Let s traženim imenom ne postoji");
                continue;
            }

            return _flightList[searchIndex];

        } while (Helper.ConfirmationMessage("ponovno unijeti ime"));

        return null;
    }
    public static void AllAvailableFlights(bool isUserReserving,List<Flight>? flightList=null)
    {
        Helper.SleepAndClear();
        
        flightList ??= _flightList;
        
        Console.WriteLine("Ispis svih dostupnih letova");
        
        FlightFormattedOutput(isUserReserving,flightList);
    }

    public static void FlightEdit()
    {
        Helper.SleepAndClear();
        AllAvailableFlights(false);
        var returnedFlight=SearchById(_flightList);

        returnedFlight?.FoundFlightEdit();
    }

    private void FoundFlightEdit()
    {
        var oldDepDateTime = this.DepartureDate;
        var oldArrDateTime = this.ArrivalDate;
        var oldFlightCrew = this.FlightCrew;
        
        var newDepDateTime=DepDateTimeEdit(oldDepDateTime);
        var newArrDateTime=ArrDateTimeEdit(newDepDateTime,oldArrDateTime);
        var newCrew = FlightCrewEdit(newDepDateTime,newArrDateTime,oldFlightCrew,this);

        if (newDepDateTime == oldDepDateTime && newArrDateTime == oldArrDateTime && newCrew == oldFlightCrew)
        {
            Console.WriteLine("\nNisi ništa promijenio.\n");
            return;
        }
        
        if (!Helper.ConfirmationMessage($"izmijeni let: {this.Id} - {this.Name} - {this.Airplane.Name}"))
        {
            Console.WriteLine("\nOdustao si od izmjene.\n");
            return;
        }

        Console.WriteLine($"\nUspješna izmjena leta: [{this.Id}] - {this.Name} - {this.Airplane.Name}\n");

        if (oldDepDateTime != newDepDateTime)
        {
            this.DepartureDate = newDepDateTime;
            Console.WriteLine($"\nPromjena polaska (staro / novo): {oldDepDateTime:yyyy-MM-dd HH:mm} / {newDepDateTime:yyyy-MM-dd HH:mm}\n");            
        }


        if (oldArrDateTime != newArrDateTime)
        {
            this.ArrivalDate = newArrDateTime;
            Console.WriteLine($"\nPromjena dolaska(staro / novo): {oldArrDateTime:yyyy-MM-dd HH:mm} / {newArrDateTime:yyyy-MM-dd HH:mm}\n");            
        }


        if (oldFlightCrew != newCrew)
        {
            this.FlightCrew = newCrew;
            Console.WriteLine($"\nPromjena posade(staro / novo): {oldFlightCrew.CrewName} / {newCrew.CrewName}\n");           
        }
        
        this.FlightTime = (this.ArrivalDate - this.DepartureDate).Duration();


    }

    private static DateTime DepDateTimeEdit(DateTime oldDepDateTime)
    {
        Helper.SleepAndClear();

        Console.WriteLine($"\nPromjena polaska leta .Trenutni polazak:{oldDepDateTime:yyyy-MM-dd HH:mm} \n");
        return !Helper.ConfirmationMessage($"promijeniti datum i vrijeme polaska leta.") 
            ? oldDepDateTime : DepartureDateInput();
    }

    private static DateTime ArrDateTimeEdit(DateTime depDateTime,DateTime oldArrDateTime)
    {
        Helper.SleepAndClear();
        
        Console.WriteLine($"\nPromjena dolaska leta.\nTrenutni polazak: {depDateTime:yyyy-MM-dd HH:mm}\nTrenutni dolazak: {oldArrDateTime:yyyy-MM-dd HH:mm}");
        if (!CheckArrivalTime(oldArrDateTime, depDateTime))
        {
            Console.WriteLine("\nPromijenio si datum i vrijeme polaska leta i sada vrijeme dolaska više nije vremenski ispred polaska.");
            Helper.MessagePrintAndSleep("\nMoraš unesti novi datum i vrijeme dolaska leta.\n");
            Helper.WaitingUser();
            return ArrivalDateInput(depDateTime);
        }
        
        if (!CheckFlightDuration(oldArrDateTime, depDateTime))
        {
            Console.WriteLine("\nPromijenio si datum i vrijeme polaska leta i sada let traje duže od 24 sata.\n");
            Helper.MessagePrintAndSleep("\nMoraš unesti novi datum i vrijeme dolaska leta.\n");
            Helper.WaitingUser();
            return ArrivalDateInput(depDateTime);
        }
        
        return !Helper.ConfirmationMessage("promijeniti datum i vrijeme dolazka leta.") 
            ? oldArrDateTime : ArrivalDateInput(depDateTime);
    }
    
    public static void RemoveFlightsWithPlaneId(int planeId)
    {
        _flightList.RemoveAll(flight=>flight.Airplane.Id == planeId);
    }
    
    public static string FindFlightsConnectedToPlane(int planeId)
    {
        var foundFlights = _flightList.FindAll(flight => flight.Airplane.Id == planeId);
        return string.Join(", ",foundFlights.Select(flight=>flight.Id + " - " + flight.Name));
    }

    private static Crew FlightCrewEdit(DateTime depDateTime, DateTime arrDateTime,Crew oldCrew,Flight currFlight)
    {
        Helper.SleepAndClear();
        Console.WriteLine($"\nPromjena posade leta.\nTrenutna posada: {oldCrew.Id} - {oldCrew.CrewName}");

        if (Crew.FlightsOverlapWithCurrentCrew(depDateTime, arrDateTime, _flightList, oldCrew,currFlight))
        {
            Helper.MessagePrintAndSleep("\nZbog promjene datuma i vremena leta sada trenutna posada ne može biti na oba leta odjednom.Promijeni posadu.\n");
            Helper.WaitingUser();
            return ChooseCrew(depDateTime, arrDateTime,true,oldCrew);
        }
            
        return !Helper.ConfirmationMessage("promijeniti posadu pridruženu letu.") 
            ? oldCrew : ChooseCrew(depDateTime, arrDateTime,true,oldCrew);        
    }

    public static List<Flight> AvailableFlightsForThisUser(List<Flight> userFlightList)
    {
        var filtratedByOverlap = _flightList.FindAll(flight => userFlightList
            .All(usrFlight => !Crew.FlightsOverlap(flight.DepartureDate, flight.ArrivalDate, usrFlight.DepartureDate,
                usrFlight.ArrivalDate)));

        var filtratedByCapacity =
            filtratedByOverlap.FindAll(flight => flight._capacityCount.Any(kvPair => kvPair.Value > 0));

        return filtratedByCapacity;
    }
    
    public Categories? ChooseCategory()
    {
        do
        {
            Console.Write("\nUnesi kategoriju(broj kategorije): ");
            if (!Helper.EnumFormatCheck(out Categories category))
            {
                Console.WriteLine("\nPogrešan format unosa.\n");
                continue;               
            }

            if (!IsDefinedInFlightCategories(category))
            {
                Console.WriteLine("\nTa kategorija nije navedena.\n");
                continue;
            }

            return category;

        } while (Helper.ConfirmationMessage("ponovno unijeti broj kategorije"));

        return null; 
    }

    private bool IsDefinedInFlightCategories(Categories category)
    {
        return _capacityCount.ContainsKey(category);
    }
}