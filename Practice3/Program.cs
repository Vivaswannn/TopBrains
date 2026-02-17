using System;
using System.Collections;

public class Program
{
    private static ArrayList numbers = new ArrayList();

    public static void Main()
    {
        while (true)
        {
            string command = Console.ReadLine();

            if (command == "add")
            {
                AddNumber();
            }
            else if (command == "remove")
            {
                RemoveNumber();
            }
            else if (command == "display")
            {
                DisplayNumbers();
            }
            else if (command == "exit")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid command.");
            }
        }
    }

    private static void AddNumber()
    {
        string input = Console.ReadLine();
        int num;

        if (int.TryParse(input, out num))
        {
            numbers.Add(num);
            Console.WriteLine(num + " added to the number list.");
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }

    private static void RemoveNumber()
    {
        string input = Console.ReadLine();
        int num;

        if (int.TryParse(input, out num))
        {
            if (numbers.Contains(num))
            {
                numbers.Remove(num);
                Console.WriteLine(num + " removed from the number list.");
            }
            else
            {
                Console.WriteLine(num + " not found in the number list.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }

    private static void DisplayNumbers()
    {
        Console.WriteLine("Current numbers in the list:");
        foreach (int num in numbers)
        {
            Console.WriteLine(num);
        }
    }
}
