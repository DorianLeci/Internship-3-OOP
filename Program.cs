using Internship_3_OOP.ClassDirectory;
namespace Internship_3_OOP;

class Program
{
    private static Random _rnd = new Random();

    static void Main(string[] args)
    {
        MainMenu();
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
                    Console.WriteLine("Uspješan odabir izbornika za putnike.\n");
                    PassengerInfoInput(true);
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

    static void PassengerInfoInput(bool isNewPassenger)
    {
        var name = Helper.NameSurnameInput("ime");
        var surname = Helper.NameSurnameInput("prezime");
        var email = Helper.EmailInput();
        var password = Helper.PasswordInput();
        var birthDate = Helper.BirthDateInput();

        // if (isNewPassenger)
        //     var newPassenger = new Passenger(Rnd.Next(1, int.MaxValue), name, surname, email, phone);
        // else if (!isNewPassenger)
        //     Passenger.CheckPassenger();

    }
}
    

