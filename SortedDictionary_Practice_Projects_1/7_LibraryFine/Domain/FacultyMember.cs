namespace LibraryFine.Domain;

public class FacultyMember : LibraryUser
{
    private const decimal RatePerDay = 0.10m;
    public FacultyMember(string memberId, string name) : base(memberId, name) { }

    public override decimal CalculateFine(int daysOverdue) => daysOverdue * RatePerDay;
}
