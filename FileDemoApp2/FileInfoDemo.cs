using System;
using System.IO;
using System.Diagnostics;

class FileInfoDemo
{
    public static void RunDemo()
    {
        // Ensure the directory exists
        string directoryPath = @"Data";
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
            Console.WriteLine("Directory created: " + directoryPath);
        }

        // Create a file
        string path = @"Data\MyFile.txt";
        FileStream fs = File.Create(path);
        fs.Close();
        Console.WriteLine("File created successfully.");

        // Write to file using FileInfo
        FileInfo fi = new FileInfo(path);
        StreamWriter sw = fi.CreateText();
        sw.WriteLine("Hello, World!");
        Console.WriteLine("Text written to file.");
        sw.Close();

        // This method is used to delete a file.
        FileInfo fi2 = new FileInfo(path);
        if (fi2.Exists)
        {
            fi2.Delete();
            Console.WriteLine("File deleted successfully.");
        }

        // The CopyTo method is used to copy a file to a new location.
        string path2 = @"Data\MyFile2.txt";
        string path3 = @"Data\NewFile.txt";
        
        // Create source file first
        FileInfo fi3 = new FileInfo(path2);
        if (!fi3.Exists)
        {
            StreamWriter sw2 = fi3.CreateText();
            sw2.WriteLine("Source file content");
            sw2.Close();
        }
        
        FileInfo fi4 = new FileInfo(path2);
        if (fi4.Exists)
        {
            fi4.CopyTo(path3, true); // true to overwrite if exists
            Console.WriteLine("File copied successfully.");
        }

        //MoveTo method is used to move a file to a new location.
        string path4 = @"Data\MovedFile.txt";
        string path5 = @"Data\NewFile.txt";
        FileInfo fi5 = new FileInfo(path5);
        if (fi5.Exists)
        {
            fi5.MoveTo(path4);
            Console.WriteLine("File moved successfully.");
        }



        //This method creates a streamwriter text
        //appends text to the file represented by this
        // instance of the FileInfo class.
        FileInfo fi6 = new FileInfo(@"Data\NewFile1.txt");
        StreamWriter sw1 = fi6.AppendText();
        sw1.WriteLine("This");
        sw1.WriteLine("is extra");
        sw1.WriteLine("text");
        Console.WriteLine("Text appended successfully.");
        sw1.Close();


        //This mwthod creates a streamwriter with UTF8 encoding
        // encodiing that reads from an existing file.
        FileInfo fi7 = new FileInfo(@"Data2\NewFile1.txt");
        StreamWriter sw3 = fi7.CreateText();
        string s= "";
        while(s=sw3.ReadLine()!=null)
        {
            Console.WriteLine(s);
        }


        //to get file information
        FileInfo fi8 = new FileInfo(@"Data2\NewFile1.txt");
        Console.WriteLine("File Name: " + fi8.Name);
        Console.WriteLine("File creation time: " + fi8.CreationTime.ToLongTimeString());
        Console.WriteLine("File last access time: " + fi8.LastAccessTime.ToLongTimeString());
        Console.WriteLine("File length: " + fi8.Length.ToString() + " bytes");
        Console.WriteLine("File extension: " + fi8.Extension);
        Console.WriteLine("File exist status: " + fi8.Exists);
    }
}