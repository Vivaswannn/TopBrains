namespace UniversityCourseRegistration.Domain;

public class Student : Person
{
    public double Gpa { get; }

    public Student(string id, string name, double gpa) : base(id, name)
    {
        if (gpa < 0 || gpa > 4.0) throw new Exceptions.InvalidGPAException(gpa);
        Gpa = gpa;
    }
}
