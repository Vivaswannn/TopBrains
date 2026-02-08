# C# Advanced Concepts Explanation

## 1. Partial Classes

**Definition**: A class definition split across multiple files.

**Syntax**:
```csharp
// File 1: PartialEmployeeOne.cs
public partial class PartialEmployee {
    // Properties and fields
}

// File 2: PartialEmployeeTwo.cs
public partial class PartialEmployee {
    // Methods
}
```

**Key Points**:
- All parts must use `partial` keyword
- Must be in same namespace
- Compiler merges them into one class
- Useful for organizing large classes

---

## 2. Inheritance

**Definition**: A mechanism where a derived class inherits members from a base class.

**Syntax**:
```csharp
public class BaseClass { ... }
public class DerivedClass : BaseClass { ... }
```

**Inheritance Chain in Your Code**:
```
Employee (Base)
    ↓
Manager (Derived from Employee)
    ↓
GeneralManager (Derived from Manager)
```

**Benefits**:
- Code reusability
- Polymorphism
- Hierarchical modeling

---

## 3. Protected Access Modifier

**Definition**: Members accessible within the class and its derived classes.

**Access Levels**:
- `private`: Only within the class
- `protected`: Class + derived classes
- `public`: Everywhere
- `internal`: Same assembly

**Example**:
```csharp
public class Employee {
    protected int Eid;  // Manager can access this
    private int secret; // Manager CANNOT access this
}
```

---

## 4. Virtual and Override (Polymorphism)

**Virtual**: Marks a method as overridable in derived classes.
**Override**: Replaces the base implementation.

**Syntax**:
```csharp
// Base class
public virtual void MethodName() { ... }

// Derived class
public override void MethodName() { ... }
```

**Polymorphism Example**:
```csharp
Employee emp = new Manager();
emp.DisplayEmployeeData();  // Calls Manager's version!
```

**Rules**:
- Base method must be `virtual` or `abstract`
- Override method must use `override` keyword
- Cannot override `static` or `private` methods

---

## 5. Sealed Keyword

**Definition**: Prevents further inheritance.

**Syntax**:
```csharp
public sealed class Manager : Employee { ... }
// GeneralManager cannot inherit from sealed Manager
```

**When to Use**:
- Prevent unintended inheritance
- Security/design constraints
- Performance optimization

**Can Also Seal Methods**:
```csharp
public sealed override void MethodName() { ... }
// Prevents further overriding in child classes
```

---

## 6. Partial Methods

**Definition**: Method declared in one partial class part, implemented in another.

**Syntax**:
```csharp
// Declaration
partial void PartialMethod();

// Implementation
partial void PartialMethod() {
    // Implementation
}
```

**Rules**:
- Must return `void`
- Cannot have access modifiers (implicitly `private`)
- Cannot be `virtual` or `abstract`
- If not implemented, calls are removed at compile time

---

## Complete Example Flow

```csharp
// 1. Create instance
PartialEmployee emp = new PartialEmployee();

// 2. Set properties (from PartialEmployeeOne.cs)
emp.FirstName = "John";

// 3. Call methods (from PartialEmployeeTwo.cs)
emp.DisplayFullName();      // Uses properties from PartialEmployeeOne.cs
emp.DisplayEmployeeData();  // Uses properties from PartialEmployeeOne.cs
```

The compiler treats both partial files as one complete class!



