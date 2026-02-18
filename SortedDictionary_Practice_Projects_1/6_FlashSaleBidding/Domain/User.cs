namespace FlashSaleBidding.Domain;

public abstract class User
{
    public string Id { get; }
    public string Name { get; }

    protected User(string id, string name)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}
