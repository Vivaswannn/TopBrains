using System;
using System.IO;

class BinaryFileDemo
{
    private string path = @"E:\Data\Data.bin";

    private void EnsureDirectoryExists()
    {
        string directoryPath = Path.GetDirectoryName(path);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
    }

    public void CreateBinaryFile()
    {
        EnsureDirectoryExists();
        
        if (File.Exists(path))
        {
            Console.WriteLine("Binary file already exists.");
            return;
        }

        FileStream fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write);
        fs.Close();
        Console.WriteLine("Binary file created.");
    }

    public void WriteBinary()
    {
        EnsureDirectoryExists();
        
        FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write);
        BinaryWriter bw = new BinaryWriter(fs);

        Console.Write("Enter ID: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write("Enter Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Salary: ");
        double salary = double.Parse(Console.ReadLine());

        bw.Write(id);
        bw.Write(name);
        bw.Write(salary);

        bw.Close();
        fs.Close();
        Console.WriteLine("Data written to binary file.");
    }

    public void ReadBinary()
    {
        if (!File.Exists(path))
        {
            Console.WriteLine("Binary file does not exist.");
            return;
        }

        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        BinaryReader br = new BinaryReader(fs);

        Console.WriteLine("\n--- Binary File Data ---");
        while (fs.Position < fs.Length)
        {
            int id = br.ReadInt32();
            string name = br.ReadString();
            double salary = br.ReadDouble();

            Console.WriteLine($"ID: {id}, Name: {name}, Salary: {salary}");
        }

        br.Close();
        fs.Close();
    }

    public void DeleteBinary()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
            Console.WriteLine("Binary file deleted.");
        }
        else
        {
            Console.WriteLine("Binary file does not exist.");
        }
    }
}