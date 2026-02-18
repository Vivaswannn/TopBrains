using LibraryFine.Domain;
using LibraryFine.Exceptions;

namespace LibraryFine.Services;

/// <summary>SortedDictionary&lt;decimal, Member&gt; — key = outstanding fine (members sorted by fine).</summary>
public class FineService
{
    private readonly SortedDictionary<decimal, Member> _membersByFine = new();
    private readonly Dictionary<string, (decimal fineKey, Member member)> _byMemberId = new();

    private void ReindexMember(Member member, decimal oldFine, decimal newFine)
    {
        if (_byMemberId.TryGetValue(member.User.MemberId, out var pair))
        {
            _membersByFine.Remove(pair.fineKey);
        }
        member.OutstandingFine = newFine;
        while (_membersByFine.ContainsKey(newFine)) newFine += 0.01m;
        _membersByFine[newFine] = member;
        _byMemberId[member.User.MemberId] = (newFine, member);
    }

    public void AddFine(string memberId, decimal amount)
    {
        if (!_byMemberId.TryGetValue(memberId, out var pair))
            throw new FineNotFoundException(memberId);
        if (amount <= 0) throw new InvalidPaymentException(amount, "Fine amount must be positive.");
        var m = pair.member;
        var oldFine = m.OutstandingFine;
        ReindexMember(m, oldFine, oldFine + amount);
    }

    public void RegisterMember(Member member)
    {
        if (member == null) throw new ArgumentNullException(nameof(member));
        var fine = member.OutstandingFine;
        while (_membersByFine.ContainsKey(fine)) fine += 0.01m;
        _membersByFine[fine] = member;
        _byMemberId[member.User.MemberId] = (fine, member);
    }

    public void PayFine(string memberId, decimal amount)
    {
        if (!_byMemberId.TryGetValue(memberId, out var pair))
            throw new FineNotFoundException(memberId);
        if (amount <= 0) throw new InvalidPaymentException(amount, "Payment amount must be positive.");
        if (amount > pair.member.OutstandingFine)
            throw new InvalidPaymentException(amount, "Payment exceeds outstanding fine.");
        var m = pair.member;
        ReindexMember(m, m.OutstandingFine, m.OutstandingFine - amount);
    }

    public IReadOnlyDictionary<decimal, Member> GetMembersByFine() => _membersByFine;
}
