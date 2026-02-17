using System;
//public sealed class Manager: Employee{

public class Manager: Employee{
    double Bonus, CA;
    public override void GetEmployeeData(){
        Console.WriteLine("Enter Manager Details: ");
        Console.WriteLine("Enter the ID");
        Eid = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter the Name");
        Ename = Console.ReadLine();
        Console.WriteLine("Enter Manager Bonus:");
        Bonus = double.Parse(Console.ReadLine());
        Console.WriteLine("Enter Manager CA:");
        CA = double.Parse(Console.ReadLine());
    }

    public override void DisplayEmployeeData(){
        Console.WriteLine("-----Manager Details-----");
        Console.WriteLine("Manager ID: " + Eid);
        Console.WriteLine("Manager Name: " + Ename);
        Console.WriteLine("Manager Bonus: " + Bonus);
        Console.WriteLine("Manager CA: " + CA);
    }

    public override void CalculateSalary(){
        // double TotalSalary = Salary + Bonus + CA;
        Console.WriteLine("Manager Total Salary: " + Salary);
    }
}