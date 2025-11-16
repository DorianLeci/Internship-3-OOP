using Internship_3_OOP.ClassDirectory;
namespace Internship_3_OOP;

class Program
{
    private static Random _rnd = new Random();

    static void Main(string[] args)
    {
        DataSeed();
        MainMenu();
    }

    static void DataSeed()
    {
        var pass1 = new Passenger(Guid.NewGuid(), "Dorian", "Leci", "zandzartz@gmail.com", "FujF48Ym#",
            new DateOnly(2004, 3, 28), 'M');
        var pass2 = new Passenger(Guid.NewGuid(), "Nikola", "Filipović", "nfilip5@net.hr", "1234567#A",
            new DateOnly(2004, 4, 27), 'M');
        var pass3 = new Passenger(Guid.NewGuid(), "Marija", "Hanić", "marijah@gmail.com", "123AbcdE#",
            new DateOnly(2005, 6, 10), 'F');
    }
    static void MainMenu()
    {
        while (true)
        {
            Console.WriteLine("----------------------");
            Console.WriteLine("1 - Putnici\n");
            Console.WriteLine("2 - Letovi\n");
            Console.WriteLine("3 - Avioni\n");
            Console.WriteLine("4 - Posada\n");
            Console.WriteLine("0 - Izlaz iz programa");
            Console.WriteLine("----------------------\n");
            if (int.TryParse(Console.ReadLine(), out int inputMainMenu))
            {
                switch (inputMainMenu)
                {
                    case 0:
                        Console.WriteLine("Uspješan odabir.Izlaz iz aplikacije\n");
                        Environment.Exit(0);
                        break;
                    case 1:
                        Console.WriteLine("Uspješan odabir izbornika za putnike.\n");
                        PassengerMenu();
                        break;
                    default:
                        Console.WriteLine("Unos nije među ponuđenima.Unesi ponovno");
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nPogrešan tip podatka->unesi cijeli broj.");
            }
        }
    }

    static void PassengerMenu()
    {
        while (true)
        {
            Console.WriteLine("----------------------");
            Console.WriteLine("1 - Registracija\n");
            Console.WriteLine("2 - Prijava\n");
            Console.WriteLine("0 - Izlaz iz programa");
            Console.WriteLine("----------------------\n");
            if (int.TryParse(Console.ReadLine(), out int inputMainMenu))
            {
                switch (inputMainMenu)
                {
                    case 0:
                        Console.WriteLine("Uspješan odabir.Izlaz iz aplikacije\n");
                        Environment.Exit(0);
                        break;
                    case 1:
                        Console.WriteLine("Uspješan odabir.Registracija putnika.\n");
                        PassengerRegistration(true);
                        Helper.WaitingUser();
                        break;
                    case 2:
                        if (Passenger.IsPassengerListEmpty())
                        {
                            Console.WriteLine("Moraš prvo registrirati nekog korisnika prije nego što možeš pristupiti prijavi.");
                        }
                        else
                        {
                            Console.WriteLine("Uspješan odabir.Prijava putnika\n");
                            PassengerLogin(false);                           
                        }

                        Helper.WaitingUser();
                        break;
                    default:
                        Console.WriteLine("Unos nije među ponuđenima.Unesi ponovno");
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nPogrešan tip podatka->unesi cijeli broj.");
            }
        }
    }

    static void PassengerRegistration(bool isPassengerNew)
    {

        var name = Helper.NameSurnameInput("ime");
        var surname = Helper.NameSurnameInput("prezime");
        var email = Passenger.EmailRegisterInput();
        var password = Helper.PasswordInput();
        var birthDate = Helper.BirthDateInput();
        var gender = Helper.GenderInput();
        
        if (Helper.ConfirmationMessage("dodati putnika(izvršiti registraciju"))
        {
            var registeredPassenger=new Passenger(Guid.NewGuid(), name,surname, email, password, birthDate, gender);
            Console.WriteLine("Uspješna registracija");
        }      
    }
    static void PassengerLogin(bool isPassengerNew)
    {
        var email = Passenger.EmailLoginInput();
        if (email == "")
        {
            Console.WriteLine("Neuspješna prijava.Pokušaj ponovno kasnije.\n");
            return;
        }
        var password = Helper.PasswordInput();

    }
    
}
    

