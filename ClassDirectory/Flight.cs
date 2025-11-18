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

    public Flight(int id, string name, DateTime departureDate,DateTime arrivalDate,double distance,TimeSpan flightTime,Airplane airplane)
    {
        this.Id = id; 
        this.Name = name;
        this.DepartureDate = departureDate;
        this.ArrivalDate = arrivalDate;
        this.Distance = distance;
        this.FlightTime = flightTime;
        this.Airplane = airplane;
        this.CreationTime = DateTime.Now;
        this.UpdateTime = DateTime.Now;
    }
    public static void AddFlight()
    {
        var id = Helper.IdGenerator();
        var name = FlightNameInput();
        var distance = FlightDistanceInput();
        var departureDate = DepartureDateInput();
        var arrivalDate = ArrivalDateInput(departureDate);
        var flightTime = (arrivalDate - departureDate).Duration();
        var airplane = ChooseAirplane();
        airplane.FlightCount++;

        if (Helper.ConfirmationMessage("dodati novi let"))
        {
            _flightList.Add(new Flight(id, name, departureDate, arrivalDate,distance, flightTime, airplane));
        }

    }

    private static string FlightNameInput()
    {
        while (true)
        {
            Console.Write("\nUnesi ime leta (npr. AA789 ili AB7894): ");
            var inputName = Console.ReadLine()!.Trim().ToUpper();
            inputName=Helper.RemoveWhiteSpace(inputName);
            if (Regex.IsMatch(inputName, @"^[A-Z]{2}[0-9]{3,4}$") && !_flightList.Any(flight=>flight.Name==inputName))
            {
                return inputName;
            }
            else Console.WriteLine("Pogrešan unos imena leta.\n");
        }
    }

    private static DateTime DepartureDateInput()
    {
        while (true)
        {
            Console.Write("Unesi kada je let pošao.(YYYY-MM-DD HH:mm): ");
            if (DateTime.TryParseExact(Console.ReadLine()!.Trim(),"yyyy-MM-dd HH:mm",CultureInfo.InvariantCulture,
                    DateTimeStyles.None,   out var inputDate) 
                && inputDate >= DateTime.MinValue && inputDate <= DateTime.Today)
                return inputDate;

            else  Console.WriteLine("\nPogrešan unos datuma.");
        }
    }
    private static DateTime ArrivalDateInput(DateTime departureDate)
    {
        while (true)
        {
            Console.Write("Unesi kada je let završio.(YYYY-MM-DD HH:mm) ");
            if (DateTime.TryParseExact(Console.ReadLine()!.Trim(),"yyyy-MM-dd HH:mm",CultureInfo.InvariantCulture,
                    DateTimeStyles.None,   out var inputDate) 
                && inputDate >= DateTime.MinValue && inputDate <= DateTime.Today && inputDate>departureDate)
                return inputDate;
            else if(inputDate<=departureDate)
                Console.WriteLine("\nAvion ne može sletjeti prije nego što je uzletio.");
            else  Console.WriteLine("\nPogrešan unos datuma.");
        }
    }

    private static Airplane ChooseAirplane()
    {
        Console.WriteLine("Lista dostupnih aviona");
        ClassDirectory.Airplane.AirplaneOutput();
    
        while (true)
        {
            Console.WriteLine("Pridruži let zrakoplovu");
            var searchIndex = Airplane.FormatAndSearchByName();
            if (searchIndex!=-1)
            {
                return Airplane.ListAt(searchIndex);
            }
        }
    }

    private static double FlightDistanceInput()
    {
        while (true)
        {
            Console.Write("Unesi prijeđenu udaljenost u KM: ");
            if (Double.TryParse(Console.ReadLine(), out var inputDist) && inputDist > 0)
                return inputDist;
            else Console.WriteLine("Pogrešan unos kilometraže.\n");
        }
    }
    
}