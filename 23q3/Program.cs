using System;

class Person{
    private string name;
    private int age;
    private string address;

    //properties
    public string Name{
        get { return name; }
        set { name = value; }
    }
    public int Age{
        get { return age; }
        set { age = value; }
    }
    public string Address{
        get { return address; }
        set { address = value; }
    }

}
class Program{
    public static void Main(string[] args){
        Person person=new Person();

        Console.WriteLine("Enter Name:");
        person.Name=Console.ReadLine();
        Console.WriteLine("Enter Age:");
        person.Age=Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter Address:");
        person.Address=Console.ReadLine();
        Console.WriteLine("Person Details:");
        Console.WriteLine("Name: " + person.Name);
        Console.WriteLine("Age: " + person.Age);
        Console.WriteLine("Address: " + person.Address);
        Console.ReadKey();
    }
}