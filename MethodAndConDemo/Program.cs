using System;

class Program{
    public static void Main(string[] args){
        // Calculator calculator = new Calculator(); // calling new calculator as default constructor

        // int num1=10;
        // int num2=20;
        // int sum=calculator.Add(num1,num2); // calling method with parameter with return type
        // Console.WriteLine("Addition is: " + sum);

        // int subtract=calculator.Subtract(); // calling method without parameter with return type
        // Console.WriteLine("Subtraction is: " + subtract);


        // //calling new calculator as parameterized constructor
        // Calculator calculator2 = new Calculator(20,30);
        // calculator.Multiply(5,6); // calling method with parameter without return type

        // calculator2.Divide();

//...........................................................................................................


        // Calculator calculator = new Calculator();
        // int n1=30;
        // int n2=40;
        // System.Console.WriteLine("Before Swapping in Main: n1= " + n1 + " n2= " + n2);
        // calculator.Swap(n1,n2);
        // System.Console.WriteLine("After Swapping in Main: n1= " + n1 + " n2= " + n2);
        // Console.WriteLine("\n\n\n");

        // // Call by reference using ref keyword
        // int num3=50;
        // int num4=60;
        // System.Console.WriteLine("Before Swapping in Main using ref: num3= " + num3 + " num4= " + num4);
        // calculator.Swap1(ref num3,ref num4);
        // System.Console.WriteLine("After Swapping in Main using ref: num3= " + num3 + " num4= " + num4);
        // Console.WriteLine("\n\n\n");

        // // Out parameter
        // Calculator calculator2 = new Calculator();
        // int num1=30;
        // int num2=50;
        // int result=0;
        // int num3=0;
        // int num4=0;

        // calculator2.Addition(num1,num2,out result,out num3,out num4);
        // Console.WriteLine("In Main Addition is: " + result);

        StaticInstanceDemo demo = new StaticInstanceDemo();
        demo.var=30;
        demo.Count();
        demo.Count();
        demo.Count();
        Console.WriteLine("Instance variable after increment: " + demo.Display());

        //demo.var1=40; //error cannot access static variable or method by using instance
        //StaticInstanceDemo.var1=50;
    }
}