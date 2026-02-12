using System;
using System.Collections;

class Program
{
    public static void Main(string[] args)
    {
        ArrayList names = new ArrayList();

        while (true)
        {
            string name = Console.ReadLine();

            if (name.Equals("stop", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            bool exists = false;

            for (int i = 0; i < names.Count; i++)
            {
                string existingName = names[i].ToString();

                if (existingName.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    exists = true;
                    break;
                }
            }

            if (exists)
            {
                Console.WriteLine(name + " is already in the collection.");
            }
            else
            {
                names.Add(name);
                Console.WriteLine(name + " added to the collection.");
            }
        }

        Console.WriteLine("Unique student names entered:");
        for (int i = 0; i < names.Count; i++)
        {
            Console.WriteLine(names[i]);
        }

        Console.ReadKey();
    }
}
