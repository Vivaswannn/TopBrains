using System;
using System.IO;

// class FileDemo
// {
//     private string path = @"E:\Data\Text.txt";

//     public void WriteToFile(string text)
//     {
//         FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
//         StreamWriter sw = new StreamWriter(fs);
//         sw.WriteLine(text);
//         sw.Close();
//         fs.Close();
//     }

//     public string ReadFromFile()
//     {
//         if (!File.Exists(path))
//         {
//             return "File does not exist.";
//         }

//         FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
//         StreamReader sr = new StreamReader(fs);
//         string content = sr.ReadToEnd();
//         sr.Close();
//         fs.Close();
//         return content;
//     }

//     public void AppendToFile(string text)
//     {
//         FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write);
//         StreamWriter sw = new StreamWriter(fs);
//         sw.WriteLine(text);
//         sw.Close();
//         fs.Close();
//     }

//     public void DeleteFile()
//     {
//         if (File.Exists(path))
//         {
//             File.Delete(path);
//         }
//     }

//     public bool FileExists()
//     {
//         return File.Exists(path);
//     }
// }




class FileDemo
{
    private string path = @"E:\Data\Text.txt";

    private void EnsureDirectoryExists()
    {
        string directoryPath = Path.GetDirectoryName(path);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
    }

    public void CreateFile()
    {
        EnsureDirectoryExists();
        
        if (File.Exists(path))
        {
            Console.WriteLine("File already exists.");
            return;
        }

        FileStream fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write);
        fs.Close();
        Console.WriteLine("File created successfully.");
    }

    public void WriteToFile(string text)
    {
        EnsureDirectoryExists();
        
        FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
        StreamWriter sw = new StreamWriter(fs);
        sw.WriteLine(text);
        sw.Close();
        fs.Close();
    }

    public string ReadFromFile()
    {
        if (!File.Exists(path))
            return "File does not exist.";

        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        StreamReader sr = new StreamReader(fs);
        string content = sr.ReadToEnd();
        sr.Close();
        fs.Close();
        return content;
    }

    public void AppendToFile(string text)
    {
        EnsureDirectoryExists();
        
        FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write);
        StreamWriter sw = new StreamWriter(fs);
        sw.WriteLine(text);
        sw.Close();
        fs.Close();
    }

    public void DeleteFile()
    {
        if (File.Exists(path))
            File.Delete(path);
    }

    public bool FileExists()
    {
        return File.Exists(path);
    }

    public void ShowFileInfo()
    {
        if (!File.Exists(path))
        {
            Console.WriteLine("File does not exist.");
            return;
        }

        FileInfo fi = new FileInfo(path);
        Console.WriteLine("\n--- File Info ---");
        Console.WriteLine("Name: " + fi.Name);
        Console.WriteLine("Path: " + fi.FullName);
        Console.WriteLine("Size: " + fi.Length + " bytes");
        Console.WriteLine("Created: " + fi.CreationTime);
        Console.WriteLine("Last Modified: " + fi.LastWriteTime);
    }
}