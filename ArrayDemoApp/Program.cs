using System;

namespace ArrayDemoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Declare and initialize an array of integers
            // int[] arr;
            // arr = new int[6];

            // arr[0]= 10;
            // arr[1]= 20;
            // arr[2]= 30;
            // arr[3]= 40;
            // arr[4]= 50;
            // arr[5]= 60;

            // Console.WriteLine("Enter length of array:");
            // int n = Convert.ToInt32(Console.ReadLine());
            // int[] arr = new int[n];
            // Console.WriteLine("Enter elements of array:");
            // for (int i = 0; i < n; i++)
            // {
            //     arr[i] = Convert.ToInt32(Console.ReadLine());
            // }


            // int length = arr.Length;
            // Console.WriteLine("Length of the array: " + length + "\n");

            // for (int i = 0; i < n; i++)
            // {
            //     Console.WriteLine("Element at index " + i + ": " + arr[i]);
            // }

            // foreach(int i in arr){
            //     Console.WriteLine("Element: " + i);
            // }

            // char[] charArray = new char[11]{'H', 'e', 'l', 'l', 'o', ' ', 'W', 'o', 'r', 'l', 'd'};
            // char[] charArray2 = new char[10];
            // Console.WriteLine("Character Array 1: ");
            // for(int i=0; i<charArray2.Length; i++){
            //     charArray2[i]=Convert.ToChar(Console.ReadLine());
            // }
            // for(int i=0; i<charArray2.Length; i++){
            //     Console.Write(charArray2[i]);
            // }

           int[,] arr = new int [3,3];
           arr[0,0]=1;
           arr[0,1]=2;
           arr[0,2]=3;
                    arr[1,0]=4;
                        arr[1,1]=5;
                        arr[1,2]=6;
                            arr[2,0]=7;
                            arr[2,1]=8;
                                arr[2,2]=9;

            Console.WriteLine("2D Array Elements: ");
            for(int i=0; i<3; i++){
                for(int j=0; j<3; j++){
                    Console.Write(arr[i,j] + " ");
                }
                Console.WriteLine();
            }   

    }

}
}