using System;
using MyRetailLogic;

class Program
{
    static void Main()
    {
        int num1, num2, numResult;

        // Taking Input
        Console.Write("Enter First Number: ");
        num1 = Int32.Parse(Console.ReadLine());

        Console.Write("Enter Second Number: ");
        num2 = Int32.Parse(Console.ReadLine());

        numResult = num1 + num2;

        // Call method from another file
        RetailLogic obj = new RetailLogic();
        int disc = obj.CalcDiscount(numResult);

        // Print
        Console.WriteLine("The Sum of {0} and {1} is {2}", num1, num2, numResult);
        Console.WriteLine("LPU Shopee");
        Console.WriteLine("Price of Product 1 {0}", num1);
        Console.WriteLine("Price of Product 2 {0}", num2);
        Console.WriteLine("Total Price {0}", numResult);
        Console.WriteLine("Discount price of product {0}", disc);
        Console.WriteLine("Payable amount after discount :{0}", numResult - disc);
    }
}
