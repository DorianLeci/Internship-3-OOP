using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Internship_3_OOP.ClassDirectory;

public class Flight: IHasName
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
    public Crew FlightCrew { get; }
    private static List<Flight> _flightList = new List<Flight>();

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
    }

    public void AddToList()
    {
        _flightList.Add(this);
    }
    public static void AddFlight()
    {
        Console.Clear();
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

    public static void FlightFormattedOutput()
    {
        Console.WriteLine("\n--------------");
        foreach(var flight in _flightList)
        {
            flight.OutputForOneFlight();
        }

        Console.WriteLine("--------------\n");
    }

    private void OutputForOneFlight()
    {
        var depDateTimeString = this.DepartureDate.ToString("yyyy-MM-dd HH:mm");
        var arrDateTimeString=this.ArrivalDate.ToString("yyyy-MM-dd HH:mm");
        Console.WriteLine("{0} - {1} - {2} - {3} - {4} -{5} - {6} - {7} - {8}\n",this.Id,this.Name,depDateTimeString,arrDateTimeString,
            this.Distance,this.FlightTime,this.Airplane.Name,this.FlightCrew.Id,this.FlightCrew.CrewName);        
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
        Helper.SleepAndClear();
        Console.WriteLine("\nLista dostupnih aviona\n");
        Airplane.AirplaneOutput();
    
        while (true)
        {
            Console.WriteLine("Pridruži let avionu");
            var searchIndex = Airplane.LinkFlightAndAirplane();
            if (searchIndex != -1) return Airplane.ListAt(searchIndex);
            
            Console.WriteLine("Ne postoji avion s unesenim imenom.\n");
        }
    }
    private static Crew ChooseCrew(DateTime depDateTime,DateTime arrDateTime)
    {
        Helper.SleepAndClear();
        Console.WriteLine("\nPridruživanje posade letu (prikazuju se samo posade koje nisu zauzete u trenutku ovog leta).\n");
        
        var foundCrews=Crew.FindAvailableCrews(depDateTime, arrDateTime,_flightList);
        
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
                        var searchedFlightById = SearchById();
                        if (searchedFlightById == null)
                        {
                            Helper.WaitingUser();
                            break;
                        }
                        Helper.MessagePrintAndSleep("\nUspješan pronalazak aviona.");
                        searchedFlightById.OutputForOneFlight();
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
                        searchedAirplaneByName.OutputForOneFlight();
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
    private static Flight? SearchById()
    {
        Console.Clear();
        AllAvailableFlights();
        do
        {
            Console.Write("\nUnesi id: ");
            if (!Helper.IsIntegerValid(out var inputId))
            {
                Console.WriteLine("Pogrešan format unosa.");
                continue;               
            }
            
            var exist = _flightList.Any(plane => plane.Id == inputId);
            if (!exist)
            {
                Console.WriteLine("Let s traženim id-om ne postoji");
                continue;
            }
            return _flightList.Find(plane => plane.Id == inputId);
                    
        } while (Helper.ConfirmationMessage("ponovno unijeti id"));

        return null;        
    }

    private static Flight? SearchByName()
    {
        Console.Clear();
        AllAvailableFlights();
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
    private static void AllAvailableFlights()
    {
        Console.WriteLine("Ispis svih dostupnih letova");
        foreach (var flight in _flightList)
        {
            Console.WriteLine("{0} - {1} - {2}",flight.Id,flight.Name,flight.Airplane.Name);
        }
    }

    public static void FlightEdit()
    {
        Console.Clear();
        AllAvailableFlights();
        var returnedFlight=SearchById();

        returnedFlight?.FoundFlightEdit();
    }

    private void FoundFlightEdit()
    {
        var oldDepDateTime = this.DepartureDate;
        var oldArrDateTime = this.ArrivalDate;
        var newDepDateTime=DepDateTimeEdit(oldDepDateTime);
        var newArrDateTime=ArrDateTimeEdit(newDepDateTime,oldArrDateTime);

        if (oldDepDateTime == newDepDateTime && oldArrDateTime == newArrDateTime)
        {
            Console.WriteLine("Ništa nisi promijenio.\n");
            return;
        }

        if (!Helper.ConfirmationMessage($"izmijeni let: {this.Id} - {this.Name} - {this.Airplane.Name}"))
        {
            Console.WriteLine("\nOdustao si od izmjene.\n");
            return;
        }

        Console.WriteLine($"\nUspješna izmjena leta: {this.Id} - {this.Name} - {this.Airplane.Name}\n");
        this.ArrivalDate = newArrDateTime;
        this.DepartureDate = newDepDateTime;
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
            return ArrivalDateInput(depDateTime);
        }
        if (!CheckFlightDuration(oldArrDateTime, depDateTime))
        {
            Console.WriteLine("\nPromijenio si datum i vrijeme polaska leta i sada let traje duže od 24 sata.\n");
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
    
}