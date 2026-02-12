using System;
class Program{
    static void Main(string[] args){
        // Employee emp=new Employee();
        // emp[0]="string 1";
        // emp[1]="string 2";
        // emp[2]="string 3";
        // emp[3]="string 4";
        // emp[4]="string 5";

        // for(int i=0;i<5;i++){
        //     Console.WriteLine(emp[i]);
        // }

        // Console.WriteLine("Index of 'string 3' is {0}:", emp["string 3"]);
        // Console.WriteLine("Value string 4 is at index:"+ emp["string 4"]);

        //.............................................................................................................

        // int num1=0;
        // int num2=0;
        // int result=0;

        // int[] arr = new int[5]{10,20,30,40,50};

        // try{
        //     Console.WriteLine("Enter first number:");
        //     num1= Convert.ToInt32(Console.ReadLine());
        //     Console.WriteLine("Enter second number:");
        //     num2= Convert.ToInt32(Console.ReadLine());
        //     result= num1 / num2;
        //     Console.WriteLine("Result is: "+ result);

        //     for(int i =0;i<=5;i++){
        //         Console.WriteLine(arr[i]);
        // }
        // catch (IndexOutOfRangeException ex){
        //     Console.WriteLine("Array index is out of range. Please provide a valid index.");
        // }
        // catch(DivideByZeroException ex){
        //     Console.WriteLine("Division by zero is not allowed. Please provide a non-zero divisor.");
        // }
        // catch(FormatException ex){
        //     Console.WriteLine("Invalid input format. Please enter numeric values only.");
        // }
        // catch(Exception ex){
        //     Console.WriteLine("An unexpected error occurred: "+ ex.Message.ToString());
        // }
        // finally{
        //     Console.WriteLine($"Division of {num1} by {num2} gives result {result}");
        // }
        
        //.............................................................................................................

        try{
            throw new MyException("RAJESH");
        }
        catch(Exception ex){
            Console.WriteLine("Caught in Main: "+ ex.Message.ToString());
        }
        Console.WriteLine("LAST LINE OF PROGRAM");
    }
}
