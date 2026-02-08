using System;

class Employee  //internal is the default access modifier for classes when nothing is specified
{
    private int _empId=0;
    private string _name=string.Empty;
    private string _department=string.Empty;
    private float _salary=0.0f;
    private bool _status=true;

    public int EmpId{ //EmpId property
        get{
            return _empId;
        }
        set{
            _empId=value;
        }
    }

    public string Name{ //Name property
        get{
            return _name;
        }
        set{
            _name=value;
        }
    }
    public string Department{ //Despartment property
        get{
            return _department;
        }
        set{
            _department=value;
        }
    }
    public float Salary{ //Salary property
        get{
            return _salary;
        }
        set{
            _salary=value;
        }
    }
    public bool status{ //status property
        get{
            return _status;
        }
        set{
            _status=value;
        }
    }

    public void AcceptDetails(){
        Console.WriteLine("Enter Employee Id:");
        EmpId=Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter Employee Name:");
        Name=Console.ReadLine();
        Console.WriteLine("Enter Employee Department:");
        Department=Console.ReadLine();
        Console.WriteLine("Enter Employee Salary:");
        Salary=Convert.ToSingle(Console.ReadLine());
    }
    public void DisplayDetails(){
        Console.WriteLine("Employee Id: "+EmpId);
        Console.WriteLine("Employee Name: "+Name);
        Console.WriteLine("Employee Department: "+Department);
        Console.WriteLine("Employee Salary: "+Salary);
        Console.WriteLine("Employee Status: "+status);
    }
}