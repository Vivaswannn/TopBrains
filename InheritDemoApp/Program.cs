using System;

class Program
{
    public static void Main(string[] args)
    {
        IPayment payment;
        payment = new CreditCardPayment();
        payment.Refund(1000.0);
        payment.Pay(1000.0);
    }    //     Employee emp = new Employee;
    //     EmployeeId = 101;
    //     Name = "John Doe";
    //     Salary = "50000";
    // };
    // Console.WriteLine("Employee Details:");
    // Console.WriteLine($"ID: {emp.EmployeeId}, Name: {emp.Name}, Salary: {emp.Salary}");
    // System.Console.WriteLine($"Employee Object {emp}");
    
}