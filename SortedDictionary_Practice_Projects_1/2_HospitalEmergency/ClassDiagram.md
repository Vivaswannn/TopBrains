# 2. Hospital Emergency Queue — Class Diagram

```
                    <<abstract>>
                    Person
    + Id, Name
           △
           |
        Patient
    + Severity, Condition
    + Treat(): string  (polymorphic)
```

**Exceptions:** `EmergencyException` → `InvalidSeverityException`, `PatientNotFoundException`, `QueueOverflowException`

**Core:** `SortedDictionary<int, Queue<Patient>>` — key = severity (1 = Critical). Get next = first available from lowest key.
