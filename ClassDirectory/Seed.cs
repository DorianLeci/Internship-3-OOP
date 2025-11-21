namespace Internship_3_OOP.ClassDirectory;

public class Seed
{
    private static List<Airplane> _airplanes = [];
    private static List<Crew> _crewMemberList = [];
    private static List<Flight> _flights = [];
    public static void DataSeed()
    {
        var pass1 = new Passenger("Dorian", "Leci", "zandzartz@gmail.com", 2004,'M', "FujF48Ym#");
        var pass2 = new Passenger("Nikola", "Filipović", "nfilip5@net.hr", 2004,'M', "1234567#A");
        var pass3 = new Passenger("Marija", "Hanić", "marijah@gmail.com", 2005,
            'F', "123AbcdE#");
        
        AirplaneSeed();
        StaffMemberSeed();
        FlightSeed();
        
        pass1.AddToFlightDict(_flights[0],_flights[0].GetCategories()[1]);
        pass2.AddToFlightDict(_flights[0],_flights[0].GetCategories()[1]);
        pass3.AddToFlightDict(_flights[1],_flights[1].GetCategories()[1]);
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
        
        var catDict4 = new Dictionary<Categories, int>()
        {
            {Categories.Economy,1},
            {Categories.Business,1},
        };
        var airplane4 = new Airplane("Croatia 4",2002,catDict4);   
        airplane4.AddToList();
        
        _airplanes.AddRange([airplane1, airplane2, airplane3,airplane4]);
        
    }
    private static void FlightSeed()
    {
        var depDate = new DateOnly(2025, 11, 22);
        var depTime = new TimeOnly(12, 30);
        var arrDate = new DateOnly(2025, 11, 22);
        var arrTime = new TimeOnly(13,30);
        var depDateTime = depDate.ToDateTime(depTime);
        var  arrDateTime = arrDate.ToDateTime(arrTime);
        var flightTime = (depDateTime - arrDateTime).Duration();
        
        var flight1 = new Flight("CR789", depDateTime, arrDateTime, 150, flightTime, _airplanes[1],_crewMemberList[0]);
        flight1.AddToList();
        _flights.Add(flight1);
        _airplanes[1].FlightCount++;

        
        depDate = new DateOnly(2025, 12, 12);
        depTime = new TimeOnly(11,40);
        arrDate = new DateOnly(2025, 12, 13);
        arrTime = new TimeOnly(03,40);
        depDateTime=depDate.ToDateTime(depTime);
        arrDateTime = arrDate.ToDateTime(arrTime);
        flightTime = (depDateTime - arrDateTime).Duration();
        
        var flight2 = new Flight("CR800", depDateTime, arrDateTime, 110, flightTime, _airplanes[2],_crewMemberList[1]);
        flight2.AddToList();
        _flights.Add(flight2);
        _airplanes[2].FlightCount++;

        
        depDate = new DateOnly(2025, 12, 17);
        depTime = new TimeOnly(12,40);
        arrDate = new DateOnly(2025, 12 ,17);
        arrTime = new TimeOnly(18,40);
        depDateTime=depDate.ToDateTime(depTime);
        arrDateTime = arrDate.ToDateTime(arrTime);
        flightTime = (depDateTime - arrDateTime).Duration();   
        
        var flight3 = new Flight("CR801", depDateTime, arrDateTime, 200, flightTime, _airplanes[0],_crewMemberList[2]);
        flight3.AddToList();
        _flights.Add(flight3);
        _airplanes[0].FlightCount++;
        
        depDate = new DateOnly(2025, 12, 25);
        depTime = new TimeOnly(11,40);
        arrDate = new DateOnly(2025, 12 ,25);
        arrTime = new TimeOnly(14,40);
        depDateTime=depDate.ToDateTime(depTime);
        arrDateTime = arrDate.ToDateTime(arrTime);
        flightTime = (depDateTime - arrDateTime).Duration();   
        
        var flight4 = new Flight("CR802", depDateTime, arrDateTime, 300, flightTime, _airplanes[0],_crewMemberList[0]);
        flight4.AddToList();
        _airplanes[0].FlightCount++;
        
        depDate = new DateOnly(2025, 12, 26);
        depTime = new TimeOnly(08,40);
        arrDate = new DateOnly(2025, 12 ,26);
        arrTime = new TimeOnly(14,45);
        depDateTime=depDate.ToDateTime(depTime);
        arrDateTime = arrDate.ToDateTime(arrTime);
        flightTime = (depDateTime - arrDateTime).Duration();   
        
        var flight5 = new Flight("CR803", depDateTime, arrDateTime, 250, flightTime, _airplanes[3],_crewMemberList[0]);
        flight5.AddToList();
        _airplanes[3].FlightCount++;
        
    }

    private static void StaffMemberSeed()
    {
        CrewSeed();
        
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

    private static void CrewSeed()
    {
        var member1 = new StaffMember("Ante", "Antić",2004, 'M', StaffMember.MemberTypeEnum.Pilot);
        var member2=new StaffMember("Petar", "Petrović", 1985, 'M', StaffMember.MemberTypeEnum.Copilot);
        var member3=new StaffMember("Roko", "Roković", 1985, 'F', StaffMember.MemberTypeEnum.Steward);
        var member4=new StaffMember("Vesna", "Vesnić", 1993, 'F', StaffMember.MemberTypeEnum.Stewardess);
        
        member1.AddToList();
        member2.AddToList();
        member3.AddToList();
        member4.AddToList();
        
        var newCrew=new Crew("First Crew" ,[member1, member2, member3, member4]);
        newCrew.AddToCrewList();
        _crewMemberList.Add(newCrew);
        
        
        member1 = new StaffMember("Mia", "Mia",2001, 'F', StaffMember.MemberTypeEnum.Pilot);
        member2=new StaffMember("Kristijan", "Kristijanić", 1986, 'M', StaffMember.MemberTypeEnum.Copilot);
        member3=new StaffMember("Božo", "Božić", 1985, 'F', StaffMember.MemberTypeEnum.Stewardess);
        member4=new StaffMember("Vesna", "Vesnić", 1993, 'F', StaffMember.MemberTypeEnum.Stewardess); 
        
        member1.AddToList();
        member2.AddToList();
        member3.AddToList();
        member4.AddToList();
        
        newCrew=new Crew("Second Crew", [member1, member2, member3, member4]);
        newCrew.AddToCrewList();
        _crewMemberList.Add(newCrew);
        
        
        member1 = new StaffMember("Mišo", "Kovač",1978, 'M', StaffMember.MemberTypeEnum.Pilot);
        member2=new StaffMember("Andrej", "Andrejić", 1986, 'M', StaffMember.MemberTypeEnum.Copilot);
        member3=new StaffMember("Božidar", "Božidarević", 2000, 'M', StaffMember.MemberTypeEnum.Steward);
        member4=new StaffMember("Krešo", "Krešić", 2005, 'M', StaffMember.MemberTypeEnum.Steward); 
        
        member1.AddToList();
        member2.AddToList();
        member3.AddToList();
        member4.AddToList();
        
        newCrew=new Crew("Third Crew", [member1, member2, member3, member4]);
        newCrew.AddToCrewList();
        _crewMemberList.Add(newCrew);
        
        member1 = new StaffMember("Luka", "Lukić",1995, 'M', StaffMember.MemberTypeEnum.Pilot);
        member2=new StaffMember("Hana", "Hanić", 1998, 'F', StaffMember.MemberTypeEnum.Copilot);
        member3=new StaffMember("Tesa", "Tesić", 2001, 'F', StaffMember.MemberTypeEnum.Stewardess);
        member4=new StaffMember("Lana", "Lanić", 2005, 'F', StaffMember.MemberTypeEnum.Stewardess); 
        
        member1.AddToList();
        member2.AddToList();
        member3.AddToList();
        member4.AddToList();
        
        newCrew=new Crew("Fourth Crew", [member1, member2, member3, member4]);
        newCrew.AddToCrewList();
        _crewMemberList.Add(newCrew);
        
    }
}