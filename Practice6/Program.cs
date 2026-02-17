using System;
using System.Collections.Generic;

public class Program
{
    public static LinkedList<string> students = new LinkedList<string>();

    public static void Main(string[] args)
    {
        int choice;
        do
        {
            Console.WriteLine("\nLinkedList Operations:");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Display Students");
            Console.WriteLine("3. Update Student");
            Console.WriteLine("4. Delete Student by Name");
            Console.WriteLine("5. Clear List");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice (1-6): ");

            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        AddStudent();
                        break;
                    case 2:
                        DisplayStudents();
                        break;
                    case 3:
                        UpdateStudent();
                        break;
                    case 4:
                        DeleteStudent();
                        break;
                    case 5:
                        ClearList();
                        break;
                    case 6:
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                choice = 0;
            }

        } while (choice != 6);
    }

    public static void AddStudent()
    {
        Console.Write("Enter student name to add: ");
        string name = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(name))
        {
            students.AddLast(name);
            Console.WriteLine($"{name} added to the list.");
        }
        else
        {
            Console.WriteLine("Name cannot be empty.");
        }
    }

    public static void DisplayStudents()
    {
        if (students.Count == 0)
        {
            Console.WriteLine("No students in the list.");
            return;
        }

        Console.WriteLine("Students in the list:");
        foreach (var student in students)
        {
            Console.WriteLine(student);
        }
    }

    public static void UpdateStudent()
    {
        Console.Write("Enter the current student name to update: ");
        string currentName = Console.ReadLine();

        LinkedListNode<string> node = students.Find(currentName);
        if (node != null)
        {
            Console.Write("Enter the new student name: ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
            {
                node.Value = newName;
                Console.WriteLine($"{currentName} updated to {newName}.");
            }
            else
            {
                Console.WriteLine("New name cannot be empty.");
            }
        }
        else
        {
            Console.WriteLine($"{currentName} not found in the list.");
        }
    }

    public static void DeleteStudent()
    {
        Console.Write("Enter student name to delete: ");
        string name = Console.ReadLine();

        if (students.Remove(name))
        {
            Console.WriteLine($"{name} removed from the list.");
        }
        else
        {
            Console.WriteLine($"{name} not found in the list.");
        }
    }

    public static void ClearList()
    {
        students.Clear();
        Console.WriteLine("The list has been cleared.");
    }
}