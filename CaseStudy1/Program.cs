using System;

class Program{

    public static void Main(string[] args){
        Calculator calculator=new Calculator();

        int sum=calculator.Add(10,20);
        Console.WriteLine("Addition is: " + sum);

        calculator.Subtract();

        int mult=calculator.Multiply(5,6);
        Console.WriteLine("Multiplication is: " + mult);

        calculator.Divide();
    }
}