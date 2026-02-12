using System;
using System.Collections;

public class Student{
    public int Id{get;set;}
    public string Name{get;set;}
    public string Grade{get;set;}
}

public class StudentManager{
    public Dictionary<int, Student> Students{get;set;}

    public StudentManager(){
        Students= new Dictionary<int, Student>();

    }

    public void AddStudent(Student student){
        Students[student.Id]=student;
    }
    public void DisplayStudents(){
        Console.WriteLine("enter students information:");
        foreach(KeyValuePair<int,Student> entry in Students){
            Student student=entry.Value;
            Console.WriteLine("ID: "+student.Id+", Name: "+student.Name+", Grade: "+student.Grade);
        }
    }
}

class Program{
    public static void Main(string[] args){
        StudentManager manager= new StudentManager();

        while(true){
            string input= Console.ReadLine();
            if(input.ToLower()=="exit"){
                break;
            }

            int id;
            if(!int.TryParse(input,out id)){
                Console.WriteLine("Invalid ID. Please enter a numeric value.");
                continue;
            }

            Console.WriteLine("Enter student name:");
            string name= Console.ReadLine();

            Console.WriteLine("Enter student grade:");
            string grade= Console.ReadLine();

            Student student= new Student(){
                Id=id,
                Name=name,
                Grade=grade
            };

            manager.AddStudent(student);
        }

        manager.DisplayStudents();
    }
}