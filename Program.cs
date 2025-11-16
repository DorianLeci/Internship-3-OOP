// See https://aka.ms/new-console-template for more information
namespace Internship_3_OOP;

class Program
{
    static void Main(string[] args)
    {}
        static void MainMenu()
    {
        while (true)
        {
            Console.WriteLine("----------------------");
            Console.WriteLine("1 - Putnici\n");
            Console.WriteLine("2 - Letovi\n");
            Console.WriteLine("3 - Avioni\n");
            Console.WriteLine("4 - Posada\n");
            Console.WriteLine("5 - Izlaz iz programa");
            Console.WriteLine("----------------------\n");
            if (int.TryParse(Console.ReadLine(), out int inputMainMenu))
            {
                switch (inputMainMenu)
                {
                    case 5:
                        Console.WriteLine("Uspješan odabir.Izlaz iz aplikacije\n");
                        Environment.Exit(0);
                        break;
                    case 1:
                        Console.WriteLine("Uspješan odabir izbornika za putnike.\n");
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
