using System;

class Student
{
    // Properties
    public string Name { get; set; }
    public int Age { get; set; }
    public string Grade { get; set; }

    // Default constructor
    public Student()
    {
        Name = "Hellen Doe";
        Age = 21;
        Grade = "A";
    }

    // Parameterized constructor
    public Student(string name, int age, string grade)
    {
        this.Name = name;
        this.Age = age;
        this.Grade = grade;
    }
}

class Program
{
    public static void Main(string[] args)
    {
        // Default student
        Student student = new Student();
        Console.WriteLine("Default Student:");
        Console.WriteLine(student.Name);
        Console.WriteLine(student.Age);
        Console.WriteLine(student.Grade);

        // User input
        Console.WriteLine("Enter Name:");
        string name = Console.ReadLine();

        Console.WriteLine("Enter Age:");
        int age = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Enter Grade:");
        string grade = Console.ReadLine();

        // New student using parameterized constructor
        Student student1 = new Student(name, age, grade);
        Console.WriteLine("New Student:");
        Console.WriteLine(student1.Name);
        Console.WriteLine(student1.Age);
        Console.WriteLine(student1.Grade);

        Console.ReadKey();
    }
}
