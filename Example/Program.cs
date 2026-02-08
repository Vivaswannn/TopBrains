using System;

namespace Example
{
    public delegate void Print(int val, string str);
    public delegate void Print1(int val);
    public delegate int AddDel(int val);
    

    class Program
    {
        AddDel add = delegate(int val)
        {
            return val + 10;
        };
        


        public static void PrintHelperMethod(Print1 printDel, int val)
        {
            val+=10;
            printDel(val);
        }






        public static void Main(string[] args)
        {
            //PrintHelperMethod(30,20);
            //passing anonymous method as a parameter

            PrintHelperMethod(delegate(int val){
                Console.WriteLine("Anonymous Method: {0}", val);
            }, 30);




            Print printDel = delegate(int val, string str)
            {
                Console.WriteLine("Hello from the delegate!");
            };
            printDel(10, "Sample String");


            var ManagerInfo = new //anonymous type
            {
                Id=1001,
                Name = "Alice Johnson",
            };

            Console.WriteLine($"Manager ID: {ManagerInfo.Id}, Name: {ManagerInfo.Name}");

            MyMethod(ManagerInfo);
        }

        public static void MyMethod(dynamic dy){
            Console.WriteLine(dy);
            Console.WriteLine(dy);    
        }
    }
}