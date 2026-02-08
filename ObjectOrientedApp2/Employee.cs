// 13)/*program to read eno,ename,basic salary and calculate  
// pf,hra,da,net salary and gross salary and print eno,ename,basic 
// salary,
// gross salary and net salary*/

// pf= 12% of basic salary.
// hra=20% of basic salary.
// da= 15% of basic salary.
// gross salary=pf+hra+da+basic salary;
// net salary=gross salary - pf;

using System;

class Employee{
    private int _eno;
    private string _ename;
    private float _basicsalary;
    private float pf;
    private float hra;
    private float da;
    private float grosssalary;
    private float netsalary;

    public int Eno{
        get{
            return _eno;
        }
        set{
            _eno=value;
        }
    }

    public string Ename{
        get{
            return _ename;
        }
        set{
            _ename=value;
        }
    }
    
    public float BasicSalary{
        get{
            return _basicsalary;
        }
        set{
            _basicsalary=value;
        }
    }

    public void AcceptDetails(){
        Console.WriteLine("Enter Employee Number:");
        Eno=Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter Employee Name:");
        Ename=Console.ReadLine();
        Console.WriteLine("Enter Employee Basic Salary:");
        BasicSalary=Convert.ToSingle(Console.ReadLine());
    }

    public void DisplayDetails(){
        pf= (12*BasicSalary)/100;
        hra= (20*BasicSalary)/100;
        da= (15*BasicSalary)/100;
        grosssalary= pf + hra + da + BasicSalary;
        netsalary= grosssalary - pf;

        Console.WriteLine("Employee Number: "+Eno);
        Console.WriteLine("Employee Name: "+Ename);
        Console.WriteLine("Employee Basic Salary: "+BasicSalary);
        Console.WriteLine("Employee Gross Salary: "+grosssalary);
        Console.WriteLine("Employee Net Salary: "+netsalary);
    }
}