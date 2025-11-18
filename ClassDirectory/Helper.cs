namespace Internship_3_OOP.ClassDirectory;
using System;
using System.Text.RegularExpressions;
public class Helper
{
    private static int _idCounter = 0;
    private enum GenderEnum
    {
        Male='M',
        Female='F'
    }

    public static void WaitingUser()
    {
        Console.WriteLine("Čeka se any key korisnika....");
        Console.ReadKey();
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
        while(true)
        {
            Console.Write("\nUnesi godinu {0}.Ne smije biti novija od trenutačne niti starija od prve godine: ",message);
            if (int.TryParse(Console.ReadLine()?.Trim(), out var date) && date<=DateTime.Today.Year && date>=DateTime.MinValue.Year)
                return date;
            else
                Console.WriteLine("\nPogrešan unos godine.");
        }               
    }

    public static char GenderInput()
    {
        while(true)
        {
            Console.Write("\nUnesi spol.(M,F) ili (m,f): ");
            if (char.TryParse(Console.ReadLine()?.ToUpper(),out var inputGender) && GenderCheck(inputGender))
                return inputGender;
            else
                Console.WriteLine("\nPogrešan unos spola.");
        }               
    }

    public static bool GenderCheck(char inputGender)
    {
        try
        {
            return Enum.IsDefined(typeof(GenderEnum), (int)inputGender);
        }
        catch (Exception)
        {
            return false;
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

    public static bool IsIdValid(out int inputId)
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
}