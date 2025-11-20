namespace Internship_3_OOP.ClassDirectory;

public class StaffMember:Person
{
    public enum MemberTypeEnum
    {
        Pilot,
        Copilot,
        Stewardess,
        Steward
    }
    public MemberTypeEnum StaffMemberType;
    
    private static Dictionary<MemberTypeEnum,StaffMember> _crewMembers=new Dictionary<MemberTypeEnum,StaffMember>();
    public StaffMember(string name, string surname,int birthYear, char gender,
        MemberTypeEnum staffMemberType) :
        base(name, surname,birthYear, gender)
    {
        this.StaffMemberType = staffMemberType;
    }

    public static void AddStaffMember()
    {
        var name = Passenger.PassengerNameInput("ime");
        var surname = Passenger.PassengerNameInput("prezime");
        var birthYear = Helper.YearInput("rođenja");
        var gender = Helper.GenderInput();
        var type=ChooseType();
        
        if (!Helper.ConfirmationMessage("dodati člana osoblja"))
        {
            Console.WriteLine("\nOdustao si od dodvanja novog člana osoblja.\n");
            return;
        }     
        var registeredStaffMember=new StaffMember( name,surname,birthYear,gender,type);
        Console.WriteLine("\nUspješna registracija");
        registeredStaffMember.StaffMemberOutput();
    }

    private void StaffMemberOutput()
    {
        Console.WriteLine($"{this.Name} - {this.Surname} - {this.BirthYear} - {this.Gender} - {this.StaffMemberType}");
    }

    private static MemberTypeEnum ChooseType()
    {
        AvailibleTypes();
        while (true)
        {
            Console.Write("Unesi tip člana osoblja (broj): ");
            if (!IsTypeValid(out var type))
            {
                Console.WriteLine("Pogrešan format unosa.\n");
                continue;
            }

            if (!Helper.IsDefinedInEnum(type))
            {
                Console.WriteLine("\nVrijednost nije definirana.\n");
                continue;
            }

            return type;

        }
    }

    private static void AvailibleTypes()
    {
        Console.WriteLine("\nDostupni tipovi za člana osoblja.\n");
        var i = 0;
        foreach (var type in Enum.GetNames(typeof(MemberTypeEnum)))
        {
            Console.WriteLine($"{i}: {type}");
            i++;
        }
    }

    private static bool IsTypeValid(out MemberTypeEnum type)
    {
        var input = Console.ReadKey().KeyChar;
        return Enum.TryParse(input.ToString(),true, out type);
    }

}