using System;
// namespace AdvanceCsharpApp{
namespace CalculatorApp{
    //Declaration of delegate
    public delegate int CalculatorDelegate(int num1, int num2);

    class Program{
        public static void Main(string[] args){
            Calculator calculator = new Calculator();

            //Instantiating delegate with addition method
            CalculatorDelegate calcutAdd = new CalculatorDelegate(calculator.Add);
            CalculatorDelegate calcutSub = new CalculatorDelegate(calculator.Subtract);
            CalculatorDelegate calculMultiply = new CalculatorDelegate(calculator.Multiply);
            CalculatorDelegate calcutDivid = new CalculatorDelegate(calculator.Divide);
            CalculatorDelegate calcutMulticast;

            //calling the Delegate single cast delegate
            //int sum = calcutAdd(10, 20);
            //Console.WriteLine("Addition: " + sum);

            // int difference = calcutSub(30, 15);
            // Console.WriteLine("Subtraction: " + difference);

            // int product = calculMultiply(5, 4);
            // Console.WriteLine("Multiplication: " + product);

            // int quotient = calcutDivid(40, 8);
            // Console.WriteLine("Division: " + quotient);


            calcutMulticast = calcutAdd;
            calcutMulticast += calcutSub;
            calcutMulticast += calculMultiply;
            calcutMulticast += calcutDivid;

            Console.WriteLine("called Multicast Delegate {0}:", calcutMulticast(20, 10));

            //PartialEmployee part = new PartialEmployee();
            //part.FirstName = "John";
            //part.DisplayFullName();
            //part.DisplayEmployeeData();
            //part.PartialMethod();

        }
    }
}