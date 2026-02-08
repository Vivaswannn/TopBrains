using System;

class Program
{
    int number1, number2, result;
    
    public static void Main(string[] args)
    {
        //write a program to print number from 1 to 10 using while loop
        // int i = 1;
        // while (i <= 10)
        // {
        //     Console.WriteLine(i);
        //     i++;
        // }

        // for(int i=10;i>=1;i--){
        //     Console.WriteLine(i);
        // }

        int[] numbers = {1,2,3,4,5,6,7,8,90,100};
        // foreach(int i in numbers){
        //     Console.WriteLine(i);
        // }

        for(int i=0;i<numbers.Length;i++){
            Console.WriteLine($"Index at {i} : {numbers[i]}");
        }
        // Program calc = new Program();
        // int choice;
        // Console.WriteLine("1. Add");
        // Console.WriteLine("2. Subtract");
        // Console.WriteLine("3. Multiply");
        // Console.WriteLine("4. Divide");
        // Console.WriteLine("5. Remainder");
        // Console.WriteLine("Enter your choice:");
        // choice = Convert.ToInt32(Console.ReadLine());
        // switch(choice){
        //     case 1: calc.Add();
        //     break;
        //     case 2: calc.Subtract();
        //     break;
        //     case 3: calc.Multiply();
        //     break;
        //     case 4: calc.Divide();
        //     break;
        //     case 5: calc.Remainder();
        //     break;
        //     default: Console.WriteLine("Invalid choice");
        //     break;
        // }

    }
    
    public void Add()
    {
        Console.WriteLine("enter first number:");
        number1 = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("enter second number:");
        number2 = Convert.ToInt32(Console.ReadLine());

        result = number1 + number2;
        Console.WriteLine($"The sum of {number1} and {number2} is {result}");
    }
    
    public void Subtract()
    {
        Console.WriteLine("enter first number:");
        number1 = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("enter second number:");
        number2 = Convert.ToInt32(Console.ReadLine());
        result = number1 - number2;
        Console.WriteLine($"The difference of {number1} and {number2} is {result}");
    }
    public void Multiply()
    {
        Console.WriteLine("enter first number:");
        number1 = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("enter second number:");
        number2 = Convert.ToInt32(Console.ReadLine());
        result = number1 * number2;
        Console.WriteLine($"The product of {number1} and {number2} is {result}");
    }
    public void Divide(){
        Console.WriteLine("enter first number:");
        number1= Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("enter second number:");
        number2= Convert.ToInt32(Console.ReadLine());
        result= number1 / number2;
        Console.WriteLine($"The quotient of {number1} and {number2} is {result}");

    }
    public void Remainder(){
        Console.WriteLine("enter first number:");
        number1= Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("enter second number:");
        number2= Convert.ToInt32(Console.ReadLine());
        result= number1 % number2;
        Console.WriteLine($"The remainder of {number1} and {number2} is {result}");
    }

    
}