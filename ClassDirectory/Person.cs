namespace Internship_3_OOP.ClassDirectory;

public abstract class Person
{
    protected int Id { get; }
    protected string Name { get; }
    protected string Surname { get; }
    protected int BirthYear { get; }
    protected char Gender { get; }
    public DateTime CreationTime { get; }
    public DateTime UpdateTime { get;set; }

    protected Person(string name, string surname,int birthYear, char gender)
    {
        this.Id = Helper.IdGenerator();
        this.Name = name;
        this.Surname = surname;
        this.BirthYear = birthYear;
        this.Gender = gender;
        this.CreationTime = DateTime.Now;
        this.UpdateTime = DateTime.Now;
    }
}