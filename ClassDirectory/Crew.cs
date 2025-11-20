namespace Internship_3_OOP.ClassDirectory;

using CrewDict=Dictionary<string, List<StaffMember>>;
public class Crew
{
    private static CrewDict _crewMembers = new CrewDict();

    public static void CreateNewCrew(string crewName)
    {
        _crewMembers[crewName] = [];
    }
    public static void AddCrewMember(string crewName,List<StaffMember> memberList)
    {
        foreach (var member in memberList)
        {
            _crewMembers[crewName].Add(member);
        }
    }


    
}