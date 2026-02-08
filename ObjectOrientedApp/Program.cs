using System;
class Program{
    public static void Main(string[] args){

        //create the object of Employee class
        Employee employee= new Employee();

        // employee.AcceptDetails(); //binding the empoyee object to AcceptDetails method
        // employee.DisplayDetails();

        Employee employee1= new Employee();
        employee1.EmpId=101;
        employee1.Name="John Doe";
        employee1.Department="IT";
        employee1.Salary=75000.0f;
        employee1.status=true;
        employee1.DisplayDetails();

        Console.ReadKey();

    }
}