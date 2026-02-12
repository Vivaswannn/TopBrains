using System;
class Employee{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public string Salary { get; set; }

    public Employee(){

    }

    public Employee(int employeeId, string name, float salary)
    {
        EmployeeId = employeeId;
        Name = name;
        Salary = salary.ToString();
    }
    

}