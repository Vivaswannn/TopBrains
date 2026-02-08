// Question No : 1 / 1
// Task:
// Write a C# program that calculates the factorial of a non-negative integer entered by the user.
// The program should continuously prompt the user for input until they enter the letter q to quit.
// The program should perform the following:
// Requirements:
// Prompt the user to enter a non-negative integer.
// Calculate the factorial of the entered number.
// Display the result to the user.
// If the user enters a negative integer, display an error message.
// If the user enters any non-numeric input other than q, display an error message.
// Write the solution within the Main function in the Program.cs file.

// Input Format:
// The input should be an integer greater than 0.
// The input should be q to quit the program.

// Output Format:
// The output is to be printed as the factorial of the input number.

// Refer Sample Output:
// Sample Input 1
// 4
// 2
// q
// Sample Output 1
// The factorial of 4 is 24.
// The factorial of 2 is 2.

// Sample Input 2
// 8
// -2
// q
// Sample Output 2
// The factorial of 8 is 40320.
// Invalid input! Please enter a non-negative integer.

// Commands to Run the Project:
// cd dotnetapp
// dotnet run
// dotnet build
// dotnet clean

// Note:
// The project will not be submitted if "Submit Project" is not done at least once.


using System;
namespace Practice3{
    class Program{
        static void Main(string[] args){
            while(true){
                Console.WriteLine("Enter a non-negative integer (or 'q' to quit):");
                string input = Console.ReadLine();
                if(input.ToLower() == "q"){
                    break;
                }
                int num=Convert.ToInt32(input);
                    if(num < 0){
                        Console.WriteLine("Invalid input! Please enter a non-negative integer.");
                    }
                    else{
                        long factorial = 1;
                        for(int i = 1; i <= num; i++){
                            factorial *= i;
                        }
                        Console.WriteLine("The factorial of " + num + " is " + factorial + ".");
                    }
            }
        }
    }
}