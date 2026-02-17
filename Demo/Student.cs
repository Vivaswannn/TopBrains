using System;

public class Student{
    public int RollNo {
        get; set;
    }

    public string Name {
        get; set;
    }

    //No argument constructor
    public Student() {
        RollNo = 0;
        Name = string.Empty;
    }

    //Parameterized constructor
    public Student(int rollNo, string name) {
        this.RollNo = rollNo;
        this.Name = name;
    }

    //method to display student details
    public void DisplayData(){
        Console.WriteLine("Roll No: {0}, Name: {1}", RollNo, Name);
        
    }
}