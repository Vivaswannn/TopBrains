using System;

class Calculator
{
    // public int number1;
    // public int number2;
    // {
    //     get
    //     {
            
    //     }
    //     set
    //     {
            
    //     }
    // }

    public int Number1 { get; set; } // auto-implemented property, class level variable or instance variable
    public int Number2 { get; set; }

    public int Result { get; set; }

    public Calculator()  // default constructor
    {
        Number1 = 0;
        Number2 = 0;
        Result = 0;
    }

    public Calculator(int Number1, int Number2)  // parameterized constructor
    {
        this.Number1 = Number1;
        this.Number2 = Number2;
    }
//Method with Parameters and Return Type
//Method without Parameters and with Return Type
//Method with Parameters and without Return Type


    // method with parameters with return type
    private int Add(int num1, int num2)
    {
        Number1 = num1;
        Number2 = num2;
        Result = Number1 + Number2;
        return Result;
    }

    //method without parameter with return type
    public int Subtract(){
        Number1 = 30;
        Number2 = 10;
        Result = Number1 - Number2;
        return Result;
    }

    //method with parameter without return type
    public void Multiply(int num1, int num2){
        Number1 = num1;
        Number2 = num2;
        Result = Number1 * Number2;
        Console.WriteLine("Multiplication is: " + Result);
    }

    //method without parameter without return type
    public void Divide(){
        int num1=Number1;
        int num2=Number2;
        Result = num1 / num2;
        Console.WriteLine("Division is: " + Result);
    }

    public void Swap(int num1, int num2){
        int n1=num1;
        int n2=num2;
        int temp=0;
        Console.WriteLine("Before Swapping: n1= " + n1 + " n2= " + n2);

        temp=n1;
        n1=n2;
        n2=temp;
        Console.WriteLine("After Swapping: n1= " + n1 + " n2= " + n2);
    }

    //inout parameter passby reference
    public void Swap1(ref int num3, ref int num4){
        int temp=0;
        Console.WriteLine("Before Swapping: num3= " + num3 + " num4= " + num4);
        temp=num3;
        num3=num4;
        num4=temp;
        Console.WriteLine("After Swapping: num3= " + num3 + " num4= " + num4);
    }

    public void Addition(int n1, int n2, out int result, out int n3, out int n4){
        n3 = n1;
        n4 = n2;
        result = n1 + n2;
    }
}

