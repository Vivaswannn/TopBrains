using System;

class Program{
    public static void Main(string[] args){

        Student student=new Student();

        // student.AcceptDetails();
        // student.DisplayDetails();

        // student.Num=1;
        // student.Name="Alice Smith";
        //student.Marks=new int[]{95, 88, 92, 85, 90, 87};
        student.AcceptDetails();
        student.DisplayDetails();

        Console.ReadKey();
    }
}