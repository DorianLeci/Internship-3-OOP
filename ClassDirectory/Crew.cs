namespace Internship_3_OOP.ClassDirectory;

using CrewDict=Dictionary<string, List<StaffMember>>;

public class Crew
{
    private static CrewDict _crews = new CrewDict();

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
        _crews[crewName] = [];
        
        var pilot = StaffMember.ChooseMember(StaffMember.MemberTypeEnum.Pilot);
        _crews[crewName].Add(pilot);
        
        var copilot= StaffMember.ChooseMember(StaffMember.MemberTypeEnum.Copilot);
        _crews[crewName].Add(copilot);
        
        AddFlightAttendant(crewName);

        if (!Helper.ConfirmationMessage("unijeti novu posadu"))
        {
            Console.WriteLine("\nOdustao si od unosa nove posade.\n");
            _crews.Remove(crewName);            
        }

        Console.WriteLine($"\nUspješan unos nove posade u trenutku: {copilot.CreationTime}\n");
        SingleCrewOutput(new KeyValuePair<string, List<StaffMember>>(crewName,_crews[crewName]));
        



    }

    private static void AddFlightAttendant(string crewName)
    {
        for (int i = 0; i < 2; i++)
        {
            var gender = Helper.GenderInput();
            var argument=(gender=='M') ? StaffMember.MemberTypeEnum.Steward : StaffMember.MemberTypeEnum.Stewardess;
            var flightAttendant=StaffMember.ChooseMember(argument);
            _crews[crewName].Add(flightAttendant);
        }
    }
    public static void AddCrewMembers(string crewName,List<StaffMember> memberList)
    {
        _crews.Add(crewName, memberList);
    }

    public static bool IsMemberInAnyCrew(int memberId)
    {
        return _crews.Any(crew =>StaffMember.MemberExistenceCheck(memberId,crew.Value));
    }

    public static void AllCrewsOutput()
    {
        Helper.SleepAndClear();
        Console.WriteLine("\nPrikaz svih posada.\n");
        foreach (var crew in _crews)
            SingleCrewOutput(crew);

    }

    private static void SingleCrewOutput(KeyValuePair<string, List<StaffMember>> crew)
    {
        var enumerable = crew.Value.Select(member=>member.StaffMemberStringReduced());
        var joinString=string.Join(", ", enumerable);
        Console.WriteLine($"{crew.Key} - [{joinString}]");

        Console.WriteLine("\nPrikaz liste članova\n");
        StaffMember.MembersListOutput(crew.Value);
    }


    
}