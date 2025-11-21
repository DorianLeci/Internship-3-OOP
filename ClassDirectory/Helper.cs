namespace Internship_3_OOP.ClassDirectory;
using System;
using System.Text.RegularExpressions;
public class Helper
{
    private static int _idCounter = 0;
    private enum GenderEnum
    {
        M,
        F
    }

    public static void WaitingUser()
    {
        Console.WriteLine("\nČeka se any key korisnika....");
        Console.ReadKey();
    }
    public static string NameSurnameInput(string message)
    {
        Helper.SleepAndClear();
        Console.WriteLine("\nUnos podatka: {0}",message);
        while (true)
        {
            Console.Write("\nUnesi {0}: ",message);
            var inputPassenger = Console.ReadLine()!.ToLower();
            var removed=Helper.RemoveWhiteSpace(inputPassenger);
            if (!ValidateNameSurname(removed))
            {
                Console.WriteLine("\nPogrešan unos\n");
                continue;
            }
            var formattedInput = Helper.ReturnFormattedInput(inputPassenger);
            return formattedInput;

        }

    }
    private static bool ValidateNameSurname(string inputPassenger)
    {
        return (!string.IsNullOrEmpty(inputPassenger) && inputPassenger.All(ch => char.IsLetter(ch)));
    }
    public static string RemoveWhiteSpace(string inputString)
    {
        return new string(inputString.Where(ch => !char.IsWhiteSpace(ch)).ToArray());
    }
    public static string[] FormatNameSurname(string[] inputArray)
    {
        int i = 0;
        foreach (var nameSurname in inputArray)
        {
            inputArray[i] = char.ToUpper(nameSurname[0]) + nameSurname.Substring(1);
            if (i != (inputArray.Length - 1))
                inputArray[i] += " ";
            i++;
        }

        return inputArray;
    }
    public static string ReturnFormattedInput(string inputPlane)
    {
        var inputArray = Helper.FormatNameSurname(inputPlane.Split(" ",StringSplitOptions.RemoveEmptyEntries));
        var formattedInput = string.Concat(inputArray);
        return formattedInput;
    }   
    public static bool EmailCheck(string inputEmail)
    {
        return (!string.IsNullOrEmpty(inputEmail) && Regex.IsMatch(inputEmail,@"^\w+\@\p{L}+\.\p{L}+"));
    
    }
    

    public static int YearInput(string message)
    {
        SleepAndClear();
        Console.WriteLine("\nUnos godine.");
        while(true)
        {
            Console.Write("\nUnesi godinu {0}. Starost mora barem 18 godina i ili unos mora biti poslije 1950 godine: ",message);
            if (!IsIntegerValid(out var inputYear))
            {
                Console.WriteLine("\nPogrešan format unosa.\n");
                continue;
            }

            if (!YearCheck(inputYear))
            {
                Console.WriteLine("\nOsoba je mlađa od 18 godina.\n");
                continue;
            }

            if (!IsToOld(inputYear))
            {
                Console.WriteLine("\nOsoba je prestara.Unesi godinu rođenja poslije 1950 godine.\n");
                continue;
            }

            return inputYear;
        }               
    }

    private static bool YearCheck(int inputYear)
    {
        return (DateTime.Now.Year-inputYear)>=18;
    }

    private static bool IsToOld(int inputYear)
    {
        return inputYear>1950;
    }

    public static char GenderInput(string message="")
    {
        SleepAndClear();
        Console.WriteLine($"\nUnos spola {message}");
        while(true)
        {
            Console.Write("\nUnesi spol.(M,F) ili (m,f): ");
            if (!EnumFormatCheck(out GenderEnum inputGender))
            {
                Console.WriteLine("\nPogrešan format unosa.\n");
                continue;
            }

            if (!IsDefinedInEnum(inputGender))
            {
                Console.WriteLine("\nSpol nije definiran.\n");
                continue;
            }

            return inputGender.ToString()[0];
        }               
    }
    
    public static bool ConfirmationMessage(string messageType)
    {
        while (true)
        {
            Console.WriteLine("\nŽeliš li {0} -- y/n.\n", messageType);
            var charInput=Console.ReadKey().KeyChar;
            switch (charInput)
            {
                case 'y':
                    return true;
                case 'n':
                    Console.WriteLine("\nOperacija obustavljena.\n");
                    return false;
                default:
                    Console.WriteLine("Pogrešan unos.Pokušaj ponovno.\n");
                    break;
            }            
        }

    }

    public static int IdGenerator()
    {
        return _idCounter++;
    }

    public static bool DoubleInputCheck(out double input)
    {
        return Double.TryParse(Console.ReadLine()!.Trim(), out input);
    }

    public static void MessagePrintAndSleep(string message)
    {
        Console.WriteLine(message);
        Thread.Sleep(1000);
    }

    public static void SleepAndClear()
    {
        Thread.Sleep(400);
        Console.Clear();
    }
    public static bool IsIntegerValid(out int inputId)
    {
        return int.TryParse(Console.ReadLine()?.Trim(), out inputId);
    }
    public static int FormatAndSearchByName<T>(List<T> objectList) where T : IHasName
    {
        Console.Write("\nUnesi ime: ");
        
        var inputItem = Console.ReadLine()!.ToLower();
        var formattedInput=(typeof(T)==typeof(Airplane)? Helper.ReturnFormattedInput(inputItem):RemoveWhiteSpace(inputItem));
        
        var exist = Helper.ObjectExists(formattedInput,objectList);
        if (!exist)
            return -1;

        return objectList.FindIndex(item => 
            string.Equals(item.Name, formattedInput, StringComparison.OrdinalIgnoreCase));

    }
    public static bool ObjectExists<T>(string inputName,List<T> objectList) where T : IHasName
    {
        return (objectList.Count > 0) && objectList.Any(item => string.Equals(item.Name,inputName,StringComparison.OrdinalIgnoreCase));
    }
    public static bool IsDefinedInEnum<T>(T input) where T:Enum
    {
        return Enum.IsDefined(typeof(T), input);
    }
    public static bool EnumFormatCheck<T>(out T inputItem) where T:struct,Enum
    {
        var input = Console.ReadKey().KeyChar;
        return Enum.TryParse(input.ToString(),true, out inputItem);
    }
}