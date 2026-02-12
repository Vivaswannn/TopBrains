using System;
public class Employee{
    protected int Eid, Eage;
    protected string Ename, Eadress;
    protected float Salary;

    public virtual void GetEmployeeData(){
        Console.WriteLine("-----Enter Employee Details-----");
        Console.WriteLine("Enter Employee ID:");
        Eid = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter Employee Name:");
        Ename = Console.ReadLine();
        Console.WriteLine("Enter Employee Age:");
        Eage = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter Employee Address:");
        Eadress = Console.ReadLine();
        Console.WriteLine("Enter Employee Salary:");
        Salary = float.Parse(Console.ReadLine());
    }

    public virtual void DisplayEmployeeData(){
        Console.WriteLine("-----Employee Details-----");
        Console.WriteLine("Employee ID: " + Eid);
        Console.WriteLine("Employee Name: " + Ename);
        Console.WriteLine("Employee Age: " + Eage);
        Console.WriteLine("Employee Address: " + Eadress);
    }

    public virtual void CalculateSalary(){
        Console.WriteLine("Employee Salary: " + Salary);
    }
}