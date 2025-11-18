namespace Internship_3_OOP.ClassDirectory;

public class Seed
{
    private static List<Airplane> _airplanes = [];
    public static void DataSeed()
    {
        var pass1 = new Passenger(Guid.NewGuid(), "Dorian", "Leci", "zandzartz@gmail.com", "FujF48Ym#",
            2004, 'M');
        var pass2 = new Passenger(Guid.NewGuid(), "Nikola", "Filipović", "nfilip5@net.hr", "1234567#A",
            2004, 'M');
        var pass3 = new Passenger(Guid.NewGuid(), "Marija", "Hanić", "marijah@gmail.com", "123AbcdE#",
            2005, 'F');
        
        AirplaneSeed();
        FlightSeed();
    }
    private static void AirplaneSeed()
    {
        var catDict1 = new Dictionary<Categories, int>()
        {
            {Categories.Economy,200},
            {Categories.Business,100},
            {Categories.Standard,150},
            { Categories.Vip ,50}
        };
        var airplane1 = new Airplane("Croatia 1",2004,catDict1);    
        airplane1.AddToList();
        
        var catDict2 = new Dictionary<Categories, int>()
        {
            {Categories.Economy,200},
            {Categories.Business,80},
            {Categories.Standard,130},
        };
        var airplane2 = new Airplane("Croatia 2",2004,catDict2);  
        airplane2.AddToList();
        
        var catDict3 = new Dictionary<Categories, int>()
        {
            {Categories.Economy,150},
            {Categories.Business,180},
            {Categories.Standard,130},
        };
        var airplane3 = new Airplane("Croatia 3",2004,catDict3);   
        airplane3.AddToList();
        
        _airplanes.AddRange([airplane1, airplane2, airplane3]);
        
    }
    private static void FlightSeed()
    {
        var depDate = new DateOnly(2025, 12, 11);
        var depTime = new TimeOnly(12, 30);
        var arrDate = new DateOnly(2025, 12, 11);
        var arrTime = new TimeOnly(13,30);
        var depDateTime = depDate.ToDateTime(depTime);
        var  arrDateTime = arrDate.ToDateTime(arrTime);
        var flightTime = (depDateTime - arrDateTime).Duration();
        
        var flight1 = new Flight("CR789", depDateTime, arrDateTime, 150, flightTime, _airplanes[1]);
        flight1.AddToList();

        depDate = new DateOnly(2025, 12, 12);
        depTime = new TimeOnly(12,40);
        arrDate = new DateOnly(2025, 12, 13);
        arrTime = new TimeOnly(03,40);
        depDateTime=depDate.ToDateTime(depTime);
        arrDateTime = arrDate.ToDateTime(arrTime);
        flightTime = (depDateTime - arrDateTime).Duration();   
        
        var flight2 = new Flight("CR800", depDateTime, arrDateTime, 150, flightTime, _airplanes[2]);
        flight2.AddToList();
        
    }    
    
}