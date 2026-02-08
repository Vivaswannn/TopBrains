using System;

class Program
{
    static void Main()
    {
        int num1 = 100;
        int num2 = 200;
        int numResult;

        // Taking Input Below
        Console.Write("Enter First Number: ");
        num1 = Int32.Parse(Console.ReadLine());

        Console.Write("Enter Second Number: ");
        num2 = Int32.Parse(Console.ReadLine());

        // Business Logic
        int disc = (num1 + num2) * 10/100;   // example calculation
        numResult = num1 + num2;

        // Print
	Console.Write("The Sum of {0} and {1} is {2}",num1,num2,numResult);
	Console.WriteLine("LPU Shopee");
	Console.WriteLine("Price of Product 1 {0}",num1);
	Console.WriteLine("Price of Product 2 {1}",num2);
	Console.WriteLine("Total Price {0}",(num1+num2));

        Console.WriteLine("Discount price of product {0}",disc);
		Console.WriteLine("Payable amount after discount :{0}",numResult);

        //Console.ReadLine(); // wait for input before closing
    }
}
