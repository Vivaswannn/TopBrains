using System;

public static class Program
{
    public static void Main(string[] args)
    {
        Run();
    }

    public static void Run()
    {
        // int[] arr = new int[] { 50, 20, 10, 40, 30 };

        // Console.WriteLine($"Array Index of value 3: {Array.IndexOf(arr, 3)}");
        // Console.WriteLine($"Array value at Index 2: {arr.GetValue(2)}");
        // Console.WriteLine($"Array is Fixed Size {arr.IsFixedSize}");
        // Console.WriteLine($"Array is Read Only {arr.IsReadOnly}");
        // Console.WriteLine($"Array's Rank {arr.Rank}");
        // Console.WriteLine("\n\n\n");

        Employee employee1 = new Employee()
        {
            Id = 3,
            Name = "Alice"
        };
        Employee employee2 = new Employee()
        {
            Id = 1,
            Name = "Bob"
        };
        Employee employee3 = new Employee()
        {
            Id = 4,
            Name = "Charlie"
        };
        Employee employee4 = new Employee()
        {
            Id = 5,
            Name = "David"
        };
        Employee employee5 = new Employee()
        {
            Id = 2,
            Name = "Eve"
        };
        Employee[] employeeList = new Employee[5];
        employeeList[0]=employee1;
        employeeList[1]=employee2;
        employeeList[2]=employee3;
        employeeList[3]=employee4;
        employeeList[4]=employee5;

        Console.WriteLine("Employee List Before Sorting:");
        foreach (var emp in employeeList)
        {
            Console.WriteLine(emp);
        }

        Array.Sort(employeeList);

        Console.WriteLine("Employee List After Sorting:");
        foreach (var emp in employeeList)
        {
            Console.WriteLine(emp);
        }

        Console.WriteLine("\n\n\n");

        // Console.WriteLine("Before Sorting");
        // for (int i = 0; i < arr.Length; i++)
        // {
        //     Console.WriteLine(arr[i]);
        // }

        // Array.Sort(arr);
        // Console.WriteLine("\n\n\n");
        // Console.WriteLine("After Sorting");
        // Console.WriteLine("Before Reversing");
        // for (int i = 0; i < arr.Length; i++)
        // {
        //     Console.WriteLine(arr[i]);
        // }

        // Console.WriteLine("After Reversing");
        // Array.Reverse(arr);
        // for (int i = 0; i < arr.Length; i++)
        // {
        //     Console.WriteLine(arr[i]);
        // }

        Employee employee = new Employee()
        {
            Id = 2,
            Name = "John"
        };
        PassObject(employee);

        Employee returnedEmployee = ReturnObject();
        Console.WriteLine(returnedEmployee);

        PassArrayObject(employeeList);
    }

    public static void PassObject(Employee emp)
    {
        Console.WriteLine("Inside PassObject Method");
        Console.WriteLine(emp);
    }

    public static Employee ReturnObject()
    {
        Console.WriteLine("Inside ReturnObject Method");
        Employee emp = new Employee()
        {
            Id = 10,
            Name = "Returned Employee"
        };
        return emp;
    }

    public static void PassArrayObject(Employee[] empArray)
    {
        foreach (var emp in empArray)
        {
            Console.WriteLine(emp);
        }
    }
}


