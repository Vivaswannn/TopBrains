using System.Data;

class Employees:DataTable
{
    public Employees(){
        this.Columns.Add("EmpID",typeof(int));
        this.Columns.Add("EmpName",typeof(string));
        this.Columns.Add("Age",typeof(int));
        this.Columns.Add("Department",typeof(string));
        this.Columns.Add("Salary",typeof(int));
    }
}