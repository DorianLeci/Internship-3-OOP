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
    public static string EmailInput()
    {
        while(true)
        {
            Console.WriteLine("\nUnesi email.");
            var inputEmail = Console.ReadLine()!;
            inputEmail=Helper.RemoveWhiteSpace(inputEmail);
            if (EmailCheck(inputEmail))
                return inputEmail;
            else
                Console.WriteLine("\nPogrešan unos emaila .Mora sadržavati @ znak.");
        }        
    }

    public static bool EmailCheck(string inputEmail)
    {
        return (!string.IsNullOrEmpty(inputEmail) && Regex.IsMatch(inputEmail,@"^\p{L}+\@\p{L}+\.\p{L}+"));
    
    }

    public static bool PhoneInput(string inputPhone )
    {
        return (!string.IsNullOrEmpty(inputPhone) && Regex.IsMatch(inputPhone,@"^\+\d{3}\s\d{3}-\d{3}-\d{4}$"));
    }

    public static string PasswordInput()
    {
        while(true)
        {
            Console.WriteLine("\nUnesi lozinku.Minimalno osam znakova.Mora sadržavati bar jedno veliki slovo i jedan specijalni znak");
            var inputPassword = Console.ReadLine()!;
            inputPassword=Helper.RemoveWhiteSpace(inputPassword);
            if (PasswordCheck(inputPassword))
                return inputPassword;
            else
                Console.WriteLine("\nPogrešan unos lozinke.");
        }           
    }

    public static bool PasswordCheck(string inputPassword)
    {
        if (string.IsNullOrEmpty(inputPassword))
            return false;
        
        bool isSpecialChar = inputPassword.Any(ch => !char.IsLetterOrDigit(ch));
        bool isUpperChar = inputPassword.Any(ch => char.IsUpper(ch));
        
        return  (isSpecialChar && isUpperChar && inputPassword.Length>=8);
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
            if (char.TryParse(Console.ReadLine(),out var inputGender) && GenderCheck(inputGender))
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

}