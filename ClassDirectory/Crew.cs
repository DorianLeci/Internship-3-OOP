namespace Internship_3_OOP.ClassDirectory;

using CrewDict=Dictionary<string, List<StaffMember>>;

public class Crew
{
    public int Id { get; }
    public string CrewName { get; }
    private List<StaffMember> _crewMemberList;
    private static List<Crew> _crewList = [];
    private DateTime CreationTime { get; }
    private DateTime UpdateTime { get; }

    public Crew(string crewName,List<StaffMember> crewMemberList)
    {
        this.Id = Helper.IdGenerator();
        this.CrewName = crewName;
        this._crewMemberList = crewMemberList;
        this.CreationTime=DateTime.Now;
        this.UpdateTime=DateTime.Now;
    }

    public void AddToCrewList()
    {
        _crewList.Add(this);
    }
    public static void CreateNewCrew()
    {
        Helper.SleepAndClear();
        if (StaffMember.ListOfAllAvailableMembers(StaffMember.MemberTypeEnum.Pilot).Count==0)
        {
            Console.WriteLine("\nNe postoji niti jedan doustpan pilot.Unesi nove pilote pa pokušaj ponovno kasnije.\n");
            return;
        }
        
        if (StaffMember.ListOfAllAvailableMembers(StaffMember.MemberTypeEnum.Copilot).Count==0)
        {
            Console.WriteLine("\nNe postoji niti jedan doustupan kopilot.Unesi nove kopilote pa pokušaj ponovno kasnije.\n");
            return;
        }

        var stewardList = StaffMember.ListOfAllAvailableMembers(StaffMember.MemberTypeEnum.Steward);
        var stewardessList = StaffMember.ListOfAllAvailableMembers(StaffMember.MemberTypeEnum.Stewardess);
        if (stewardList.Count <2 || stewardessList.Count <2)
        {
            Console.WriteLine($"\nDodaj bar 2 stjuarda i bar 2 stjuardese.\nTrenutni broj dostupnih sjuarda: {stewardList.Count}.\nTrenutni broj dostupnih stjuardesa: {stewardessList.Count}.\n");
            return;
        }
        
        var crewName = Helper.NameSurnameInput("ime posade");
        
        var pilot = StaffMember.ChooseMember(StaffMember.MemberTypeEnum.Pilot);
        
        var copilot= StaffMember.ChooseMember(StaffMember.MemberTypeEnum.Copilot);
        
        var flightAttendant1=AddFlightAttendant(crewName);
        var flightAttendant2=AddFlightAttendant(crewName);

        if (!Helper.ConfirmationMessage("unijeti novu posadu"))
        {
            Console.WriteLine("\nOdustao si od unosa nove posade.\n");
            return;
        }

        var newCrew = new Crew(crewName, [pilot, copilot, flightAttendant1, flightAttendant2]);
        newCrew.AddToCrewList();
        
        Console.WriteLine($"\nUspješan unos nove posade u trenutku: {newCrew.CreationTime}\n");
        SingleCrewOutput(newCrew);
        



    }

    private static StaffMember AddFlightAttendant(string crewName)
    {
        var gender = Helper.GenderInput();
        var argument=(gender=='M') ? StaffMember.MemberTypeEnum.Steward : StaffMember.MemberTypeEnum.Stewardess;
        var flightAttendant=StaffMember.ChooseMember(argument);

        return flightAttendant;
    }
    
    public static bool IsMemberInAnyCrew(int memberId)
    {
        return _crewList.Any(crew =>StaffMember.MemberExistenceCheck(memberId,crew._crewMemberList));
    }

    public static void AllCrewsOutput()
    {
        Helper.SleepAndClear();
        Console.WriteLine("\nPrikaz svih posada.\n");
        foreach (var crew in _crewList)
            SingleCrewOutput(crew);

    }
    
    private static void SingleCrewOutput(Crew crew)
    {
        crew.GeneralCrewInfo();
        
        Console.WriteLine("\nPrikaz liste članova\n");
        StaffMember.MembersListOutput(crew._crewMemberList);
    }

    public void GeneralCrewInfo()
    {
        var enumerable = this._crewMemberList.Select(member=>member.StaffMemberStringReduced());
        var joinString=string.Join(", ", enumerable);
        Console.WriteLine($"{this.Id} - {this.CrewName} - [{joinString}]");        
    }
    public static List<Crew> FindAvailableCrews(DateTime depDateTime,DateTime arrDateTime,List<Flight>flightList)
    {
        var found = flightList.FindAll(flight =>FlightsOverlap(flight.DepartureDate,flight.ArrivalDate,depDateTime,arrDateTime)).Distinct().ToList();
        var busyCrews=found.Select(flight => flight.FlightCrew).ToList();
        var crewListFiltrated=_crewList.Where(crew=>!busyCrews.Contains(crew)).ToList();

        return crewListFiltrated;
    }

    private static bool FlightsOverlap(DateTime start1,DateTime end1,DateTime start2,DateTime end2)
    {
        return start1 < end2 && start2 < end1;
    }
    
    
}