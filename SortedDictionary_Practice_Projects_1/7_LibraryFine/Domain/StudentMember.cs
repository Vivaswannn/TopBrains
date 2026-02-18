namespace LibraryFine.Domain;

public class StudentMember : LibraryUser
{
    private const decimal RatePerDay = 0.25m;
    public StudentMember(string memberId, string name) : base(memberId, name) { }

    public override decimal CalculateFine(int daysOverdue) => daysOverdue * RatePerDay;
}
