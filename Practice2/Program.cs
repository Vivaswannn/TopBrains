// Question No : 1 / 1
// Task:
// Write a C# program that reads a temperature in Celsius from the user, converts it to Fahrenheit, and then prints both the original Celsius temperature and the converted Fahrenheit temperature.
// Requirements:
// The program should read a temperature in Celsius from the user.
// The program should convert the temperature from Celsius to Fahrenheit using the below formula.
// Formula: Fahrenheit = (Celsius * 9 / 5) + 32
// The program should print both the original temperature in Celsius and the converted temperature in Fahrenheit.
// Write the solution within the Main function in the Program.cs file.
// Input Format:
// The first line of input is the floating point number.
// Output Format:
// The first line of output should be temperature in Celsius.
// The second line of output should be temperature in Fahrenheit.
// Refer Sample Output:
// Sample Input
// 32
// Sample Output
// Temperature in Celsius: 32
// Temperature in Fahrenheit: 89.6
// Commands to Run the Project:
// cd dotnetapp
// dotnet run
// dotnet build
// dotnet clean
// Note:
// The project will not be submitted if "Submit Project" is not done at least once.

using System;
namespace Practice2{
    class Program{
    static void Main(string[] args){
        Console.WriteLine("enter temperature:");
        float celcius=Convert.ToSingle(Console.ReadLine());
        float fahrenheit=(celcius * 9/5) + 32;
        Console.WriteLine(celcius);
        Console.WriteLine(fahrenheit);
    }
    }
}