namespace UniversityCourseRegistration.Domain;

public abstract class Person
{
    public string Id { get; }
    public string Name { get; }

    protected Person(string id, string name)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}
