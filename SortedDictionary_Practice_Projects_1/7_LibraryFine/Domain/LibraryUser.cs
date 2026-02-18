namespace LibraryFine.Domain;

public abstract class LibraryUser
{
    public string MemberId { get; }
    public string Name { get; }

    protected LibraryUser(string memberId, string name)
    {
        MemberId = memberId ?? throw new ArgumentNullException(nameof(memberId));
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    /// <summary>Override in derived classes for different fine calculation.</summary>
    public abstract decimal CalculateFine(int daysOverdue);
}
