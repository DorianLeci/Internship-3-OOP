namespace Internship_3_OOP.ClassDirectory;

public class Passenger
{
    public Guid Id { get; }
    public string Name { get; }
    public string Surname { get; }
    public string Email { get; }
    private string _password;
    public int BirthYear { get; }
    public char Gender { get; }

    public DateTime creationTime { get; }
    public DateTime updateTime { get; }

    private static List<Passenger> _passengerList = new List<Passenger>();
    public Passenger(Guid id, string name, string surname, string email,string password,int birthYear,char gender)
    {
        this.Id = id;
        this.Name = name;
        this.Surname = surname;
        this.Email = email;
        this._password = password;
        this.BirthYear = birthYear;
        this.Gender = gender;
        this.creationTime = DateTime.Now;
        this.updateTime = DateTime.Now;
        _passengerList.Add(this);
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
                Console.WriteLine("Osoba s ovim emailom već postoji.Unesi drugi email.\n");
            
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