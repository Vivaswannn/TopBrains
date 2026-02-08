// 1. Write a program in C# Sharp to store elements in an array and print it.               
// Test Data:
// Input 10 elements in the array:
// element - 0 : 1
// element - 1 : 1
// element - 2 : 2
// .......
// Expected Output :
// Elements in array are: 1 1 2 3 4 5 6 7 8 9

// using System;

// namespace Array1{
//     class program{
//         public static void Main(string[] args){
//             int[] arr= new int[10];
//             Console.WriteLine("Input 10 elements in the array:");
//             for(int i=0;i<10;i++){
//                 Console.Write("element - {0} : ",i);
//                 arr[i]=Convert.ToInt32(Console.ReadLine());
//             }
//             Console.WriteLine("Elements in array are: ");
//             for(int i=0;i<10;i++){
//                 Console.Write(arr[i] + " ");
//             }
//         }
//     }
// }



// 2. Write a program in C# Sharp to read n number of values in an array and display it in reverse order.               
// Test Data :
// Input the number of elements to store in the array :3
// Input 3 number of elements in the array :
// element - 0 : 2
// element - 1 : 5
// element - 2 : 7
// Expected Output:
// The values store into the array are:
// 2 5 7
// The values store into the array in reverse are :
// 7 5 2

// using System;

// namespace Array1{
//     class program{
//         public static void Main(string[] args){
//             Console.WriteLine("Input the number of elements to store in the array :");
//             int n=Convert.ToInt32(Console.ReadLine());
//             int[] arr= new int[n];
//             Console.WriteLine("Input {0} number of elements in the array :",n);
//             for(int i=0;i<n;i++){
//                 Console.Write("element - {0} : ",i);
//                 arr[i]=Convert.ToInt32(Console.ReadLine());
//             }
//             Console.WriteLine("The values store into the array are:");
//             for(int i=0;i<n;i++){
//                 Console.Write(arr[i] + " ");
//             }
//             Console.WriteLine("\nThe values store into the array in reverse are :");
//             for(int i=n-1;i>=0;i--){
//                 Console.Write(arr[i] + " ");
//             }
//         }
//     }
// }



// 3. Write a program in C# Sharp to find the sum of all elements of the array.               
// Test Data :
// Input the number of elements to be stored in the array :3
// Input 3 elements in the array :
// element - 0 : 2
// element - 1 : 5
// element - 2 : 8
// Expected Output :
// Sum of all elements stored in the array is : 15

// using System;

// namespace Array1{
//     class program{
//         public static void Main(string[] args){
//             Console.WriteLine("Input the number of elements to be stored in the array :");
//             int n=Convert.ToInt32(Console.ReadLine());
//             int[] arr= new int[n];
//             Console.WriteLine("Input {0} elements in the array :",n);
//             for(int i=0;i<n;i++){
//                 Console.Write("element - {0} : ",i);
//                 arr[i]=Convert.ToInt32(Console.ReadLine());
//             }
//             int sum=0;
//             for(int i=0;i<n;i++){
//                 sum+=arr[i];
//             }
//             Console.WriteLine("Sum of all elements stored in the array is : {0}",sum);
//         }}}




// 4. Write a program in C# Sharp to copy the elements one array into another array.               
// Test Data :
// Input the number of elements to be stored in the array :3
// Input 3 elements in the array :
// element - 0 : 15
// element - 1 : 10
// element - 2 : 12
// Expected Output:
// The elements stored in the first array are :
// 15 10 12
// The elements copied into the second array are :
// 15 10 12


// using System;

// namespace Array1{
//     class program{
//         public static void Main(string[] args){
//             Console.WriteLine("Input the number of elements to be stored in the array :");
//             int n= Convert.ToInt32(Console.ReadLine());
//             int[] arr=new int[n];
//             Console.WriteLine("Input {0} elements in the array :",n);
//             for(int i=0;i<n;i++){
//                 Console.Write("element - {0} : ",i);
//                 arr[i]=Convert.ToInt32(Console.ReadLine());
//             }
//             int[] arr2=new int[n];
//             for(int i=0;i<n;i++){
//                 arr2[i]=arr[i];
//             }
//             Console.WriteLine("The elements stored in the first array are :");
//             for(int i=0;i<n;i++){
//                 Console.Write(arr[i] + " ");
//             }
//             Console.WriteLine("\nThe elements copied into the second array are :");
//             for(int i=0;i<n;i++){
//                 Console.Write(arr2[i] + " ");
//             }
//         }}}




// 5. Write a program in C# Sharp to count a total number of duplicate elements in an array.               
// Test Data :
// Input the number of elements to be stored in the array :3
// Input 3 elements in the array :
// element - 0 : 5
// element - 1 : 1
// element - 2 : 1
// Expected Output :
// Total number of duplicate elements found in the array is : 1


// using System;

// namespace Array1 {
//     class Program {
//         public static void Main(string[] args) {

//             Console.WriteLine("Input the number of elements to be stored in the array :");
//             int n = Convert.ToInt32(Console.ReadLine());

//             int[] arr = new int[n];

//             Console.WriteLine("Input {0} elements in the array :", n);
//             for (int i = 0; i < n; i++) {
//                 Console.Write("element - {0} : ", i);
//                 arr[i] = Convert.ToInt32(Console.ReadLine());
//             }

//             int dupCount = 0;

//             for (int i = 0; i < n; i++) {

//                 // check if already counted before
//                 bool alreadyCounted = false;
//                 for (int k = 0; k < i; k++) {
//                     if (arr[i] == arr[k]) {
//                         alreadyCounted = true;
//                         break;
//                     }
//                 }

//                 if (alreadyCounted)
//                     continue;

//                 // check if it repeats later
//                 for (int j = i + 1; j < n; j++) {
//                     if (arr[i] == arr[j]) {
//                         dupCount++;
//                         break;
//                     }
//                 }
//             }

//             Console.WriteLine(
//                 "Total number of duplicate elements found in the array is : {0}",
//                 dupCount
//             );
//         }
//     }
// }




// 6. Write a program in C# Sharp to print all unique elements in an array.               
// Test Data :
// Input the number of elements to be stored in the array :3
// Input 3 elements in the array :
// element - 0 : 1
// element - 1 : 5
// element - 2 : 1
// Expected Output :
// The unique elements found in the array are :
// 5

// using System;

// namespace Array1 {
//     class Program {
//         public static void Main(string[] args) {

//             Console.WriteLine("Input the number of elements to be stored in the array :");
//             int n = Convert.ToInt32(Console.ReadLine());

//             int[] arr = new int[n];

//             Console.WriteLine("Input {0} elements in the array :", n);
//             for (int i = 0; i < n; i++) {
//                 Console.Write("element - {0} : ", i);
//                 arr[i] = Convert.ToInt32(Console.ReadLine());
//             }

//             Console.WriteLine("The unique elements found in the array are :");

//             for (int i = 0; i < n; i++) {
//                 bool isUnique = true;

//                 // check if arr[i] is unique
//                 for (int j = 0; j < n; j++) {
//                     if (i != j && arr[i] == arr[j]) {
//                         isUnique = false;
//                         break;
//                     }
//                 }

//                 if (isUnique) {
//                     Console.WriteLine(arr[i]);
//                 }
//             }
//         }
//     }
// }




// 7. Write a program in C# Sharp to merge two arrays of same size sorted in ascending order.               
// Test Data :
// Input the number of elements to be stored in the first array :3
// Input 3 elements in the array :
// element - 0 : 1
// element - 1 : 2
// element - 2 : 3
// Input the number of elements to be stored in the second array :3
// Input 3 elements in the array:
// element - 0 : 1
// element - 1 : 2
// element - 2 : 3
// Expected Output:
// The merged array in ascending order is :
// 1 1 2 2 3 3

using System;

namespace Array1 {
    class Program {
        public static void Main(string[] args) {

            Console.WriteLine("Input the number of elements to be stored in the first array :");
            int n1 = Convert.ToInt32(Console.ReadLine());

            int[] arr1 = new int[n1];

            Console.WriteLine("Input {0} elements in the array :", n1);
            for (int i = 0; i < n1; i++) {
                Console.Write("element - {0} : ", i);
                arr1[i] = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("Input the number of elements to be stored in the second array :");
            int n2 = Convert.ToInt32(Console.ReadLine());

            int[] arr2 = new int[n2];

            Console.WriteLine("Input {0} elements in the array:", n2);
            for (int i = 0; i < n2; i++) {
                Console.Write("element - {0} : ", i);
                arr2[i] = Convert.ToInt32(Console.ReadLine());
            }

            int[] mergedArr = new int[n1 + n2];
            int index = 0;

            for (int i = 0; i < n1; i++) {
                mergedArr[index++] = arr1[i];
            }

            for (int i = 0; i < n2; i++) {
                mergedArr[index++] = arr2[i];
            }

            Array.Sort(mergedArr);

            Console.WriteLine("The merged array in ascending order is :");
            for (int i = 0; i < mergedArr.Length; i++) {
                Console.Write(mergedArr[i] + " ");
            }
        }
    }
}