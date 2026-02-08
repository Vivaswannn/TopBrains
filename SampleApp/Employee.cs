using System;
public class Employee: IComparable<Employee>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public int CompareTo(Employee? other)
    {
        if (other == null) return 1;
        return this.Id.CompareTo(other.Id); //ascending order
    }

    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}";
    }
}