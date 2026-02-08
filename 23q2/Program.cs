using System;

// ✅ FIX 1: Car class moved OUTSIDE Program class
class Car
{
    // Properties
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }

    // ✅ FIX 2: Constructor moved INSIDE Car class
    public Car(string make, string model, int year)
    {
        this.Make = make;
        this.Model = model;
        this.Year = year;
    }

    // ✅ FIX 3: Added parentheses () and moved method INSIDE Car class
    public void DisplayDetails()
    {
        Console.WriteLine("Car Details:");
        Console.WriteLine("Make: " + Make);
        Console.WriteLine("Model: " + Model);
        Console.WriteLine("Year: " + Year);
    }

    // ✅ FIX 4: Method correctly placed inside Car class
    public void DisplayAge()
    {
        int currentYear = 2024;
        int age = currentYear - Year;
        Console.WriteLine("Car Age: " + age + " years");
    }
}

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter car make:");
        string make = Console.ReadLine();

        Console.WriteLine("Enter car model:");
        string model = Console.ReadLine();

        Console.WriteLine("Enter car year:");

        // ✅ FIX 5: Corrected Convert spelling + variable name
        int year = Convert.ToInt32(Console.ReadLine());

        // ✅ FIX 6: Passing correct variable (year, not age)
        Car car1 = new Car(make, model, year);

        car1.DisplayDetails();
        car1.DisplayAge();

        Console.ReadKey();
    }
}
