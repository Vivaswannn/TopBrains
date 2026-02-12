using System;

// class Program
// {
//     public static void Main(string[] args)
//     {
//         FileDemo fd = new FileDemo();

//         Console.WriteLine("Enter your name:");
//         string name = Console.ReadLine();
//         fd.WriteToFile(name);

//         Console.WriteLine("\nFile content:");
//         Console.WriteLine(fd.ReadFromFile());

//         Console.WriteLine("\nDo you want to append more text? (yes/no)");
//         string choice = Console.ReadLine();

//         if (choice.ToLower() == "yes")
//         {
//             Console.WriteLine("Enter text to append:");
//             string extra = Console.ReadLine();
//             fd.AppendToFile(extra);
//         }

//         Console.WriteLine("\nUpdated File Content:");
//         Console.WriteLine(fd.ReadFromFile());

//         Console.WriteLine("\nDo you want to delete the file? (yes/no)");
//         string del = Console.ReadLine();

//         if (del.ToLower() == "yes")
//         {
//             fd.DeleteFile();
//             Console.WriteLine("File deleted.");
//         }
//     }
// }


// FileDemoApp/Program.cs

// class Program
// {
//     public static void Main(string[] args)
//     {
//         FileDemo fd = new FileDemo();
//         int choice;

//         do
//         {
//             Console.WriteLine("\n---- FILE MENU ----");
//             Console.WriteLine("1. Create File");
//             Console.WriteLine("2. Write to File");
//             Console.WriteLine("3. Read File");
//             Console.WriteLine("4. Append to File");
//             Console.WriteLine("5. Delete File");
//             Console.WriteLine("6. Check File Exists");
//             Console.WriteLine("7. File Info");
//             Console.WriteLine("8. Exit");

//             Console.Write("Enter your choice: ");
//             choice = int.Parse(Console.ReadLine());

//             switch (choice)
//             {
//                 case 1:
//                     fd.CreateFile();
//                     break;

//                 case 2:
//                     Console.Write("Enter text to write: ");
//                     fd.WriteToFile(Console.ReadLine());
//                     break;

//                 case 3:
//                     Console.WriteLine("\nFile Content:");
//                     Console.WriteLine(fd.ReadFromFile());
//                     break;

//                 case 4:
//                     Console.Write("Enter text to append: ");
//                     fd.AppendToFile(Console.ReadLine());
//                     break;

//                 case 5:
//                     fd.DeleteFile();
//                     Console.WriteLine("File deleted if existed.");
//                     break;

//                 case 6:
//                     Console.WriteLine("File Exists: " + fd.FileExists());
//                     break;

//                 case 7:
//                     fd.ShowFileInfo();
//                     break;

//                 case 8:
//                     Console.WriteLine("Exiting...");
//                     break;

//                 default:
//                     Console.WriteLine("Invalid choice!");
//                     break;
//             }

//         } while (choice != 8);
//     }
// }




//BinaryFileDemoApp/Program.cs

class Program
{
    public static void Main(string[] args)
    {
        FileDemo textFile = new FileDemo();
        BinaryFileDemo binFile = new BinaryFileDemo();

        int choice;
        do
        {
            Console.WriteLine("\n---- MAIN MENU ----");
            Console.WriteLine("1. Text File Operations");
            Console.WriteLine("2. Binary File Operations");
            Console.WriteLine("3. Exit");
            Console.Write("Enter choice: ");
            choice = int.Parse(Console.ReadLine());

            if (choice == 1)
                TextMenu(textFile);
            else if (choice == 2)
                BinaryMenu(binFile);

        } while (choice != 3);
    }

    static void TextMenu(FileDemo fd)
    {
        int ch;
        do
        {
            Console.WriteLine("\n-- TEXT FILE MENU --");
            Console.WriteLine("1. Create");
            Console.WriteLine("2. Write");
            Console.WriteLine("3. Read");
            Console.WriteLine("4. Append");
            Console.WriteLine("5. Delete");
            Console.WriteLine("6. Info");
            Console.WriteLine("7. Back");
            Console.Write("Enter choice: ");
            ch = int.Parse(Console.ReadLine());

            switch (ch)
            {
                case 1: fd.CreateFile(); break;
                case 2: Console.Write("Enter text: "); fd.WriteToFile(Console.ReadLine()); break;
                case 3: Console.WriteLine(fd.ReadFromFile()); break;
                case 4: Console.Write("Enter text: "); fd.AppendToFile(Console.ReadLine()); break;
                case 5: fd.DeleteFile(); break;
                case 6: fd.ShowFileInfo(); break;
            }

        } while (ch != 7);
    }

    static void BinaryMenu(BinaryFileDemo bf)
    {
        int ch;
        do
        {
            Console.WriteLine("\n-- BINARY FILE MENU --");
            Console.WriteLine("1. Create");
            Console.WriteLine("2. Write Data");
            Console.WriteLine("3. Read Data");
            Console.WriteLine("4. Delete");
            Console.WriteLine("5. Back");
            Console.Write("Enter choice: ");
            ch = int.Parse(Console.ReadLine());

            switch (ch)
            {
                case 1: bf.CreateBinaryFile(); break;
                case 2: bf.WriteBinary(); break;
                case 3: bf.ReadBinary(); break;
                case 4: bf.DeleteBinary(); break;
            }

        } while (ch != 5);
    }
}