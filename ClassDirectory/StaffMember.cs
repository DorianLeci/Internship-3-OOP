
using System.Globalization;

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

    private static Dictionary<MemberTypeEnum, List<StaffMember>> _staffMembers =
        new()
        {
            { MemberTypeEnum.Pilot, [] },
            { MemberTypeEnum.Copilot, [] },
            { MemberTypeEnum.Steward, [] },
            { MemberTypeEnum.Stewardess, [] }
        };
    public StaffMember(string name, string surname,int birthYear, char gender,
        MemberTypeEnum staffMemberType) :
        base(name, surname,birthYear, gender)
    {
        this.StaffMemberType = staffMemberType;
    }

    public void AddToList()
    {
        _staffMembers[this.StaffMemberType].Add(this);
    }
    public static StaffMember? AddStaffMember(MemberTypeEnum? defaultMemberType=null)
    {
        var name = Helper.NameSurnameInput("ime");
        var surname = Helper.NameSurnameInput("prezime");
        var birthYear = Helper.YearInput("rođenja");
        var type=defaultMemberType ?? ChooseType();
        var gender = type switch
        {
            MemberTypeEnum.Steward => 'M',
            MemberTypeEnum.Stewardess => 'F',
            _ => Helper.GenderInput()
        };

        if (!Helper.ConfirmationMessage("dodati člana osoblja"))
        {
            Console.WriteLine("\nOdustao si od dodvanja novog člana osoblja.\n");
            return null;
        }     
        var registeredStaffMember=new StaffMember( name,surname,birthYear,gender,type);
        registeredStaffMember.AddToList();
        
        Console.WriteLine($"\nUspješno dodavanje novog člana osoblja u trenutku: {registeredStaffMember.CreationTime:yyyy-MM-dd HH:mm}\n");
        registeredStaffMember.StaffMemberOutput();

        return registeredStaffMember;
    }

    private void StaffMemberOutput()
    {
        Console.WriteLine($"{this.Id} - {this.Name} - {this.Surname} - {this.BirthYear} - {this.Gender} - {this.StaffMemberType}");
    }

    public string StaffMemberStringReduced()
    {
        return string.Join(" - ", [this.Id, this.Name, this.Surname]);
    }

    private static MemberTypeEnum ChooseType()
    {
        Helper.SleepAndClear();
        Console.WriteLine("\nUnos tipa člana osoblja.");
        AvailableTypes();
        while (true)
        {
            Console.Write("\nUnesi tip člana osoblja (broj): ");
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

    private static void AvailableTypes()
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

    public static StaffMember ChooseMember(MemberTypeEnum staffMemberType)
    {
        Helper.SleepAndClear();
        Console.WriteLine($"\nOdabir osobe tipa : {staffMemberType}.\n");
        
        var list=ListOfAllAvailableMembers(staffMemberType);
        AvailableMembersListOutput(list,staffMemberType);
        
        while (true)
        {
            Console.Write("\nUnesi id osobe koju želiš dodati u posadu: ");
            if (!Helper.IsIntegerValid(out var inputId))
            {
                Console.WriteLine("\nPogrešan format unosa.\n");
                continue;
            }
            
            if (!MemberExistenceCheck(inputId, list))
            {
                Console.WriteLine($"\nNe postoji dostupna osoba tipa {staffMemberType} s unesenim id-om.\n");
                continue;
            }

            return list.Find(member=>member.Id==inputId)!;
        }

    }

    public static bool MemberExistenceCheck(int id,List<StaffMember> list)
    {
        return list.Any(member=>member.Id==id);
    }
    
    public static List<StaffMember> ListOfAllAvailableMembers(MemberTypeEnum type)
    {
        var list = new List<StaffMember>();
        foreach (var member in _staffMembers[type])
        {
            if (Crew.IsMemberInAnyCrew(member.Id)) continue;
            list.Add(member);
        }
        
        return list;
    }

    private static void AvailableMembersListOutput(List<StaffMember> list,MemberTypeEnum type)
    {
        Console.WriteLine($"\nPrikaz svih dostupnih osoba tipa {type} (onih koji nisu članovi niti jedne posade) : \n");
        MembersListOutput(list);
    }

    public static void MembersListOutput(List<StaffMember> list)
    {
        Console.WriteLine("\n----------");
        foreach (var member in list)
        {
            member.StaffMemberOutput();
        }       
        Console.WriteLine("---------------\n");
    }

}