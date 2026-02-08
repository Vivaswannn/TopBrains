using System;
using System.Text;
class Program{
    public static void Main(string[] args){

        //arrays 
        // int[] arr=new int[5];
        // arr[0]=2;
        // arr[1]=4;
        // arr[2]=6;
        // arr[3]=8;
        // arr[4]=10;

        // for(int i=0;i<arr.Length;i++){
        //     Console.WriteLine(arr[i]);
        // }

        //collections
        // List<int> numbers= new List<int>();
        // numbers.Add(1);
        // numbers.Add(2);
        // numbers.Add(3);
        // numbers.Add(4);
        // numbers.Add(5);

        // for(int i =0;i<numbers.Count;i++){
        //     Console.WriteLine(numbers[i]);

       // Q: Why use List<T> instead of array?

//👉 Because List<T> is dynamic and type-safe.

// Q: What is the advantage of generics?

// 👉 Compile-time type safety and better performance.



//string and string builder

        string s = "hello";
        StringBuilder sb = new StringBuilder();

        sb.Append(s);
        sb.Append(" ");
        sb.Append("Vivaswan");

        Console.WriteLine(sb.ToString());
        }
    }
