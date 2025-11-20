namespace Internship_3_OOP.ClassDirectory;

using CrewDict=Dictionary<string, List<StaffMember>>;

public class Crew
{
    private static CrewDict _crews = new CrewDict();

    public static void CreateNewCrew()
    {
        if (StaffMember.ListOfAllAvailableMembers(StaffMember.MemberTypeEnum.Pilot).Count==0)
        {
            Console.WriteLine("\nNe postoji niti jedan doustpan pilot.Unesi nove pilote pa pokušaj ponovno kasnije.\n");
            return;
        }

        var listOfCopilots = StaffMember.ListOfAllAvailableMembers(StaffMember.MemberTypeEnum.Copilot);
        if (StaffMember.ListOfAllAvailableMembers(StaffMember.MemberTypeEnum.Copilot).Count==0 
            || StaffMember.ListOfAllAvailableMembers(StaffMember.MemberTypeEnum.Copilot).Count==1)
        {
            Console.WriteLine($"\nPostoji samo {listOfCopilots.Count} (treba biti barem dva) doustupan kopilot.Unesi nove kopilote pa pokušaj ponovno kasnije.\n");
            return;
        }
        
        
        var crewName = Helper.NameSurnameInput("ime posade");
        
        var gender=Helper.GenderInput("M ili F (M- za stjuarda,F- za stjuardesu)");
        switch (gender)
        {
            case 'M':
                if (StaffMember.ListOfAllAvailableMembers(StaffMember.MemberTypeEnum.Steward).Count==0)
                {
                    Console.WriteLine("\nNe postoji niti jedan dostupan stjuard.Unesi nove stjuarde pa pokušaj ponovno kasnije.\n");
                    return;
                }

                break;
            case 'F':
                if (StaffMember.ListOfAllAvailableMembers(StaffMember.MemberTypeEnum.Stewardess).Count==0)
                {
                    Console.WriteLine("\nNe postoji niti jedna dostupna stjuardesa.Unesi nove stjuardese pa pokušaj ponovno kasnije.\n");
                    return;
                }
                break;
        }

        
        _crews[crewName] = [];
        
        var argument = (gender == 'M') ? StaffMember.MemberTypeEnum.Steward : StaffMember.MemberTypeEnum.Stewardess;
        var flightAttendant=StaffMember.ChooseMember(argument); 
        _crews[crewName].Add(flightAttendant);
        
        var pilot = StaffMember.ChooseMember(StaffMember.MemberTypeEnum.Pilot);
        _crews[crewName].Add(pilot);
        
        var copilot1= StaffMember.ChooseMember(StaffMember.MemberTypeEnum.Copilot);
        _crews[crewName].Add(copilot1);
        
        var copilot2 = StaffMember.ChooseMember(StaffMember.MemberTypeEnum.Copilot);
        _crews[crewName].Add(copilot2);

        if (Helper.ConfirmationMessage("unijeti novu posadu")) return;
        
        Console.WriteLine("\nOdustao si od unosa nove posade.\n");
        _crews.Remove(crewName);


    }
    
    public static void AddCrewMembers(string crewName,List<StaffMember> memberList)
    {
        _crews.Add(crewName, memberList);
    }

    public static bool IsMemberInAnyCrew(int memberId)
    {
        return _crews.Any(crew =>StaffMember.MemberExistenceCheck(memberId,crew.Value));
    }




    
}