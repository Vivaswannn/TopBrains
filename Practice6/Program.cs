// Question No : 1 / 1
// Task:
// Write a C# program that reads a student’s exam score from the user and determines their grade based on the following grading scale:
// Score ≥ 90 : Grade A
// 80 ≥ Score < 90 : Grade B
// 70 ≥ Score < 80 : Grade C
// 60 ≥ Score < 70 : Grade D
// Score < 60 : Grade F
// The program should use nested if statements to evaluate the input and print the corresponding grade.
// Write the solution within the Main function in the Program.cs file.

// Input Format:
// The first line of input should be an integer.
// Output Format:
// The first line of output should be the Grade based on the score.

// Refer Sample Output:
// Sample Input 1
// 90
// Sample Output 1
// Grade: A

// Commands to Run the Project:
// cd dotnetapp
// dotnet run
// dotnet build
// dotnet clean

// Note:
// The project will not be submitted if "Submit Project" is not done at least once.

using System;
namespace Practice6{
    class Program{
        static void Main(string[] args){
            Console.WriteLine("Enter the marks:");
            int score = Convert.ToInt32(Console.ReadLine());
            string grade;

            if(score >= 90){
                grade = "A";
            }
            else if(score >= 80){
                grade = "B";
            }
            else if(score >= 70){
                grade = "C";
            }
            else if(score >= 60){
                grade = "D";
            }
            else{
                grade = "F";
            }

            Console.WriteLine("Grade: " + grade);
        }
    }
}
x