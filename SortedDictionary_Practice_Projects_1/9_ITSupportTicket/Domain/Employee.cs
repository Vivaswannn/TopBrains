namespace ITSupportTicket.Domain;

public class Employee : User
{
    public string Department { get; }
    public Employee(string id, string name, string department) : base(id, name) => Department = department ?? "";
}
