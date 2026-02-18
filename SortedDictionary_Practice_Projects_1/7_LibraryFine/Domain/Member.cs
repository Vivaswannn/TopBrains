namespace LibraryFine.Domain;

/// <summary>Member with outstanding fine amount (for SortedDictionary key).</summary>
public class Member
{
    public LibraryUser User { get; }
    public decimal OutstandingFine { get; set; }

    public Member(LibraryUser user, decimal initialFine = 0)
    {
        User = user ?? throw new ArgumentNullException(nameof(user));
        OutstandingFine = initialFine >= 0 ? initialFine : 0;
    }
}
