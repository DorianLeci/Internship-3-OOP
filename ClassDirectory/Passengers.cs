namespace Internship_3_OOP.ClassDirectory;

public class Passenger
{
    public Guid Id { get; }
    public string Name { get; }
    public string Surname { get; }
    public string Email { get; }
    public string Password { get; }
    public DateOnly BirthDate { get; }
    
    public char Gender { get; }

    private static List<Passenger> _passengerList = new List<Passenger>();
    public Passenger(Guid id, string name, string surname, string email,string password,DateOnly birthDate,char gender)
    {
        this.Id = id;
        this.Name = name;
        this.Surname = surname;
        this.Email = email;
        this.Password = password;
        this.BirthDate = birthDate;
        this.Gender = gender;
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
            {
                Console.WriteLine("Ostalo još: {0} pokušaja",counter);
            }
            else if (counter == 0)
            {
                return "";
            }
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
    public static bool IsEmailUnique(string inputEmail)
    {
        return _passengerList.All(passenger=>passenger.Email != inputEmail);
    }

    public static bool IsPassengerListEmpty()
    {
        return _passengerList.Count == 0;
    }
}