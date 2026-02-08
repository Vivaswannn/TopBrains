using System;
using System.IO;

class Program
{
    private const string filename = "attendance.txt";

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\n--- Student Attendance Logger ---");
            Console.WriteLine("1. Add Attendance Record");
            Console.WriteLine("2. View Attendance Log");
            Console.WriteLine("3. Exit");
            Console.Write("Enter choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddAttendance();
                    break;

                case "2":
                    ViewAttendance();
                    break;

                case "3":
                    Console.WriteLine("Exiting application.");
                    return;

                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }
    static void AddAttendance()
    {
        try
        {
            Console.Write("Enter Student ID: ");
            string id = Console.ReadLine();

            Console.Write("Enter Student Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Status (Present/Absent): ");
            string status = Console.ReadLine();

            string record = $"{DateTime.Now:dd/MM/yyyy} | {id} | {name} | {status}";

            using (StreamWriter writer = new StreamWriter(filename, true))
            {
                writer.WriteLine(record);
            }

            Console.WriteLine("Attendance recorded.");
        }
        catch (IOException)
        {
            Console.WriteLine("Error while writing to file.");
        }
    }
    static void ViewAttendance()
    {
        try
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("No attendance records found.");
                return;
            }

            using (StreamReader reader = new StreamReader(filename))
            {
                string content = reader.ReadToEnd();

                if (string.IsNullOrWhiteSpace(content))
                {
                    Console.WriteLine("No attendance records found.");
                }
                else
                {
                    Console.WriteLine("\n--- Attendance Log ---");
                    Console.WriteLine(content);
                }
            }
        }
        catch (IOException)
        {
            Console.WriteLine("Error while reading file.");
        }
    }
}
