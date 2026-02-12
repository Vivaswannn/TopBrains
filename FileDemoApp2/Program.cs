using System;
using System.IO;
using System.Diagnostics;

class Program
{
    public static void Main()
    {
        // Ensure the directory exists
        string directoryPath = @"Data";
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
            Console.WriteLine("Directory created: " + directoryPath);
        }

        // Create a file
        string filePath = @"Data\MyFile.txt";
        FileStream fs = File.Create(filePath);
        fs.Close();
        Console.WriteLine("File created successfully.");

        // Write to file using FileInfo
        FileInfo fi = new FileInfo(filePath);
        StreamWriter sw = fi.CreateText();
        sw.WriteLine("Hello, World!");
        sw.Close();
        Console.WriteLine("Text written to file.");

        // Read from file
        Console.WriteLine("\nReading file content:");
        string content = File.ReadAllText(filePath);
        Console.WriteLine(content);
        
        // Run FileInfoDemo to demonstrate Delete, CopyTo, and MoveTo
        FileInfoDemo.RunDemo();
    }
}