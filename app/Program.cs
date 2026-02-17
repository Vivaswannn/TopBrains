using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

[Serializable]
public class Applicant
{
    public string ApplicantID { get; set; }
    public string Name { get; set; }
    public string CurrentLocation { get; set; }
    public string PreferredLocation { get; set; }
    public string CoreCompetency { get; set; }
    public int PassingYear { get; set; }
}

class Program
{
    static List<Applicant> applicants = new List<Applicant>();
    static string filePath = "applicants.json";

    static void Main(string[] args)
    {
        LoadFromFile();

        while (true)
        {
            Console.WriteLine("\n--- CampusHire Applicant Management ---");
            Console.WriteLine("1. Add Applicant");
            Console.WriteLine("2. Display All Applicants");
            Console.WriteLine("3. Search Applicant by ID");
            Console.WriteLine("4. Update Applicant");
            Console.WriteLine("5. Delete Applicant");
            Console.WriteLine("6. Exit");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid choice.");
                continue;
            }

            switch (choice)
            {
                case 1: AddApplicant(); break;
                case 2: DisplayAll(); break;
                case 3: SearchApplicant(); break;
                case 4: UpdateApplicant(); break;
                case 5: DeleteApplicant(); break;
                case 6:
                    SaveToFile();
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    static void AddApplicant()
    {
        Applicant applicant = new Applicant();

        Console.Write("Applicant ID: ");
        applicant.ApplicantID = Console.ReadLine();

        Console.Write("Name: ");
        applicant.Name = Console.ReadLine();

        Console.Write("Current Location: ");
        applicant.CurrentLocation = Console.ReadLine();

        Console.Write("Preferred Location: ");
        applicant.PreferredLocation = Console.ReadLine();

        Console.Write("Core Competency: ");
        applicant.CoreCompetency = Console.ReadLine();

        Console.Write("Passing Year: ");
        if (!int.TryParse(Console.ReadLine(), out int year))
        {
            Console.WriteLine("Invalid year.");
            return;
        }

        applicant.PassingYear = year;

        if (!ValidateApplicant(applicant))
            return;

        applicants.Add(applicant);
        SaveToFile();
        Console.WriteLine("Applicant Added Successfully.");
    }

    static void DisplayAll()
    {
        if (applicants.Count == 0)
        {
            Console.WriteLine("No applicants available.");
            return;
        }

        foreach (var a in applicants)
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("ID: " + a.ApplicantID);
            Console.WriteLine("Name: " + a.Name);
            Console.WriteLine("Current Location: " + a.CurrentLocation);
            Console.WriteLine("Preferred Location: " + a.PreferredLocation);
            Console.WriteLine("Core Competency: " + a.CoreCompetency);
            Console.WriteLine("Passing Year: " + a.PassingYear);
        }
    }

    static void SearchApplicant()
    {
        Console.Write("Enter Applicant ID: ");
        string id = Console.ReadLine();

        var applicant = applicants.Find(a =>
            a.ApplicantID.Equals(id, StringComparison.OrdinalIgnoreCase));

        if (applicant != null)
        {
            Console.WriteLine("Applicant Found:");
            Console.WriteLine($"{applicant.ApplicantID} | {applicant.Name} | {applicant.CoreCompetency}");
        }
        else
        {
            Console.WriteLine("Applicant Not Found.");
        }
    }

    static void UpdateApplicant()
    {
        Console.Write("Enter Applicant ID to update: ");
        string id = Console.ReadLine();

        var applicant = applicants.Find(a =>
            a.ApplicantID.Equals(id, StringComparison.OrdinalIgnoreCase));

        if (applicant == null)
        {
            Console.WriteLine("Applicant Not Found.");
            return;
        }

        Console.Write("New Name: ");
        applicant.Name = Console.ReadLine();

        Console.Write("New Preferred Location: ");
        applicant.PreferredLocation = Console.ReadLine();

        SaveToFile();
        Console.WriteLine("Applicant Updated Successfully.");
    }

    static void DeleteApplicant()
    {
        Console.Write("Enter Applicant ID to delete: ");
        string id = Console.ReadLine();

        var applicant = applicants.Find(a =>
            a.ApplicantID.Equals(id, StringComparison.OrdinalIgnoreCase));

        if (applicant != null)
        {
            applicants.Remove(applicant);
            SaveToFile();
            Console.WriteLine("Applicant Deleted Successfully.");
        }
        else
        {
            Console.WriteLine("Applicant Not Found.");
        }
    }

    static bool ValidateApplicant(Applicant a)
    {
        if (string.IsNullOrWhiteSpace(a.ApplicantID) ||
            string.IsNullOrWhiteSpace(a.Name) ||
            string.IsNullOrWhiteSpace(a.CurrentLocation) ||
            string.IsNullOrWhiteSpace(a.PreferredLocation) ||
            string.IsNullOrWhiteSpace(a.CoreCompetency))
        {
            Console.WriteLine("All fields are mandatory.");
            return false;
        }

        if (a.ApplicantID.Length != 8 || !a.ApplicantID.StartsWith("CH"))
        {
            Console.WriteLine("Invalid Applicant ID. Must start with CH and be 8 characters.");
            return false;
        }

        if (a.Name.Length < 4 || a.Name.Length > 15)
        {
            Console.WriteLine("Name must be between 4 and 15 characters.");
            return false;
        }

        if (a.PassingYear > DateTime.Now.Year)
        {
            Console.WriteLine("Passing Year cannot be greater than current year.");
            return false;
        }

        return true;
    }

    static void SaveToFile()
    {
        var json = JsonSerializer.Serialize(applicants);
        File.WriteAllText(filePath, json);
    }

    static void LoadFromFile()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);

            if (!string.IsNullOrWhiteSpace(json))
            {
                applicants = JsonSerializer.Deserialize<List<Applicant>>(json);
            }

            if (applicants == null)
                applicants = new List<Applicant>();
        }
    }
}
