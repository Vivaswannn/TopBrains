# 5. University Course Registration — Class Diagram

```
                    <<abstract>>
                      Person
    + Id, Name
           △
           |
        Student
    + Gpa
```

**Exceptions:** `RegistrationException` → `CourseFullException`, `InvalidGPAException`, `DuplicateEnrollmentException`

**Core:** `SortedDictionary<double, List<Student>>` — key = GPA, descending. Encapsulation: enrollment validation in service.
