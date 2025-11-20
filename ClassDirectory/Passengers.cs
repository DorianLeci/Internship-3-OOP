namespace Internship_3_OOP.ClassDirectory;

public class Passenger:Person
{
    public string Email { get; }
    private readonly string _password;
    private static List<Passenger> _passengerList = new List<Passenger>();

    public Passenger(string name, string surname, string email, int birthYear, char gender, string password) :
        base(name, surname, birthYear, gender)
    {
        this.Email = email;
        this._password = password;
        _passengerList.Add(this);
    }
    public static void PassengerRegistration(bool isPassengerNew)
    {
        Helper.SleepAndClear();
        var name = PassengerNameInput("ime");
        var surname = PassengerNameInput("prezime");
        var email = EmailRegisterInput();
        var password = PasswordRegisterInput();
        var birthYear = Helper.YearInput("rođenja");
        var gender = Helper.GenderInput();
        
        if (!Helper.ConfirmationMessage("dodati putnika(izvršiti registraciju"))
        {
            Console.WriteLine("\nOdustao si od unosa novog putnika.\n");
            return;
        }      
        var registeredPassenger=new Passenger( name,surname, email,birthYear,gender, password);
        Console.WriteLine("\nUspješna registracija\n");
        registeredPassenger.PassengerInfo();
        Console.WriteLine();
    }

    private void PassengerInfo()
    {
        Console.WriteLine($"{this.Name} -  {this.Surname} - {this.Email} - {this.BirthYear} - {this.Gender} - {this._password}");
    }
    public static void PassengerLogin(bool isPassengerNew)
    {
        Helper.SleepAndClear();
        var email = Passenger.EmailLoginInput();
        if (email == "")
        {
            Console.WriteLine("Neuspješna prijava.Pokušaj ponovno kasnije.\n");
            return;
        }

        var password = Passenger.PasswordLoginInput(email);
        if(password == "")
            Console.WriteLine("Neuspješna prijava pokušaj ponovno kasnije.\n");
    }
    public static string PassengerNameInput(string message)
    {
        Helper.SleepAndClear();
        Console.WriteLine("\nUnos: {0}na",message);
        while (true)
        {
            Console.Write("\nUnesi {0}: ",message);
            var inputPassenger = Console.ReadLine()!.ToLower();
            var removed=Helper.RemoveWhiteSpace(inputPassenger);
            if (!ValidatePassengerName(removed))
            {
                Console.WriteLine("\nPogrešan unos\n");
                continue;
            }
            var formattedInput = Helper.ReturnFormattedInput(inputPassenger);
            return formattedInput;

        }

    }
    private static bool ValidatePassengerName(string inputPassenger)
    {
        return (!string.IsNullOrEmpty(inputPassenger) && inputPassenger.All(ch => char.IsLetter(ch)));
    }
    private static string EmailRegisterInput()
    {
        Helper.SleepAndClear();
        Console.WriteLine("\nUnos emaila");
        while(true)
        {   
            Console.Write("\nUnesi email: ");
            var inputEmail = Console.ReadLine()!;
            inputEmail=Helper.RemoveWhiteSpace(inputEmail);

            if (!Helper.EmailCheck(inputEmail))
            {
                Console.WriteLine("Pogrešan format unosa.\n");
                continue;
            }
            
            if (!IsEmailUnique(inputEmail))
            {
                Console.WriteLine("\nEmail već postoji u sustavu.Mora biti jedinstven.\n");
                continue;
            }
            return inputEmail;
            
        }        
    }
    private static string EmailLoginInput()
    {
        Helper.SleepAndClear();
        Console.WriteLine("\nUnos emaila.");
        var counter = 5;
        while(true)
        {
            switch (counter)
            {
                case 0:
                    return "";
                
                case > 0 and < 5:
                    Console.WriteLine("Ostalo još: {0} pokušaja",counter);
                    break;
            }
            Console.Write("\nUnesi email: ");
            var inputEmail = Console.ReadLine()!.Trim();
            inputEmail=Helper.RemoveWhiteSpace(inputEmail);

            if (!Helper.EmailCheck(inputEmail))
            {
                Console.WriteLine("\nPogrešan format unosa.\n");
                counter--;
                continue;
            }

            if (IsEmailUnique(inputEmail))
            {
                Console.WriteLine("\nNe postoji takav mail u sustavu.\n");
                counter--;
                continue;
            }
            return inputEmail;
            
        }        
    }   
    private static bool IsEmailUnique(string inputEmail)
    {
        return _passengerList.All(passenger=>passenger.Email != inputEmail);
    }

    public static bool IsPassengerListEmpty()
    {
        return _passengerList.Count == 0;
    }

    private static bool DoPasswordsMatch(string inputPassword,string inputEmail)
    {
        return _passengerList.Any(passenger => passenger._password == inputPassword && passenger.Email == inputEmail);
    }
    private static string PasswordRegisterInput()
    {
        while(true)
        {
            Console.WriteLine("\nUnesi lozinku.Minimalno osam znakova.Mora sadržavati bar jedno veliki slovo i jedan specijalni znak");
            var inputPassword = Console.ReadLine()!;
            inputPassword=Helper.RemoveWhiteSpace(inputPassword);
            if (PasswordCheck(inputPassword))
                return inputPassword;
            else Console.WriteLine("\nPogrešan unos lozinke.");
        }           
    }

    private static string PasswordLoginInput(string inputEmail)
    {
        var counter = 5;
        while (true)
        {
            switch (counter)
            {
                case > 0 and < 5:
                    Console.WriteLine("Ostalo još: {0} pokušaja",counter);
                    break;
                case 0:
                    return "";
            }
            Console.WriteLine("Unesi lozinku.");

            
            var inputPassword = Console.ReadLine()!.Trim();
            if (!Passenger.DoPasswordsMatch(inputPassword, inputEmail))
            {
                Console.WriteLine("\nLozinke se ne podudaraju.\n");
                counter--;
                continue;
            }
            Console.WriteLine("\nUspješna prijava.\n");
            return inputPassword;
        }        
    }
        

    private static bool PasswordCheck(string inputPassword)
    {
        if (string.IsNullOrEmpty(inputPassword))
            return false;
        
        var isSpecialChar = inputPassword.Any(ch => !char.IsLetterOrDigit(ch));
        var isUpperChar = inputPassword.Any(ch => char.IsUpper(ch));
        
        return  (isSpecialChar && isUpperChar && inputPassword.Length>=8);
    }
}
