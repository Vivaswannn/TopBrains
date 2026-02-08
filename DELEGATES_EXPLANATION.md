# Delegates and Multicast Delegates - Complete Explanation

## 📚 Table of Contents
1. [What is a Delegate?](#what-is-a-delegate)
2. [Single-Cast Delegates](#single-cast-delegates)
3. [Multicast Delegates](#multicast-delegates)
4. [How It Works Step-by-Step](#how-it-works-step-by-step)
5. [Key Concepts](#key-concepts)
6. [Real-World Use Cases](#real-world-use-cases)

---

## What is a Delegate?

**Definition**: A delegate is a type-safe function pointer that holds references to methods with matching signatures.

### Syntax:
```csharp
public delegate ReturnType DelegateName(ParameterType param1, ParameterType param2);
```

### In Your Code:
```csharp
public delegate int CalculatorDelegate(int num1, int num2);
```

**This means**:
- The delegate can hold references to methods
- Those methods must take **two `int` parameters**
- Those methods must return an **`int`**

### Why Use Delegates?
- **Type Safety**: Compiler ensures method signatures match
- **Flexibility**: Can change which method is called at runtime
- **Callback Mechanism**: Pass methods as parameters
- **Event Handling**: Foundation for events in C#

---

## Single-Cast Delegates

**Definition**: A delegate that holds reference to **one** method.

### Example from Your Code:
```csharp
CalculatorDelegate calcutAdd = new CalculatorDelegate(calculator.Add);
```

**What happens**:
1. Creates a delegate instance
2. Points it to `calculator.Add` method
3. Can be called like: `calcutAdd(10, 20)`

### Visual Representation:
```
calcutAdd ──────→ Add(int, int)
calcutSub ──────→ Subtract(int, int)
calculMultiply ─→ Multiply(int, int)
calcutDivid ────→ Divide(int, int)
```

Each delegate is **independent** and points to **one method**.

---

## Multicast Delegates

**Definition**: A delegate that holds references to **multiple** methods in an invocation list.

### How to Create:
```csharp
// Start with one method
calcutMulticast = calcutAdd;

// Add more methods using +=
calcutMulticast += calcutSub;
calcutMulticast += calculMultiply;
calcutMulticast += calcutDivid;
```

### Visual Representation:
```
calcutMulticast ──→ [Add, Subtract, Multiply, Divide]
                      ↓      ↓         ↓        ↓
                    Method  Method   Method  Method
```

### Execution Flow:
When you call `calcutMulticast(20, 10)`:

```
┌─────────────────────────────────────────┐
│ 1. Add(20, 10)        → Returns 30      │
│ 2. Subtract(20, 10)   → Returns 10      │
│ 3. Multiply(20, 10)   → Returns 200     │
│ 4. Divide(20, 10)     → Returns 2  ←─── │ Final return value
└─────────────────────────────────────────┘
```

**Important**: Only the **last method's return value** is returned!

---

## How It Works Step-by-Step

### Step 1: Delegate Declaration
```csharp
public delegate int CalculatorDelegate(int num1, int num2);
```
- Creates a **type** (like a class or interface)
- Defines the **signature** methods must match

### Step 2: Create Calculator Instance
```csharp
Calculator calculator = new Calculator();
```
- Creates an object with methods: Add, Subtract, Multiply, Divide

### Step 3: Create Single-Cast Delegates
```csharp
CalculatorDelegate calcutAdd = new CalculatorDelegate(calculator.Add);
```
- Each delegate points to **one** method
- Can be called individually

### Step 4: Create Multicast Delegate
```csharp
calcutMulticast = calcutAdd;              // Assign first method
calcutMulticast += calcutSub;             // Add second method
calcutMulticast += calculMultiply;        // Add third method
calcutMulticast += calcutDivid;           // Add fourth method
```

**What `+=` does**:
- Adds method to the **invocation list**
- Methods are stored in **order**
- All methods will execute when delegate is called

### Step 5: Invoke Multicast Delegate
```csharp
calcutMulticast(20, 10);
```

**What happens**:
1. All methods execute **sequentially**
2. Each method receives the **same parameters** (20, 10)
3. Return values are **discarded** except the last one
4. Final return value is **2** (from Divide)

---

## Key Concepts

### 1. Method Signature Matching
The method **must** match the delegate signature exactly:
- ✅ `public int Add(int num1, int num2)` - Matches!
- ❌ `public int Add(int num1)` - Wrong number of parameters
- ❌ `public void Add(int num1, int num2)` - Wrong return type

### 2. Delegate Invocation
You can call a delegate in two ways:
```csharp
// Method 1: Direct call
int result = calcutAdd(10, 20);

// Method 2: Using Invoke()
int result = calcutAdd.Invoke(10, 20);
```

### 3. Adding/Removing Methods
```csharp
// Add method
calcutMulticast += calculator.Add;

// Remove method
calcutMulticast -= calculator.Add;

// Check if delegate is null
if (calcutMulticast != null) {
    calcutMulticast(20, 10);
}
```

### 4. Return Values in Multicast
- **All methods execute**
- **Only last return value** is used
- Other return values are **discarded**

**If you need all return values**:
```csharp
// Get all delegates in the invocation list
Delegate[] delegates = calcutMulticast.GetInvocationList();
foreach (CalculatorDelegate del in delegates) {
    int result = del(20, 10);
    Console.WriteLine(result);
}
```

---

## Real-World Use Cases

### 1. Event Handling
```csharp
public delegate void ButtonClickHandler(object sender, EventArgs e);

ButtonClickHandler onClick = Button1_Click;
onClick += Button2_Click;  // Multiple handlers
onClick(sender, e);        // All handlers execute
```

### 2. Callback Functions
```csharp
public delegate void ProcessComplete(bool success);

void DoWork(ProcessComplete callback) {
    // ... do work ...
    callback(true);  // Notify when done
}
```

### 3. Strategy Pattern
```csharp
public delegate int CalculationStrategy(int a, int b);

CalculationStrategy strategy = Add;
// Later, can change strategy:
strategy = Multiply;
int result = strategy(10, 5);
```

### 4. LINQ and Functional Programming
```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
numbers.Where(x => x > 2).Select(x => x * 2);
// Where and Select use delegates internally
```

---

## Summary

| Concept | Description | Example |
|---------|-------------|---------|
| **Delegate** | Type-safe function pointer | `delegate int Calc(int a, int b)` |
| **Single-Cast** | One method reference | `Calc del = Add;` |
| **Multicast** | Multiple method references | `del += Subtract; del += Multiply;` |
| **Invocation** | Calling the delegate | `del(10, 20)` |
| **Return Value** | Last method's return (multicast) | `Divide` returns `2` |

---

## Your Code Flow Summary

1. **Declare** delegate type: `CalculatorDelegate`
2. **Create** calculator object with 4 methods
3. **Create** 4 single-cast delegates (one per method)
4. **Combine** all 4 into one multicast delegate
5. **Call** multicast delegate with (20, 10)
6. **Execute** all 4 methods sequentially
7. **Return** last method's result: `2`

**Output**: `called Multicast Delegate 2:`

---

## Key Takeaways

✅ Delegates provide **type-safe** method references  
✅ Single-cast = **one** method  
✅ Multicast = **multiple** methods  
✅ Multicast executes **all** methods in order  
✅ Only **last** return value is used  
✅ Use `+=` to add, `-=` to remove  
✅ Foundation for **events** in C#


