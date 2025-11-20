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

        var name = Passenger.PassengerNameInput("ime");
        var surname = Passenger.PassengerNameInput("prezime");
        var email = Passenger.EmailRegisterInput();
        var password = Passenger.PasswordRegisterInput();
        var birthYear = Helper.YearInput("rođenja");
        var gender = Helper.GenderInput();
        
        if (Helper.ConfirmationMessage("dodati putnika(izvršiti registraciju"))
        {
            var registeredPassenger=new Passenger( name,surname, email,birthYear,gender, password);
            Console.WriteLine("Uspješna registracija");
        }      
    }
    public static void PassengerLogin(bool isPassengerNew)
    {
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
        while (true)
        {
            Console.Write("\nUnesi {0}: ",message);
            var inputPassenger = Console.ReadLine()!.ToLower();
            var removed=Helper.RemoveWhiteSpace(inputPassenger);
            if (ValidatePassengerName(removed))
            {
                var formattedInput = Helper.ReturnFormattedInput(inputPassenger);
                return formattedInput;
            }
            else Console.WriteLine("Pogrešan unos.");
        }

    }
    private static bool ValidatePassengerName(string inputPassenger)
    {
        return (!string.IsNullOrEmpty(inputPassenger) && inputPassenger.All(ch => char.IsLetter(ch)));
    }
    public static string EmailRegisterInput()
    {
        while(true)
        {   
            Console.WriteLine("\nUnesi email.");
            var inputEmail = Console.ReadLine()!;
            inputEmail=Helper.RemoveWhiteSpace(inputEmail);
            
            if (Helper.EmailCheck(inputEmail) && IsEmailUnique(inputEmail))
                return inputEmail;
            
            else if(!IsEmailUnique(inputEmail))
                Console.WriteLine("Osoba s ovim emailom već postoji.Unesi drugi email.");
            
            else Console.WriteLine("\nPogrešan unos emaila.Unesi ponovno.");
        }        
    }
    public static string EmailLoginInput()
    {
        var counter = 5;
        while(true)
        {
            if (counter > 0 && counter<5)
                Console.WriteLine("Ostalo još: {0} pokušaja",counter);
            else if (counter == 0)
                return "";
            Console.WriteLine("\nUnesi email.");
            var inputEmail = Console.ReadLine()!;
            inputEmail=Helper.RemoveWhiteSpace(inputEmail);
            
            if (Helper.EmailCheck(inputEmail) && !IsEmailUnique(inputEmail))
                return inputEmail;
            else if(Helper.EmailCheck(inputEmail) && IsEmailUnique(inputEmail))
                Console.WriteLine("\nNe postoji taj mail u sustavu.");
            else Console.WriteLine("\nPogrešan unos emaila.Unesi ponovno.");

            counter--;
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
    public static string PasswordRegisterInput()
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

    public static string PasswordLoginInput(string inputEmail)
    {
        var counter = 5;
        while (true)
        {
            if (counter > 0 && counter<5)
                Console.WriteLine("Ostalo još: {0} pokušaja",counter);
            else if (counter == 0)
                return "";
            Console.WriteLine("Unesi lozinku.");

            
            var inputPassword = Console.ReadLine()!;
            if (Passenger.DoPasswordsMatch(inputPassword, inputEmail))
            {
                Console.WriteLine("Uspješna prijava.\n");
                return inputPassword;
            }
            else Console.WriteLine("Lozinke se ne poduduraju.\n");

            counter--;
        }        
    }
        

    private static bool PasswordCheck(string inputPassword)
    {
        if (string.IsNullOrEmpty(inputPassword))
            return false;
        
        bool isSpecialChar = inputPassword.Any(ch => !char.IsLetterOrDigit(ch));
        bool isUpperChar = inputPassword.Any(ch => char.IsUpper(ch));
        
        return  (isSpecialChar && isUpperChar && inputPassword.Length>=8);
    }
}
