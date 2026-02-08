using System;

class Calculator{
    
    public Calculator(){

    }

    public int Add(int num1, int num2){
        int summ=num1+num2;
        return summ;
    }

    public void Subtract(){
        int num1=5;
        int num2=3;
        int subt=num1-num2;
        Console.WriteLine("Subtraction is: " + subt);
    }

    public int Multiply(int num1, int num2){
        int mult=num1*num2;
        return mult;
    }

    public void Divide(){
        int num1=6;
        int num2=3;
        int divv=num1/num2;
        Console.WriteLine("Division is: " + divv);
    }
}