namespace Internship_3_OOP.ClassDirectory;
using System;
using System.Text.RegularExpressions;
public class Helper
{
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
    public static string NameSurnameInput(string inputVar)
    {
        while (true)
        {
            Console.WriteLine("Unesi {0}", inputVar);
            var inputString = Console.ReadLine()!.ToLower();
            inputString = RemoveWhiteSpace(inputString);
            if (NameSurnameCheck(inputString))
            {
                var inputArray = FormatNameSurname(inputString.Split(" ",StringSplitOptions.RemoveEmptyEntries));
                return string.Concat(inputArray);
            }
            else Console.WriteLine("\nPogrešan unos {0}na .Ne smije biti prazno ili sadržavati brojeve/specijalne znakove.", inputVar);
        }
    }

    public static bool NameSurnameCheck(string inputString)
    {
        return (!string.IsNullOrEmpty(inputString) && inputString.All(ch => char.IsLetter(ch) || ch == '-'));
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
    
    public static bool EmailCheck(string inputEmail)
    {
        return (!string.IsNullOrEmpty(inputEmail) && Regex.IsMatch(inputEmail,@"^\w+\@\p{L}+\.\p{L}+"));
    
    }
    

    public static DateOnly BirthDateInput()
    {
        while(true)
        {
            Console.WriteLine("\nUnesi datum rođenja.Ne smije biti noviji od današnjeg datuma.");
            if (DateOnly.TryParse(Console.ReadLine()?.Trim(), out var date) && date.ToDateTime(new TimeOnly())<=DateTime.Now.Date)
                return date;
            else
                Console.WriteLine("\nPogrešan unos datuma.");
        }               
    }

    public static char GenderInput()
    {
        while(true)
        {
            Console.WriteLine("\nUnesi spol.(M,F) ili (m,f)");
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
        Console.WriteLine("\nŽeliš li zaista {0} -- y/n. Ako je unos krajnjeg odabira neispravan ili je odabir 'n' operacija se obustavlja.\n", messageType);
        if (char.TryParse(Console.ReadLine()?.Trim().ToLower(), out var inputChar) && inputChar == 'y')
            return true;
        else if (inputChar == 'n')
        {
            Console.WriteLine(
                "\nOperacija obustavljena.Povratak na prethodni izbornik nakon pritiska bilo koje tipke.\n");
            return false;
        }
        else
        {
            Console.WriteLine("\nUnos neispravan.Operacija se obustavljena.Povratak na prethodni izbornik nakon pritiska bilo koje tipke.\n");
            return false;
        }
    }
}