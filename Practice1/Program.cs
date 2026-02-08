// Question No : 1 / 1
// Task:
// Write a C# program that determines if a given year is a leap year.
// The program should prompt the user to enter a year, use control statements to determine if the year is a leap year, and print whether the year is a leap year or not.
// Requirements:
// The program should read a year from the user.
// The program should implement the logic to determine if the year is a leap year using control statements.
// A leap year is defined as:
// oDivisible by 4.
// oIf divisible by 100, it should also be divisible by 400.
// The program should print whether the year is a leap year or not.
// Write the solution within the Main function in the Program.cs file.
// Input Format:
// The first line of input should be a 4 digit integer.
// Output Format:
// Print a message indicating whether the entered year is a leap year or not.
// Refer Sample Output:
// Sample Input 1
// 1999
// Sample Output 1
// 1999 is not a leap year.
// Sample Input 2
// 2012
// Sample Output 2
// 2012 is a leap year.


// Commands to Run the Project:
// cd dotnetapp
// dotnet run
// dotnet build
// dotnet clean
// Note:
// The project will not be submitted if "Submit Project" is not done at least once.

using System;

namespace Practice1{
    class Program{
        static void Main(string[] args){
            Console.WriteLine("enter year:");
            int year=Convert.ToInt32(Console.ReadLine());
            bool isLeapYear=false;

            if(year % 4 == 0){
                if(year % 100 == 0){
                    if(year % 400 == 0){
                        isLeapYear=true;
                    }
                    else{
                        isLeapYear=false;
                    }
                }
                else{
                    isLeapYear=true;
                }
            }
            else{
                isLeapYear=false;
            }

            if(isLeapYear){
                Console.WriteLine(year + " is a leap year.");
            }
            else{
                Console.WriteLine(year + " is not a leap year.");
            }
        }
    }
}