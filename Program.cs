using System.Data;
using System.Linq.Expressions;
using Internship_3_OOP.ClassDirectory;
namespace Internship_3_OOP;

class Program
{
    private static Random _rnd = new Random();

    static void Main(string[] args)
    {
        Seed.DataSeed();
        MainMenu();
    }
    
    static void MainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\n----------------------");
            Console.WriteLine("1 - Putnici\n");
            Console.WriteLine("2 - Letovi\n");
            Console.WriteLine("3 - Avioni\n");
            Console.WriteLine("4 - Posada\n");
            Console.WriteLine("0 - Izlaz iz programa");
            Console.WriteLine("----------------------\n");
            Console.Write("Unos: ");
            var input = Console.ReadKey().KeyChar;
            switch (input)
            {
                case '0':
                    Helper.MessagePrintAndSleep("\nUspješan odabir.Izlaz iz aplikacije");
                    Environment.Exit(0);
                    break;
                case '1':
                    Helper.MessagePrintAndSleep("\nUspješan odabir izbornika za putnike.");
                    PassengerMenu();
                    break;
                case '2':
                    Helper.MessagePrintAndSleep("\nUspješan odabir izbornika za letove.");
                    FlightMenu();
                    break;
                case '3':
                    Helper.MessagePrintAndSleep("\nUspješan odabir izbornika za avione.");
                    AirplaneMenu();
                    break; 
                case '4':
                    Helper.MessagePrintAndSleep("\nUspješan odabir izbornika za posade.");
                    StaffMemberMenu();
                    break;     
                default:
                    Helper.MessagePrintAndSleep("\nUnos nije među ponuđenima.Unesi ponovno");
                    break;
                }
        }
    }

    public static void PassengerMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\n----------------------");
            Console.WriteLine("1 - Registracija\n");
            Console.WriteLine("2 - Prijava\n");
            Console.WriteLine("0 - Povratak na glavni izbornik.");
            Console.WriteLine("----------------------\n");
            Console.Write("Unos: ");
            var input = Console.ReadKey().KeyChar;
                switch (input)
                {
                    case '0':
                        Helper.MessagePrintAndSleep("\nUspješan odabir.Povratak na glavni izbornik.\n");
                        MainMenu();
                        break;
                    case '1':
                        Helper.MessagePrintAndSleep("\nUspješan odabir.Registracija putnika.\n");
                        Passenger.PassengerRegistration();
                        Helper.WaitingUser();
                        break;
                    case '2':
                        if (Passenger.IsPassengerListEmpty())
                        {
                            Console.WriteLine("Moraš prvo registrirati nekog korisnika prije nego što možeš pristupiti prijavi.");
                        }
                        else
                        {
                            Helper.MessagePrintAndSleep("\nUspješan odabir.Prijava putnika\n");
                            Passenger.PassengerLogin();                           
                        }

                        Helper.WaitingUser();
                        break;
                    default:
                        Helper.MessagePrintAndSleep("\nUnos nije među ponuđenima.Unesi ponovno");
                        break;
                }
        }
    }
    

    public static void AirplaneMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\n----------------------");
            Console.WriteLine("1 - Prikaz svih aviona\n");
            Console.WriteLine("2 - Dodavanje aviona\n");
            Console.WriteLine("3 - Pretraživanje aviona\n");
            Console.WriteLine("4 - Brisanje aviona\n");
            Console.WriteLine("0 - Povratak na glavni izbornik");
            Console.WriteLine("----------------------\n");
            Console.Write("Unos: ");
            var input = Console.ReadKey().KeyChar;
                switch (input)
                {
                    case '0':
                        Helper.MessagePrintAndSleep("\nUspješan odabir.Povratak na glavni izbornik.\n");
                        MainMenu();
                        break;
                    case '1':
                        if (Airplane.IsAirplaneListEmpty())
                        {
                            Helper.MessagePrintAndSleep("\nLista aviona je prazna.Ne možeš ih prikazati.\n");
                            Helper.WaitingUser();
                            break;                           
                        }
                        
                        Helper.MessagePrintAndSleep("\nUspješan odabir.Prikaz svih aviona.");
                        Airplane.AirplaneOutput();                            

                        Helper.WaitingUser();
                        break;
                    case '2':
                        Helper.MessagePrintAndSleep("\nUspješan odabir.Dodavanje aviona.\n");
                        var isNewAirplaneCreated = Airplane.AddAirplane();

                        if (!isNewAirplaneCreated)
                        {
                            Console.WriteLine("\nNije dodan novi avion.");       
                            Helper.WaitingUser();
                            break;
                        }
                        Console.Clear();
                        Helper.MessagePrintAndSleep("\nUspješno dodan novi avion.");
                        Console.WriteLine("Vrijeme stvaranja: {0}",Airplane.GetLastElement()?.creationTime);
                        Airplane.GetLastElement()?.FormattedAirplaneOutput();  
                        
                        Helper.WaitingUser();
                        break;
                    case '3':
                        if (Airplane.IsAirplaneListEmpty())
                        {
                            Helper.MessagePrintAndSleep("\nLista aviona je prazna.Ne možeš ih pretraživati.\n");
                            Helper.WaitingUser();
                            break;
                        }
                        Helper.MessagePrintAndSleep("\nUspješan odabir.Pretraživanje aviona.");
                        Airplane.AirplaneSearch();                            
                        Helper.WaitingUser();
                        break;
                    case '4':
                        if (Airplane.IsAirplaneListEmpty())
                        {
                            Helper.MessagePrintAndSleep("\nLista aviona je prazna.Ne možeš ih brisati.\n");
                            Helper.WaitingUser();
                            break;
                        }
                        Helper.MessagePrintAndSleep("\nUspješan odabir.Brisanje aviona.\n");
                        Airplane.DeleteAirplane();
                        
                        Helper.WaitingUser();
                        break;
                    default:
                        Helper.MessagePrintAndSleep("\nUnos nije među ponuđenima.Unesi ponovno");
                        break;
                }
        }        
    }

    public static void FlightMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\n----------------------");
            Console.WriteLine("1 - Izbornik za prikaz svih letova\n");
            Console.WriteLine("2 - Dodavanje leta\n");
            Console.WriteLine("3 - Pretraživanje letova\n");
            Console.WriteLine("4 - Uređivanje leta\n");
            Console.WriteLine("5 - Brisanje leta\n");
            Console.WriteLine("0 - Povratak na glavni izbornik.");
            Console.WriteLine("----------------------\n");
            Console.Write("Unos: ");
            var input = Console.ReadKey().KeyChar;
                switch (input)
                {
                    case '0':
                        Helper.MessagePrintAndSleep("\nUspješan odabir.Povratak na glavni izbornik.\n");
                        Console.WriteLine();
                        MainMenu();
                        break;
                    case '1':
                        Helper.MessagePrintAndSleep("\nUspješan odabir.Izbornik za prikaz svih letova\n");
                        Flight.FlightOutputMenu();
                        Helper.WaitingUser();
                        break;
                    case '2':
                        Helper.MessagePrintAndSleep("\nUspješan odabir.Dodavanje novog leta.\n");
                        Flight.AddFlight();
                        Helper.WaitingUser();
                        break;
                    case '3':
                        Helper.MessagePrintAndSleep("\nUspješan odabir.Pretraživanje leta.\n");
                        Flight.FlightSearch();
                        Helper.WaitingUser();
                        break;       
                    case '4':
                        Helper.MessagePrintAndSleep("\nUspješan odabir.Uređivanje leta.\n");
                        Flight.FlightEdit();
                        Helper.WaitingUser();
                        break;     
                    case '5':
                        Helper.MessagePrintAndSleep("\nUspješan odabir.Brisanje leta.\n");
                        Flight.FlightDelete();
                        Helper.WaitingUser();
                        break;   
                    default:
                        Helper.MessagePrintAndSleep("\nUnos nije među ponuđenima.Unesi ponovno.\n");
                        break;
                }
        }        
    }

    public static void StaffMemberMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\n----------------------");
            Console.WriteLine("1 - Prikaz svih posada\n");     
            Console.WriteLine("2 - Dodavanje nove posade\n");            
            Console.WriteLine("3 - Dodavanje novog člana osoblja\n");
            Console.WriteLine("0 - Povratak na glavni izbornik.");
            Console.WriteLine("----------------------\n");
            Console.Write("Unos: ");
            var input = Console.ReadKey().KeyChar;
                switch (input)
                {
                    case '0':
                        Helper.MessagePrintAndSleep("\nUspješan odabir.Povratak na glavni izbornik.\n");
                        Console.WriteLine();
                        MainMenu();
                        break;
                    case '1':
                        Helper.MessagePrintAndSleep("\nUspješan odabir.Prikaz svih posada\n");
                        Crew.AllCrewsOutput();
                        Helper.WaitingUser();
                        break;
                    case '2':
                        Helper.MessagePrintAndSleep("\nUspješan odabir.Dodavanje novog posade.\n");
                        Crew.CreateNewCrew();
                        Helper.WaitingUser();
                        break;
                    case '3':
                        Helper.MessagePrintAndSleep("\nUspješan odabir.Dodavanje novog člana osoblja.\n");
                        StaffMember.AddStaffMember();
                        Helper.WaitingUser();
                        break;       
                    default:
                        Helper.MessagePrintAndSleep("\nUnos nije među ponuđenima.Unesi ponovno.\n");
                        break;
                }
        }                
    }
}
    

