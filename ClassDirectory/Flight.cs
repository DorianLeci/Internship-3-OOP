using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Internship_3_OOP.ClassDirectory;

public class Flight
{
    public int Id { get; }
    public string Name { get; }
    public DateTime DepartureDate { get; }
    public DateTime ArrivalDate { get; }
    public double Distance { get; }
    public TimeSpan FlightTime { get; }
    public DateTime CreationTime { get; }
    public DateTime UpdateTime { get; }
    public Airplane Airplane { get; }
    private static List<Flight> _flightList = new List<Flight>();

    public Flight(string name, DateTime departureDate,DateTime arrivalDate,double distance,TimeSpan flightTime,Airplane airplane)
    {
        this.Id = Helper.IdGenerator();
        this.Name = name;
        this.DepartureDate = departureDate;
        this.ArrivalDate = arrivalDate;
        this.Distance = distance;
        this.FlightTime = flightTime;
        this.Airplane = airplane;
        this.CreationTime = DateTime.Now;
        this.UpdateTime = DateTime.Now;
    }

    public void AddToList()
    {
        _flightList.Add(this);
    }
    public static void AddFlight()
    {
        var name = FlightNameInput();
        var distance = FlightDistanceInput();
        var departureDate = DepartureDateInput();
        var arrivalDate = ArrivalDateInput(departureDate);
        var flightTime = (arrivalDate - departureDate).Duration();
        var airplane = ChooseAirplane();
        airplane.FlightCount++;

        if (!Helper.ConfirmationMessage("dodati novi let"))
            return;
        else _flightList.Add(new Flight(name, departureDate, arrivalDate,distance, flightTime, airplane));

    }

    private static string FlightNameInput()
    {
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
            if(_flightList.Count!=0 && _flightList.All(flight=>flight.Name==inputName))
            {
                Console.WriteLine("Već postoji let s istim imenom.Mora biti jedinstveno.\n");
                continue;
            }

            return inputName;
        }
    }

    private static DateTime DepartureDateInput()
    {
        while (true)
        {
            Console.Write("Unesi kada let polazi.(YYYY-MM-DD HH:mm): ");
            if (FlightDateCheck(out var inputDateTime))
                return inputDateTime;
            
            else  Console.WriteLine("\nPogrešan unos datuma.");
        }
    }
    private static DateTime ArrivalDateInput(DateTime departureDateTime)
    {
        while (true)
        {
            Console.Write("Unesi kada let završava.(YYYY-MM-DD HH:mm) ");
            if (!FlightDateCheck(out var inputDateTime))
            {
                Console.WriteLine("Pogrešan format datuma.Pokušaj ponovno.\n");
                continue;               
            }

            if (!CheckArrivalTime(inputDateTime, departureDateTime))
            {
                Console.WriteLine("Vrijeme sletanja ne smije biti prije vremena polijetanja.\n");
                continue;
            }

            if (!CheckFlightDuration(inputDateTime, departureDateTime))
            {
                Console.WriteLine("Let ne smije trajati duže od 24 sata.\n");
                continue;
            }
            
            return inputDateTime;
        }
    }
    private static bool FlightDateCheck(out DateTime inputDateTime)
    {
        var today = DateTime.Now;
        var input = Console.ReadLine()!.Trim();

        return DateTime.TryParseExact(input, "yyyy-MM-dd HH:mm",
            CultureInfo.InvariantCulture, DateTimeStyles.None, out inputDateTime) && inputDateTime.Date>=today.Date;
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
        Console.WriteLine("Lista dostupnih aviona");
        ClassDirectory.Airplane.AirplaneOutput();
    
        while (true)
        {
            Console.WriteLine("Pridruži let avionu");
            var searchIndex = Airplane.FormatAndSearchByName();
            if (searchIndex != -1) return Airplane.ListAt(searchIndex);
            
            Console.WriteLine("Ne postoji avion s unesenim imenom.\n");
            continue;
        }
    }

    private static double FlightDistanceInput()
    {
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
    
}