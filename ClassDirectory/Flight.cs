namespace Internship_3_OOP.ClassDirectory;

public class Flight
{
    public int Id { get; }
    public string Name { get; }
    public DateOnly DepartureDate { get; }
    public DateOnly ArrivalDate { get; }
    public double Distance { get; }
    public TimeOnly FlightTime { get; }
    
    public Airplane Airplane { get; }
    private List<Flight> _flights = new List<Flight>();
    
}