namespace LibraryFine.Exceptions;

public class FineNotFoundException : LibraryException
{
    public string MemberId { get; }
    public FineNotFoundException(string memberId) : base($"No fine found for member {memberId}.") => MemberId = memberId;
}
