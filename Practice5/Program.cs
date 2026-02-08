// Question No : 1 / 1
// Task:
// Write a C# program that prints the first N even numbers, where N is a positive integer entered by the user.
// Requirements:
// The program should use a for loop to generate and print the even numbers.
// Print each even number on a separate line.
// Write the solution within the Main function in the Program.cs file.

// Input Format:
// The first line of input is an integer.

// Output Format:
// The first line of output should be first entered digit of even numbers.
// The next lines of output should be the first N even numbers, where N is a positive integer entered by the user.

// Refer Sample Output:
// Sample Input
// 3
// Sample Output
// First 3 even numbers:
// 2
// 4
// 6

// Commands to Run the Project:
// cd dotnetapp
// dotnet run
// dotnet build
// dotnet clean

// Note:
// The project will not be submitted if "Submit Project" is not done at least once.

using System;
namespace Practice5{
    class Program{
        static void Main(string[] args){
            Console.WriteLine("Enter a positive integer N:");
            int N=Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"First {N} even numbers:");
            for(int i=1;i<=N;i++){
                Console.WriteLine(i*2);
            }
        }
    }
}
