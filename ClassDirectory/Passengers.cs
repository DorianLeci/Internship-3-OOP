namespace Internship_3_OOP.ClassDirectory;

public class Passenger:Person
{
    public string Email { get; }
    private readonly string _password;
    private static List<Passenger> _passengerList = [];
    private Dictionary<Flight,Categories> _usrFlightDict = new Dictionary<Flight,Categories>();

    public Passenger(string name, string surname, string email, int birthYear, char gender, string password) :
        base(name, surname, birthYear, gender)
    {
        this.Email = email;
        this._password = password;
        _passengerList.Add(this);
    }
    public static void PassengerRegistration()
    {
        Helper.SleepAndClear();
        var name = Helper.NameSurnameInput("ime");
        var surname = Helper.NameSurnameInput("prezime");
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
        Console.WriteLine($"\nUspješna registracija u trenutku: {registeredPassenger.CreationTime:yyyy-MM-dd HH:mm}\n");
        registeredPassenger.PassengerInfo();
        
    }

    private void PassengerInfo()
    {
        Console.WriteLine($"{this.Name} -  {this.Surname} - {this.Email} - {this.BirthYear} - {this.Gender} - {this._password}");
    }
    public static void PassengerLogin()
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
        
        Console.WriteLine("\nUspješna prijava.\n");
        PassengerFlightMenu(_passengerList.Find(passenger => string.Equals(passenger.Email,email) && string.Equals(passenger._password,password))!);
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
                    Console.WriteLine("Ostalo još: {0} pokušaja", counter);
                    break;
                case 0:
                    return "";
            }

            Console.Write("Unesi lozinku: ");

            
            var inputPassword = Console.ReadLine()!.Trim();
            if (!Passenger.DoPasswordsMatch(inputPassword, inputEmail))
            {
                Console.WriteLine("\nLozinke se ne podudaraju.\n");
                counter--;
                continue;
            }
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

    private static void PassengerFlightMenu(Passenger user)
    {
        while (true)
        {
            Helper.SleepAndClear();
            Console.WriteLine("\n----------------------");
            Console.WriteLine("1 - Prikaz svih rezerviranih letova\n");
            Console.WriteLine("2 - Rezervacija leta\n");
            Console.WriteLine("3 - Pretraživanje letova\n");
            Console.WriteLine("4 - Otkazivanje leta\n");
            Console.WriteLine("0 - Povratak na izbornik za korisnike.(Logout).");
            Console.WriteLine("----------------------\n");
            Console.Write("Unos: ");
            var input = Console.ReadKey().KeyChar;
            switch (input)
            {
                case '0':
                    Helper.MessagePrintAndSleep("\nUspješan odabir.Povratak na izbornik za putnike.Logout...\n");
                    Program.PassengerMenu();
                    break;
                case '1':
                    Helper.MessagePrintAndSleep("\nUspješan odabir.Prikaz svih letova koje si rezerviao.\n");
                    user.AllUserFlights();
                    Helper.WaitingUser();
                    break;
                case '2':
                    Helper.MessagePrintAndSleep("\nUspješan odabir.Rezervacija leta.\n");
                    user.ChooseFlight();
                    Helper.WaitingUser();
                    break;
                case '3':
                    Helper.MessagePrintAndSleep("\nUspješan odabir.Pretraživanje letova.\n");
                    user.SearchFlightMenu();
                    Helper.WaitingUser();
                    break;
                case '4':
                    Helper.MessagePrintAndSleep("\nUspješan odabir.Otkazivanje leta.\n");
                    user.CancelFlight();
                    Helper.WaitingUser();
                    break;                    
                default:
                    Helper.MessagePrintAndSleep("\nUnos nije među ponuđenima.Unesi ponovno.\n");
                    break;
            }            
        }

    }   
    private void ChooseFlight()
    {
        var flightsWithoutOverlap=Flight.AvailableFlightsForThisUser(_usrFlightDict.Keys.ToList());
        if (flightsWithoutOverlap.Count == 0)
        {
            Console.WriteLine("\nNe postoje dostupni letovi za ovog korisnika.Pokušaj ponovno kasnije.\n");
            return;
        }
        
        var chosenFlight = Flight.SearchById(flightsWithoutOverlap,true);
        
        if (chosenFlight == null)
        {
            Console.WriteLine("\nOdustao si od unošenja id-a leta pa si odustao i od rezervacije leta.\n");
            return;
        }
        
        Helper.SleepAndClear();
        chosenFlight.OutputCapacityForOneFlight();
        
        var chosenCategory = chosenFlight.ChooseCategory();

        if (chosenCategory == null)
        {
            Console.WriteLine("\nOdustao si od odabira kategorije u kojoj ćeš sjediti pa si odustao i od rezervacije leta.\n");
            return;
        }

        if (!Helper.ConfirmationMessage("rezervirati let"))
        {
            Console.WriteLine("\nIpak odustaješ od rezervacije leta.\n");
            return;
        }
        
        this.UpdateTime = DateTime.Now;
        this._usrFlightDict.Add(chosenFlight,chosenCategory.Value);
        
        chosenFlight.DecrementCategoryCapacity(chosenCategory.Value);
        
        Console.WriteLine($"\nUspješno rezerviran let {chosenFlight.Name} povezan s avionom {chosenFlight.Airplane.Name} u trenutku: {this.UpdateTime:yyyy-MM-dd HH:mm}." +
                          $"Odabrana kategorija: {chosenCategory}");
        
    }

    public void AddToFlightDict(Flight flight, Categories category)
    {
        this._usrFlightDict.Add(flight, category);
        flight.DecrementCategoryCapacity(category);
    }
    private void AllUserFlights()
    {
        if (_usrFlightDict.Count == 0)
        {
            Console.WriteLine("\nNisi rezervirao niti jedan let.\n");
            return;
        }
        Flight.AllAvailableFlights(false,_usrFlightDict.Keys.ToList());
    }

    private void CancelFlight()
    {
        Helper.MessagePrintAndSleep("\nOdaberi let koji želiš otkazazi.\n");
        var availableFlights=AvailableFlightsForCancelation(_usrFlightDict.Keys.ToList());
        if (availableFlights.Count == 0)
        {
            Console.WriteLine("\nLista dostupnih letova za otkazivanje je prazna.Ako imaš letove koji kreću za manje od 24h oni neće biti prikazani ovdje.\n");
            return;
        }
        
        var chosenFlight = Flight.SearchById(availableFlights);
        if (chosenFlight == null)
        {
            Console.WriteLine("\nOdustao si od unošenja id-a leta pa si odustao od otkazivanje leta.\n");
            return;
        }

        if (!Helper.ConfirmationMessage("otkazati let"))
        {
            Console.WriteLine("\nIpak odustaješ od otkazivanja leta.\n");
            return;
        }
        
        var chosenCategory=this._usrFlightDict[chosenFlight];
        this._usrFlightDict.Remove(chosenFlight);
        
        chosenFlight.IncrementCategoryCapacity(chosenCategory);
        
        this.UpdateTime = DateTime.Now;
        
        Console.WriteLine($"\nUspješno otkazan let {chosenFlight.Name} povezan s avionom {chosenFlight.Airplane.Name} u trenutku: {this.UpdateTime:yyyy-MM-dd HH:mm}." +
                          $"Odabrana kategorija: {chosenCategory}");        
    }
    private static List<Flight> AvailableFlightsForCancelation(List<Flight> userFlightList)
    {
        return userFlightList.FindAll(flight => (flight.DepartureDate - DateTime.Now).TotalHours > 24);
    }

    private void SearchFlightMenu()
    { 
        while(true){
            
            var flightList = _usrFlightDict.Keys.ToList();
                if (Flight.IsFlightListEmpty(flightList))
                {
                    Helper.MessagePrintAndSleep("\nLista letova je prazna.Ne možeš pretraživati.Povratak na izbornik za letove nakon pritiska tipke.");
                    return;
                }
                Console.Clear();
                Console.WriteLine("\n----------------------");
                Console.WriteLine("1 - Pretraživanje rezerviranih letova po id-u\n");
                Console.WriteLine("2 - Pretraživanje letova po nazivu\n");
                Console.WriteLine("0 - Povratak na izbornik za prijavljene korisnike.");
                Console.WriteLine("----------------------\n");
                Console.Write("\nUnos :");
                var input=Console.ReadKey().KeyChar;
                    switch (input)
                    {
                        case '0':
                            Helper.MessagePrintAndSleep("\nUspješan odabir.Povratak na izbornik za letove.\n");
                            Program.FlightMenu();
                            break;
                        case '1':
                            Helper.MessagePrintAndSleep("\nUspješan odabir.Pretraživanje po id-u.");
                            var searchedFlightById = Flight.SearchById(flightList);
                            if (searchedFlightById == null)
                            {
                                Helper.WaitingUser();
                                break;
                            }
                            Helper.MessagePrintAndSleep("\nUspješan pronalazak leta.");
                            searchedFlightById.OutputForOneFlight(false);
                            Helper.WaitingUser();
                            break;
                        case '2':
                            Helper.MessagePrintAndSleep("\nUspješan odabir.Pretraživanje po nazivu.\n");
                            var searchedAirplaneByName = Flight.SearchByName(flightList);
                            if (searchedAirplaneByName == null)
                            {
                                Helper.WaitingUser();
                                break;
                            }
                            
                            Helper.MessagePrintAndSleep("\nUspješan pronalazak leta.");
                            searchedAirplaneByName.OutputForOneFlight(false);
                            Helper.WaitingUser();
                            break;
                        default:
                            Helper.MessagePrintAndSleep("\nUnos nije ponuđenima.Unesi ponovno.\n");
                            break;
                    }
        }           
    }

    public static void RemoveFlightsWithPlaneId(int planeId)
    {
        foreach (var passenger in _passengerList)
        {
            var flightsToDelete=passenger._usrFlightDict.Keys.Where(flight => flight.Airplane.Id == planeId).ToList();
            foreach (var flight in flightsToDelete)
            {
                passenger._usrFlightDict.Remove(flight);
            }
        }
    }
}


