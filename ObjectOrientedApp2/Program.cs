using System;
class Program{
    public static void Main(string[] args){
        Employee employee=new Employee();

        // employee.AcceptDetails();
        // employee.DisplayDetails();

        // employee.Eno=101;
        // employee.Ename=Viv;
        // employee.BasicSalary=1002392;
        employee.AcceptDetails();
        employee.DisplayDetails();
        Console.ReadKey();
    }
}