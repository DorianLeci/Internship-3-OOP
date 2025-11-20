namespace Internship_3_OOP.ClassDirectory;

public class Seed
{
    private static List<Airplane> _airplanes = [];
    public static void DataSeed()
    {
        var pass1 = new Passenger("Dorian", "Leci", "zandzartz@gmail.com", 2004,'M', "FujF48Ym#");
        var pass2 = new Passenger("Nikola", "Filipović", "nfilip5@net.hr", 2004,'M', "1234567#A");
        var pass3 = new Passenger("Marija", "Hanić", "marijah@gmail.com", 2005,
            'F', "123AbcdE#");
        
        AirplaneSeed();
        FlightSeed();
        StaffMemberSeed();
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
        _airplanes[1].FlightCount++;

        depDate = new DateOnly(2025, 12, 12);
        depTime = new TimeOnly(12,40);
        arrDate = new DateOnly(2025, 12, 13);
        arrTime = new TimeOnly(03,40);
        depDateTime=depDate.ToDateTime(depTime);
        arrDateTime = arrDate.ToDateTime(arrTime);
        flightTime = (depDateTime - arrDateTime).Duration();
        
        var flight2 = new Flight("CR800", depDateTime, arrDateTime, 150, flightTime, _airplanes[2]);
        flight2.AddToList();
        _airplanes[2].FlightCount++;
        
    }

    private static void StaffMemberSeed()
    {
        var member1 = new StaffMember("Ante", "Antić",2004, 'M', StaffMember.MemberTypeEnum.Pilot);
        var member2=new StaffMember("Petar", "Petrović", 1985, 'M', StaffMember.MemberTypeEnum.Copilot);
        var member3=new StaffMember("Ana", "Anić", 1985, 'F', StaffMember.MemberTypeEnum.Copilot);
        var member4=new StaffMember("Vesna", "Vesnić", 1993, 'F', StaffMember.MemberTypeEnum.Stewardess);
        
        member1.AddToList();
        member2.AddToList();
        member3.AddToList();
        member4.AddToList();
        
        Crew.AddCrewMembers("Crew 1", [member1, member2, member3, member4]);

        var newPilot1 = new StaffMember("Dorian", "Leci", 2003, 'M', StaffMember.MemberTypeEnum.Pilot);
        var newPilot2=new StaffMember("Nikola", "Filipović", 2003, 'M', StaffMember.MemberTypeEnum.Pilot);
        
        var newCopilot2=new StaffMember("Zora", "Zorić", 1971, 'F', StaffMember.MemberTypeEnum.Copilot);
        var newCopilot1=new StaffMember("Vesna", "Leci", 1971, 'F', StaffMember.MemberTypeEnum.Copilot);

        
        var newSteward1=new StaffMember("Damir", "Leci", 1969, 'M', StaffMember.MemberTypeEnum.Steward);
        var newSteward2=new StaffMember("Dinko", "Dinković", 2004, 'M', StaffMember.MemberTypeEnum.Steward);
        var newStewardess1=new StaffMember("Jana", "Janić", 1975, 'F', StaffMember.MemberTypeEnum.Stewardess);
        var newStewardess2=new StaffMember("Marija", "Marić", 1980, 'F', StaffMember.MemberTypeEnum.Stewardess);
        
        newPilot1.AddToList();
        newPilot2.AddToList();
        
        newCopilot1.AddToList();
        newCopilot2.AddToList();
        
        newSteward1.AddToList();
        newSteward2.AddToList();
        
        newStewardess1.AddToList();
        newStewardess2.AddToList();
        
    }
}