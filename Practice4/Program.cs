// Question No : 1 / 1
// Task:
// Write a C# program that calculates and prints the sum of the first N natural numbers, where N is a positive integer entered by the user.
// Requirements:
// The program should prompt the user to enter a positive integer N.
// Use a for loop to calculate the sum of the first N natural numbers.
// Print the calculated sum to the console.
// Write the solution within the Main function in the Program.cs file.

// Input Format:
// The first line of input should be an integer.
// Output Format:
// The first line of output should be the sum of the natural numbers of the entered integer.

// Refer Sample Output:
// Sample Input
// 4
// Sample Output
// Sum of the first 4 natural numbers: 10

// Commands to Run the Project:
// cd dotnetapp
// dotnet run
// dotnet build
// dotnet clean

// Note:
// The project will not be submitted if "Submit Project" is not done at least once.

using System;
namespace Practice4{
    class Program{
        static void Main(string[] args){
            Console.WriteLine("Enter a positive integer N:");
            int N=Convert.ToInt32(Console.ReadLine());
            int sum=0;
            for(int i=1;i<=N;i++){
                sum+=i;
            }
            Console.WriteLine($"Sum of the first {N} natural numbers: {sum}");
        }
    }
}