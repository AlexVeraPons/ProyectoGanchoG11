# C# GameProject Code Style Guide
---

This style guide created by Alexandro Vera for C# code used in the project ProjectGancho, designed for a short project. All decisions done to optimize time in this project, this is not as rigid as other Style Guides, due to the scope of the project and the participating developers experience using style guides.

----

## Formatting Guidelines
---

### Naming

Naming rules follow [Microsoft’s C# naming guidelines](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/naming-guidelines) and mainly inspired by [C# at Google Style Guide | styleguide](https://google.github.io/styleguide/csharp-style.html) and [C# Coding Standards Best Practices - Dofactory](https://www.dofactory.com/csharp-coding-standards#enumtypes)
adapted to out use cases.

### Code

#### **DO**

-   Names of classes, methods, enumerations, public fields, public properties, namespaces: `PascalCase`.
-   Declare all member variables at the top of a class, with static variables at the very top.
-   Vertically align curly brackets.
-   Space after `if`/`for`/`while` etc., and after commas.
-   Classes use noun or noun phrases for their name (Employee, HealthBar, Jumper)
-   Names of local variables, parameters: `camelCase`.
-   Names of private, protected, internal and protected internal fields and properties: `_camelCase`.
-   Naming convention is unaffected by modifiers such as const, static, readonly, etc.
-   Names of interfaces start with `I`, e.g. `IInterface`.

#### **DO NOT**

-   do not suffix enum names with Enum
-   do not use **Underscores** in names (Health_Bar).  
Exception: you can prefix private static variables with an underscore
-   avoid using **Abbreviations** (hpBar, PlayerCtrl).  
Exceptions: abbreviations commonly used as names, such as **Id, Xml, Ftp, Uri**

### Files

-   Filenames and directory names are `PascalCase`, e.g. `MyFile.cs`.
-   In general, prefer one **class** per file. 


## Example

```csharp
public class HealthBar() // PascalCase for classes // Classes use noun or noun phrases for their name
{ // Vertically align curly brackets
	public float CurrentHealth => _currentHealth; // PascalCase for public fields

	private float _currentHealth; // Names of private fields use _camelCase
	private const _healthTick; // Naming convention is unnaffected by modifiers 

	public UpdateHealth() // PascalCase for methods
	{
		float bool checker; // name of local variables in camelCase
		if (playerIsAlive) // space after if
		{ // Vertically aligned curly brackets
			
		}
	}
}

public Enum HealthTypes // do not suffix enum names with "Enum"
{ // Vertically aligned curly brackets

}

public Interface ITakeDamage() // Names of interfaces start with I
{
	
}

```

 
---

## Coding Guidelines

---

### Constants

-   Prefer named constants to magic numbers. 
```csharp
// DO NOT DO THIS

if (animationTime >= 0.7f)
{
	// do something=
}

// DO THIS INSTEAD
private const float _animationDuration = 0.7f;

if (animationTime >=  _animationDuration)

```

---

### Property Styles

For scenarios where you want to expose a read-only public property with a private backing field, prefer the following pattern:

1.  Create a private field with a name starting with an underscore, followed by the same name as the public property but in camelCase.
2.  Create a public read-only property using the expression body syntax (=>) and the backing private field.

This pattern allows you to have better control over the state of the property and ensures that it is read-only from external code.

**Example:**

```csharp
public class CameraController
{
    // Use a private backing field
    private Camera _currentCamera;

    // Expose a read-only public property with expression body syntax (=>)
    public Camera CurrentCamera => _currentCamera;
}
```

❌**Do not use** an auto-implemented property with private access:

```csharp
public class CameraController
{
    // Avoid using a private auto-implemented property
    private Camera CurrentCamera { get; set; }
}

```

By following this style guide choice, your code becomes more explicit and maintains the intended read-only access to the property from external code.

---

### Array vs List

-   In general, prefer `List<>` over arrays for public variables, properties, and return types 
-   Prefer `List<>` when the size of the container can change.
-   Prefer arrays when the size of the container is fixed and is full on start.
-   Prefer array for multidimensional arrays.

---

### The `var` keyword

-   Use of `var` is encouraged if it aids readability by avoiding type names that are noisy, obvious, or unimportant.

```csharp
// ENCOURAGED 
       // Encouraged: Type is obvious
        var apple = new Apple();
        var request = Factory.Create<HttpRequest>();

       // Encouraged: Transient variables
        var item = GetItem();
        ProcessItem(item);


// DISCOURAGED
       // Discouraged: Basic types
        bool success = true; // instead of: var success = true;

      // Discouraged: Compiler-resolved built-in numeric types
        float number = 12 * ReturnsFloat(); // instead of: var number = 12 * ReturnsFloat();

      // Discouraged: Users would benefit from knowing the type
        List<Item> listOfItems = GetList(); // instead of: var listOfItems = GetList();
```


---

### Argument Naming

When the meaning of a function argument is nonobvious, consider one of the following remedies:

-  If you use the same number or value multiple times in different parts of your program and assume they're all the same, it's better to give that number or value a name so it's clear that they're all the same, and to make sure they stay the same.
-   Consider using Named Arguments to clarify argument meanings at the call site.

#### Before vs After 

##### Before

```csharp
public class GameControllerBefore
{
    private void Start()
    {
        SpawnEnemy(3, 2.0f, false);
    }
}

```
Just looking  at the function that is being called we don't know what the arguments are doing

##### After

```csharp
public class GameControllerAfter
{
    private void Start()
    {
        // Using Named Arguments

        SpawnEnemy(enemyCount: 3, spawnDelay: 2.0f, isBoss: false);

        // OR 

        int enemyCount = 3;
        float spawnDelay = 2.0f;
        bool isBoss = false;
        SpawnEnemy(enemyCount, spawnDelay, isBoss);
    }
}
```
We don't need to look at the function implementation to know what is happening 


##### function that is being called
```csharp
private void SpawnEnemy(int enemyCount, float spawnDelay, bool isBoss) 
{ 
	// Example implementation: spawn enemies with specified parameters 
}
```

---

### Comments

-  ❌**Do not use** comments to explain something that is obvious
```csharp
int maxEnemies = 3; // Max amount of enemies
```

-  **TRY to avoid** inline comments when possible, they add clutter and you should not have to explain code line by line unless absolutely necessary
```csharp
// DO NOT DO THIS

int maxEnemies = Scene.LoadAmount; // set the enemies to the scene load amount
int maxEnemies += _enemiesDifference; // add the random diference

// IF EXPLANATION OF THE CODE IS REALLY NECESSARY PREFER

//Calculates how many enemies are needed in the level 
int maxEnemies = Scene.LoadAmount;
int maxEnemies += _enemies Difference;
```

- Think of other ways to represent your code so that it does not need commenting 
- If you must comment, think long and hard of what you comment. Commenting is as important as code itself.
- Be clear and concise!

---

### Usage of the `this.` keyword

 - Usage of `this.` is highly encouraged to clarify who the variable refers to.

**Example:**

```csharp
// BEFORE
//what transform??
bullet.transform.SetParent(transform);

//AFTER
bullet.transform.SetParent(this.transform);
```

---

### Vertical order of Scripts

The vertical order should be like this: (loosely based on the order of execution[Unity - Manual: Order of execution for event functions (unity3d.com)](https://docs.unity3d.com/Manual/ExecutionOrder.html) with some changes for readability). **In the case that you don't need one of this functions just omit it.**

- **Public** variables
- **Serializefield** variables
- **Private** Variable
- **OnEnable/OnDisable** Functions
- **Awake** Function
- **Start** Function
- **FixedUpdate** Function
- **Update** Function

**Example:**

```csharp
using UnityEngine;

public class ExampleScript : MonoBehaviour
{
    // Public variables
    public float PublicVariable1;
    public string PublicVariable2;
    
    // Serialized variables
    [SerializeField] private int _serializedVariable1;
    [SerializeField] private GameObject _serializedVariable2;
    
    // Private variable
    private Rigidbody _privateVariable;
    
    // OnEnable/OnDisable functions
    private void OnEnable()
    {
        
    }
    
    private void OnDisable()
    {
        
    }
    
    // Awake function
    private void Awake()
    {
        
    }
    
    // Start function
    private void Start()
    {
        
    }
    
    // FixedUpdate function
    private void FixedUpdate()
    {
        
    }
    
    // Update function
    private void Update()
    {
        
    }
}

```

---

## Git

### Branches

- Master branch
- Version branches
- Feature branches
- Hot-fix branches

#### Master
This branch should contain the most stable and release-ready version of the game. You should only merge changes into this branch once they have been tested and are ready. This is the branch that will be used by testers and our game designers.

#### Version branches
This branch is the most up to date version of our game. This will later be merged down into Master once everything is tidied up and working as expected.

**Naming:** Version/(nameOfVersion

#### Feature branches
Used for each feature such as enemies, these are then merged into version branches when code has been cleaned up and all unnecessary comments and other things have been deleted. 

**Naming:** Feature/(nameOfFeature)

#### Hotfix branches
Used to fix bugs that were not found in the feature or version merges, when the bug is fixed it is instantly merged back

**Naming:** Hotfix/(nameOfBug)

### Implementation Example 

![[Project Style Guide 2023-04-09 21.56.23.excalidraw]]

---

# Good Practices

Here I will include tips I've been gathering through research, and practice. I will include all the sources and if you want to read any of the books I mention just message me and I will surely lend it to you!!

## Re writing code!!

To create clean code don't expect for it to come in an instant, be sure that before committing a change that you have cleaned up your code and improved its readability. Also don't be afraid of improving your peers code, but don't change their functionality. Remember programming is mostly reading code, so do a favor to yourself and to your companions and double check your readability!

## Tips on when to split classes

A class should follow the SOLID principles, a classes variables should be used in most functions, if you see that half of the variables are being used by some functions and the other half by other functions it might be the class crying out o be splitted into two classes

## Refactoring

Although this is already known I want to underline the importance of Refactoring, whenever you can, to explain big blocks of code or to simplify the reading experience.

### **Example:**

**Before:**
```csharp
if (playerHasPowerup) {
    if (playerIsJumping && playerIsMoving) {
        playerSpeed = playerDefaultSpeed * 2;
        playerJumpHeight = playerDefaultJumpHeight * 2;
    } else if (playerIsJumping) {
        playerJumpHeight = playerDefaultJumpHeight * 2;
    } else if (playerIsMoving) {
        playerSpeed = playerDefaultSpeed * 2;
    }
} else {
    if (playerIsJumping && playerIsMoving) {
        playerSpeed = playerDefaultSpeed * 1.5f;
        playerJumpHeight = playerDefaultJumpHeight * 1.5f;
    } else if (playerIsJumping) {
        playerJumpHeight = playerDefaultJumpHeight * 1.5f;
    } else if (playerIsMoving) {
        playerSpeed = playerDefaultSpeed * 1.5f;
    }
}
```
As you can see this is hard to understand without studying it deeply

**After:**

```csharp
if (playerHasPowerup) {
    ModifyPlayerSpeedAndJumpHeight(2f, 2f);
} else {
    ModifyPlayerSpeedAndJumpHeight(1.5f, 1.5f);
}

///////

private void ModifyPlayerSpeedAndJumpHeight(float speedMultiplier, float jumpHeightMultiplier) {
    if (playerIsJumping && playerIsMoving) {
        playerSpeed = playerDefaultSpeed * speedMultiplier;
        playerJumpHeight = playerDefaultJumpHeight * jumpHeightMultiplier;
    } else if (playerIsJumping) {
        playerJumpHeight = playerDefaultJumpHeight * jumpHeightMultiplier;
    } else if (playerIsMoving) {
        playerSpeed = playerDefaultSpeed * speedMultiplier;
    }
}

```
Now you can easily understand what is happening inside the code

# Code Smells

> A _code smell_ is a hint that something has gone wrong somewhere in your code. Note that a CodeSmell is a hint that something might be wrong, not a certainty. A perfectly good idiom may be considered a CodeSmell because it’s often misused, or because there’s a simpler alternative that works in most cases. Calling something a CodeSmell is not an attack; it’s simply a sign that a closer look is warranted.
> 
> \- > [Wards Wiki](http://www.c2.com/cgi/wiki?CodeSmell)

Here I will provide the code smells that I found the most useful. If you want to further read I recommend reading Clean code Chapter 17: "Smells and Heuristics" and this link.
[31 code smells all developers should be familiar with (pragmaticways.com)](https://pragmaticways.com/31-code-smells-you-must-know/)

## General

###  Obvious Behavior Is Unimplemented

Following “The Principle of Least Surprise,” any function or class should implement the
behaviors that another programmer could reasonably expect. For example, consider a
function that translates the name of a day to an enum that represents the day.
```csharp
Day day = DayDate.StringToDay(String dayName);
```

We would expect the string "Monday" to be translated to Day.MONDAY. We would also expect
the common abbreviations to be translated, and we would expect the function to ignore
case.

When an obvious behavior is not implemented, readers and users of the code can no
longer depend on their intuition about function names. They lose their trust in the original
author and must fall back on reading the details of the code.

### Vertical Separation

Variables and function should be defined close to where they are used. Local variables
should be declared just above their first usage and should have a small vertical scope. We
don’t want local variables declared hundreds of lines distant from their usages.
Private functions should be defined just below their first usage. Private functions
belong to the scope of the whole class, but we’d still like to limit the vertical distance
between the invocations and definitions. Finding a private function should just be a matter
of scanning downward from the first usage.


### Function Names Should Say What They Do

Look at this code:

```csharp
	Date newDate = date.add(5);
```

Would you expect this to add five days to the date? Or is it weeks, or hours? Is the date
instance changed or does the function just return a new Date without changing the old one?
You can’t tell from the call what the function does.

If the function adds five days to the date and changes the date, then it should be called
addDaysTo or increaseByDays. If, on the other hand, the function returns a new date that is
five days later but does not change the date instance, it should be called daysLater or
daysSince.

If you have to look at the implementation (or documentation) of the function to know
what it does, then you should work to find a better name or rearrange the functionality so
that it can be placed in functions with better names.

### Avoid Negative Conditionals

Negatives are just a bit harder to understand than positives. So, when possible, conditions should be expressed as positives. For example:
```csharp
	if (buffer.shouldCompact())
// is preferable to 
	if (!buffer.shouldNotCompact())

```

### Functions Should Do One Thing

It is often tempting to create functions that have multiple sections that perform a series of
operations. Functions of this kind do more than one thing, and should be converted into
many smaller functions, each of which does one thing.
For example:

```csharp
		public void pay() 
		{
        for (Employee e : employees) 
        {
            if (e.isPayday()) 
            {
                Money pay = e.calculatePay();
                e.deliverPay(pay);
            }
        }
	   }
```
This bit of code does three things. It loops over all the employees, checks to see whether
each employee ought to be paid, and then pays the employee. This code would be better
written as:

```csharp
public void pay() {
    for (Employee e : employees)
        payIfNecessary(e);
}
private void payIfNecessary(Employee e) {
    if (e.isPayday())
        calculateAndDeliverPay(e);
}
private void calculateAndDeliverPay(Employee e) {
    Money pay = e.calculatePay();
    e.deliverPay(pay);
}
```

Each of these functions does one thing.

---

# References 
---

[How to Create a Programming Style Guide | PullRequest Blog](https://www.pullrequest.com/blog/create-a-programming-style-guide/)
	[Kristories/awesome-guidelines: A curated list of high quality coding style conventions and standards. (github.com)](https://github.com/Kristories/awesome-guidelines)
[Unity — C# Coding Standards. Use C# Coding standards to benefit to… | by Samuel Asher Rivello | Medium](https://sam-16930.medium.com/coding-standards-in-c-39aefee92db8)


[C# at Google Style Guide | styleguide](https://google.github.io/styleguide/csharp-style.html)
[C# Coding Conventions | Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
[c-sharp-style-guide/README.markdown at master · kodecocodes/c-sharp-style-guide (github.com)](https://github.com/kodecocodes/c-sharp-style-guide/blob/master/README.markdown)
[C# Coding Standards Best Practices - Dofactory](https://www.dofactory.com/csharp-coding-standards#enumtypes)

[Git Branch Naming Convention: 7 Best Practices to Follow | HackerNoon](https://hackernoon.com/git-branch-naming-convention-7-best-practices-to-follow-1c2l33g2)

[31 code smells all developers should be familiar with (pragmaticways.com)](https://pragmaticways.com/31-code-smells-you-must-know/)

---
