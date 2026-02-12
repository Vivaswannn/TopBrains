// using System;
// using System.Collections.Generic;
// using System.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

// public class Student{
//     public int StudentID{get;set;}
//     public string StudentName{get;set;}
//     public int Age{get;set;}

// }

class Program{
    public static void Main(){
        //data source
        //create an instance of the Employees DataTable
        var employees=new Employees();
        //add rows to the Employees DataTable
        employees.Rows.Add(1,"Mark",25,"IT",5000);
        employees.Rows.Add(2,"Steve",30,"HR",6000);
        employees.Rows.Add(3,"Ben",28,"IT",5500);
        employees.Rows.Add(4,"Philip",32,"Finance",7000);
        employees.Rows.Add(5,"Mary",27,"HR",6200);

        //write query
        //Use LINQ to query DataTable
        var empQuery=from emp in employees.AsEnumerable()
                     where emp.Field<string>("Department")=="IT"
                     orderby emp.Field<int>("Salary") descending
                     select new{
                        Id=emp.Field<int>("EmpID"),
                        Name=emp.Field<string>("EmpName"),
                        Age=emp.Field<int>("Age"),
                        Department=emp.Field<string>("Department"),
                        Salary=emp.Field<int>("Salary")
                     };

        //execute the query
        Console.WriteLine("Employees in IT Department:");
        foreach(var emp in empQuery){
            Console.WriteLine("ID:{0}, Name:{1}, Age:{2}, Department:{3}, Salary:{4}",
            emp.Id,emp.Name,emp.Age,emp.Department,emp.Salary);
        }











        // //data source
        // //student collection
        // IList<Student> studentlist=new List<Student>(){
        //     new Student{StudentID=1,StudentName="Bill",Age=21},
        //     new Student{StudentID=2,StudentName="Steve",Age=22},
        //     new Student{StudentID=3,StudentName="John",Age=23},
        //     new Student{StudentID=4,StudentName="Mary",Age=24}
        // };
        // //write query
        // //IEnumerable<Student> Students=
        
        // var Students=from s in studentlist
        //              where s.Age>20 && s.Age<24
        //              select s;
        // //method syntax
        // var StudentsMethod=studentlist.Where(s=>s.Age>20 && s.Age<24);


        // //execute the query
        // Console.WriteLine("Following are the students with age between 20 and 24");
        // foreach(var student in Students){
        //     Console.WriteLine("Name:{0} Age:{1}",student.StudentName,student.Age);
        // }







        // //string collection data source
        // IList<string> stringlist=new List<string>()
        // {
        //    "C# Tutorials",
        //    "VB.NET Tutorials",
        //    "Learn C++",
        //    "MVC Tutorials",
        //     "Java Tutorials"
        // };

        // //Write query
        // //LINQ Query Syntax
        // var result = from s in stringlist
        //              where s.Contains("Tutorials")
        //              select s;

        // //execute the query
        // foreach(var r in result){
        //     Console.WriteLine(r);
        // }









        // //data source
        // string[] names = { "Bill", "Steve", "James", "Mohan" };


        // //write a query
        // //query syntax
        // // var name=from s in names
        // //           where s.Contains('a')
        // //           select s;

        // //method syntax
        // var name=names.Where(s=>s.Contains('a'));
                  
        // //execute the query
        // foreach (var item in name)
        // {
        //     Console.WriteLine(item);
        // }
    }
}