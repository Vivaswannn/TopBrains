using System;
class GeneralManager:Manager{ // Error cannot create child class for sealed Manager class
    public override void CalculateSalary(){
        // double TotalSalary = Salary + Bonus + CA + 5000; // General Manager gets an additional fixed amount
        Console.WriteLine("General Manager Total Salary: " + Salary);
    }
}