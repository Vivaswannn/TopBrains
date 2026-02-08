using System;

namespace PartialDemo{
    public partial class PartialEmployee{
        public void DisplayFullName(){
            Console.WriteLine("Full Name: " + FirstName + " " + LastName);
        }
        public void DisplayEmployeeData(){
            Console.WriteLine("-----Employee Details-----");
            Console.WriteLine("First Name: " + FirstName);
            Console.WriteLine("Last Name: " + LastName);
            Console.WriteLine("Salary: " + Salary);
            Console.WriteLine("Gender: " + Gender);
        }

        partial void PartialMethod(){ //Partial Method Implementation
            Console.WriteLine("This is a partial method implementation.");
        }
    }
}