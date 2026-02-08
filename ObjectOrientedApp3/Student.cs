// 12) program to read student num,name,mark of six subject in property and 
// calculate total and average and print result and division */
// program that reads a student’s exam score from the user and determines their grade based on the following grading scale:
// •	Score ≥ 90 : Grade A
// •	80 ≥ Score < 90 : Grade B
// •	70 ≥ Score < 80 : Grade C
// •	60 ≥ Score < 70 : Grade D
// •	Score < 60 : Grade F

using System;

class Student{
    
    private int _num;
    private string _name;

    private int[] _marks= new int[6];

    public int Num{
        get{
            return _num;
        }
        set{
            _num=value;
        }
    }
    public string Name{
        get{
            return _name;
        }
        set{
            _name=value;
        }
    }
    public int[] Marks{
        get{
            return _marks;
        }
        set{
            _marks=value;
        }
    }

    public void AcceptDetails(){
        Console.WriteLine("Enter Student Number:");
        Num=Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter Student Name:");
        Name=Console.ReadLine();
        for(int i=0;i<6;i++){
            Console.WriteLine($"Enter Mark for Subject {i+1}:");
            Marks[i]=Convert.ToInt32(Console.ReadLine());
        }
    }

    public void DisplayDetails(){
        int total=0;
        for(int i=0;i<6;i++){
            total+=Marks[i];
        }
        float average=total/6.0f;

        Console.WriteLine($"Student Number: {Num}");
        Console.WriteLine($"Student Name: {Name}");
        Console.WriteLine($"Total Marks: {total}");
        Console.WriteLine($"Average Marks: {average}");

        if(average>=90){
            Console.WriteLine("Grade: A");
        }
        else if(average>=80){
            Console.WriteLine("Grade: B");
        }
        else if(average>=70){
            Console.WriteLine("Grade: C");
        }
        else if(average>=60){
            Console.WriteLine("Grade: D");
        }
        else{
            Console.WriteLine("Grade: F");
        }
    }
}