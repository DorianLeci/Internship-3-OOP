namespace Internship_3_OOP.ClassDirectory;

public abstract class Person
{
    protected string Name { get; }
    protected string Surname { get; }
    protected int BirthYear { get; }
    protected char Gender { get; }
    protected DateTime CreationTime { get; }
    protected DateTime UpdateTime { get; }

    protected Person(string name, string surname,int birthYear, char gender)
    {
        this.Name = name;
        this.Surname = surname;
        this.BirthYear = birthYear;
        this.Gender = gender;
        this.CreationTime = DateTime.Now;
        this.UpdateTime = DateTime.Now;
    }
}