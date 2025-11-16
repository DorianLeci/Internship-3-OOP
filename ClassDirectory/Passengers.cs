namespace Internship_3_OOP.ClassDirectory;

public class Passenger
{
    public int Id { get; }
    public string Name { get; }
    public string Surname { get; }
    public string Email { get; }
    public string Password { get; }
    public DateOnly BirthDate { get; }
    
    public string Gender { get; }

    private static List<Passenger> _passengerList = new List<Passenger>();
    public Passenger(int id, string name, string surname, string email,string password,DateOnly birthDate,string gender)
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
    
}