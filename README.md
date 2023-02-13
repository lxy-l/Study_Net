# C#
---
### 类型系统
---
- 值类型：
    - 简单类型
        - 有符号整型：sbyte、short、int、long 
        - 无符号整型：byte、ushort、uint、ulong
        - Unicode 字符：char，表示 UTF-16 代码单元
        - IEEE 二进制浮点：float、double
        - 高精度十进制浮点数：decimal
        - 布尔值：bool，表示布尔值（true 或 false）
    - 枚举类型
        -  enum E {...} 格式的用户定义类型。 enum 类型是一种包含已命名常量的独特类型。 每个 enum 类型都有一个基础类型（必须是八种整型类型之一）。 enum 类型的值集与基础类型的值集相同。
    - 结构类型（结构是值类型，通常不需要进行堆分配。 结构类型不支持用户指定的继承，并且所有结构类型均隐式继承自类型 object）
        - 格式为 struct S {...} 的用户定义类型 
    - 可以为null的值类型
        - 值为 null 的其他所有值类型的扩展
    - 元组值类型
        - 格式为 (T1, T2, ...) 的用户定义类型 
- 引用类型：
    - 类类型（类类型支持单一继承和多形性，即派生类可以扩展和专门针对基类的机制。）
        - 其他所有类型的最终基类：object
        - Unicode 字符串：string，表示 UTF-16 代码单元序列
        - 格式为 class C {...} 的用户定义类型
    - 接口类型
        - 格式为 interface I {...} 的用户定义类型
    - 数组类型
        - 一维、多维和交错。 例如：int[]、int[,] 和 int[][]
    - 委托类型
        - 格式为 delegate int D(...) 的用户定义类型
        - 
> C# 采用统一的类型系统，因此任意类型的值都可视为 object。 
> 每种 C# 类型都直接或间接地派生自 object 类类型，而 object 是所有类型的最终基类。 
> 只需将值视为类型 object，即可将引用类型的值视为对象。 
> 通过执行 装箱 和 取消装箱操作，可以将值类型的值视为对象。 


### 成员

class 的成员要么是静态成员，要么是实例成员。 静态成员属于类，而实例成员则属于对象（类实例）。
- 常量：与类相关联的常量值
- 字段：与类关联的变量
- 方法：类可执行的操作
- 属性：与读取和写入类的已命名属性相关联的操作
- 索引器：与将类实例编入索引（像处理数组一样）相关联的操作
- 事件：类可以生成的通知
- 运算符：类支持的转换和表达式运算符
- 构造函数：初始化类实例或类本身所需的操作
- 终结器：永久放弃类的实例之前完成的操作
- 类型：类声明的嵌套类型


### 辅助功能

每个类成员都有关联的可访问性，用于控制能够访问成员的程序文本区域。 
- public：访问不受限制。
- private：访问仅限于此类。
- protected：访问仅限于此类或派生自此类的类。
- internal：仅可访问当前程序集（.exe 或 .dll）。
- protected internal：仅可访问此类、从此类中派生的类，或者同一程序集中的类。
- private protected：仅可访问此类或同一程序集中从此类中派生的类。

### 参数
1. 参数用于将值或变量引用传递给方法。 方法参数从调用方法时指定的 自变量 中获取其实际值。 有四类参数：值参数、引用参数、输出参数和参数数组。

2. 值参数用于传递输入自变量。 值参数对应于局部变量，从为其传递的自变量中获取初始值。 修改值形参不会影响为其传递的实参。
可以指定默认值，从而省略相应的自变量，这样值参数就是可选的。

3. 引用参数用于按引用传递自变量。 为引用参数传递的自变量必须是一个带有明确值的变量。 在方法执行期间，引用参数指出的存储位置与自变量相同。 引用参数使用 ref 修饰符进行声明。 下面的示例展示了如何使用 ref 参数。
```c#
static void Swap(ref int x, ref int y)
{
    int temp = x;
    x = y;
    y = temp;
}

public static void SwapExample()
{
    int i = 1, j = 2;
    Swap(ref i, ref j);
    Console.WriteLine($"{i} {j}");    // "2 1"
}
```
4. 输出参数用于按引用传递自变量。 输出参数与引用参数类似，不同之处在于，不要求向调用方提供的自变量显式赋值。 输出参数使用 out 修饰符进行声明。 下面的示例演示如何通过 C# 7 中引入的语法使用 out 参数。
```c#
static void Divide(int x, int y, out int result, out int remainder)
{
    result = x / y;
    remainder = x % y;
}

public static void OutUsage()
{
    Divide(10, 3, out int res, out int rem);
    Console.WriteLine($"{res} {rem}");	// "3 1"
}
```
5. 参数数组 允许向方法传递数量不定的自变量。 参数数组使用 params 修饰符进行声明。 参数数组只能是方法的最后一个参数，且参数数组的类型必须是一维数组类型。 System.Console 类的 Write 和 WriteLine 方法是参数数组用法的典型示例。 它们的声明方式如下。
```c#
public class Console
{
    public static void Write(string fmt, params object[] args) { }
    public static void WriteLine(string fmt, params object[] args) { }
    // ...
}
```

### 虚方法、重写方法和抽象方法

可使用虚方法、重写方法和抽象方法来定义类类型层次结构的行为。 由于类可从基类派生，因此这些派生类可能需要修改在基类中实现的行为。 虚方法是在基类中声明和实现的方法，其中任何派生类都可提供更具体的实现。 重写方法是在派生类中实现的方法，可修改基类实现的行为。 抽象方法是在基类中声明的方法，必须在所有派生类中重写。 事实上，抽象方法不在基类中定义实现。

对实例方法的方法调用可解析为基类或派生类实现。 变量的类型确定了其编译时类型。 编译时类型是编译器用于确定其成员的类型。 但是，可将变量分配给从其编译时类型派生的任何类型的实例。 运行时间类型是变量所引用的实际实例的类型。

调用虚方法时，为其调用方法的实例的 运行时类型 决定了要调用的实际方法实现代码。 调用非虚方法时，实例的 编译时类型 是决定性因素。

可以在派生类中 重写 虚方法。 如果实例方法声明中有 override 修饰符，那么实例方法可以重写签名相同的继承虚方法。 虚方法声明引入了新方法。 重写方法声明通过提供现有继承的虚方法的新实现，专门针对该方法。

抽象方法 是没有实现代码的虚方法。 抽象方法使用 abstract 修饰符进行声明，仅可在抽象类中使用。 必须在所有非抽象派生类中重写抽象方法。

下面的示例声明了一个抽象类 Expression，用于表示表达式树节点；还声明了三个派生类（Constant、VariableReference 和 Operation），用于实现常量、变量引用和算术运算的表达式树节点。 （该示例与表达式树类型相似，但与它无关）。

```c#
public abstract class Expression
{
    public abstract double Evaluate(Dictionary<string, object> vars);
}

public class Constant : Expression
{
    double _value;
    
    public Constant(double value)
    {
        _value = value;
    }
    
    public override double Evaluate(Dictionary<string, object> vars)
    {
        return _value;
    }
}

public class VariableReference : Expression
{
    string _name;
    
    public VariableReference(string name)
    {
        _name = name;
    }
    
    public override double Evaluate(Dictionary<string, object> vars)
    {
        object value = vars[_name] ?? throw new Exception($"Unknown variable: {_name}");
        return Convert.ToDouble(value);
    }
}

public class Operation : Expression
{
    Expression _left;
    char _op;
    Expression _right;
    
    public Operation(Expression left, char op, Expression right)
    {
        _left = left;
        _op = op;
        _right = right;
    }
    
    public override double Evaluate(Dictionary<string, object> vars)
    {
        double x = _left.Evaluate(vars);
        double y = _right.Evaluate(vars);
        switch (_op)
        {
            case '+': return x + y;
            case '-': return x - y;
            case '*': return x * y;
            case '/': return x / y;
            
            default: throw new Exception("Unknown operator");
        }
    }
}
```

### 方法重载
借助方法 重载，同一类中可以有多个同名的方法，只要这些方法具有唯一签名即可。 编译如何调用重载的方法时，编译器使用 重载决策 来确定要调用的特定方法。 重载决策会查找与自变量匹配度最高的一种方法。 如果找不到任何最佳匹配项，则会报告错误。 下面的示例展示了重载决策的实际工作方式。 UsageExample 方法中每个调用的注释指明了调用的方法。

```c#
class OverloadingExample
{
    static void F() => Console.WriteLine("F()");
    static void F(object x) => Console.WriteLine("F(object)");
    static void F(int x) => Console.WriteLine("F(int)");
    static void F(double x) => Console.WriteLine("F(double)");
    static void F<T>(T x) => Console.WriteLine("F<T>(T)");            
    static void F(double x, double y) => Console.WriteLine("F(double, double)");
    
    public static void UsageExample()
    {
        F();            // Invokes F()
        F(1);           // Invokes F(int)
        F(1.0);         // Invokes F(double)
        F("abc");       // Invokes F<string>(string)
        F((double)1);   // Invokes F(double)
        F((object)1);   // Invokes F(object)
        F<int>(1);      // Invokes F<int>(int)
        F(1, 1);        // Invokes F(double, double)
    }
}
```

> 1.方法重载：同一个类下，同名方法，参数类型或个数不同,与方法返回值无关。   
> 2.方法重写：在派生类中，修改基类方法（虚方法，抽象方法或者重写方法），与基类方法名，参数，返回值相同，内部实现不同。

### 构造函数

C# 支持实例和静态构造函数。 实例构造函数 是实现初始化类实例所需执行的操作的成员。 静态构造函数是实现在首次加载类时初始化类本身所需执行的操作的成员。

构造函数的声明方式与方法一样，都没有返回类型，且与所含类同名。 如果构造函数声明包含 static 修饰符，则声明的是静态构造函数。 否则，声明的是实例构造函数。


### 属性
属性 是字段的自然扩展。 两者都是包含关联类型的已命名成员，用于访问字段和属性的语法也是一样的。 不过，与字段不同的是，属性不指明存储位置。 相反，属性包含访问器，用于指定在读取或写入属性值时执行的语句。 get 访问器读取该值。 set 访问器写入该值。

属性的声明方式与字段相似，区别是属性声明以在分隔符 { 和 } 之间写入的 get 访问器或 set 访问器结束，而不是以分号结束。 同时具有 get 访问器和 set 访问器的属性是“读写属性”。 只有 get 访问器的属性是“只读属性”。 只有 set 访问器的属性是“只写属性”。

get 访问器对应于包含属性类型的返回值的无参数方法。 set 访问器对应于包含一个名为 value 的参数但不含返回类型的方法。 get 访问器会计算属性的值。 set 访问器会为属性提供新值。 当属性是赋值的目标，或者是 ++ 或 -- 的操作数时，会调用 set 访问器。 在引用了属性的其他情况下，会调用 get 访问器。

类似于字段和方法，C# 支持实例属性和静态属性。 静态属性使用静态修饰符进行声明，而实例属性则不使用静态修饰符进行声明。

属性的访问器可以是虚的。 如果属性声明包含 virtual、abstract 或 override 修饰符，则适用于属性的访问器。

## 记录 record
C# 中的记录是一个类或结构，它为使用数据模型提供特定的语法和行为。
### 何时使用记录
在下列情况下，请考虑使用记录而不是类或结构：

1. 你想要定义依赖值相等性的数据模型。
2. 你想要定义对象不可变的类型。

### 值相等性
对记录来说，值相等性表示当类型匹配且所有属性和字段值都匹配时，记录类型的两个变量相等。 对于其他引用类型（例如类），相等性是指引用相等性。 也就是说，如果类的两个变量引用同一个对象，则这两个变量是相等的。 确定两个记录实例的相等性的方法和运算符使用值相等性。

并非所有数据模型都适合使用值相等性。 例如，Entity Framework Core 依赖引用相等性，来确保它对概念上是一个实体的实体类型只使用一个实例。 因此，记录类型不适合用作 Entity Framework Core 中的实体类型。

### 不可变性
不可变类型会阻止你在对象实例化后更改该对象的任何属性或字段值。 如果你需要一个类型是线程安全的，或者需要哈希代码在哈希表中国能保持不变，那么不可变性很有用。 记录为创建和使用不可变类型提供了简洁的语法。

不可变性并不适用于所有数据方案。 例如，Entity Framework Core 不支持通过不可变实体类型进行更新。

### 记录与类和结构的区别
声明和实例化类或结构时使用的语法与操作记录时的相同。 只是将 class 关键字替换为 record，或者使用 record struct 而不是 struct。 同样地，记录类支持相同的表示继承关系的语法。 记录与类的区别如下所示：

可使用位置参数创建和实例化具有不可变属性的类型。
在类中指示引用相等性或不相等的方法和运算符（例如 Object.Equals(Object) 和 ==）在记录中指示值相等性或不相等。
可使用 with 表达式对不可变对象创建在所选属性中具有新值的副本。
记录的 ToString 方法会创建一个格式字符串，它显示对象的类型名称及其所有公共属性的名称和值。
记录可从另一个记录继承。 但记录不可从类继承，类也不可从记录继承。
记录结构与结构的不同之处是，编译器合成了方法来确定相等性和 ToString。 编译器为位置记录结构合成 Deconstruct 方法。

### 示例
下面的示例定义了一个公共记录，它使用位置参数来声明和实例化记录。 然后，它会输出类型名称和属性值：
```C#
public record Person(string FirstName, string LastName);

public static void Main()
{
    Person person = new("Nancy", "Davolio");
    Console.WriteLine(person);
    // output: Person { FirstName = Nancy, LastName = Davolio }
}
```
下面的示例演示了记录中的值相等性：
```C#
public record Person(string FirstName, string LastName, string[] PhoneNumbers);

public static void Main()
{
    var phoneNumbers = new string[2];
    Person person1 = new("Nancy", "Davolio", phoneNumbers);
    Person person2 = new("Nancy", "Davolio", phoneNumbers);
    Console.WriteLine(person1 == person2); // output: True

    person1.PhoneNumbers[0] = "555-1234";
    Console.WriteLine(person1 == person2); // output: True

    Console.WriteLine(ReferenceEquals(person1, person2)); // output: False
}
```
下面的示例演示如何使用 with 表达式来复制不可变对象和更改其中的一个属性：
```C#
public record Person(string FirstName, string LastName)
{
    public string[] PhoneNumbers { get; init; }
}

public static void Main()
{
    Person person1 = new("Nancy", "Davolio") { PhoneNumbers = new string[1] };
    Console.WriteLine(person1);
    // output: Person { FirstName = Nancy, LastName = Davolio, PhoneNumbers = System.String[] }

    Person person2 = person1 with { FirstName = "John" };
    Console.WriteLine(person2);
    // output: Person { FirstName = John, LastName = Davolio, PhoneNumbers = System.String[] }
    Console.WriteLine(person1 == person2); // output: False

    person2 = person1 with { PhoneNumbers = new string[1] };
    Console.WriteLine(person2);
    // output: Person { FirstName = Nancy, LastName = Davolio, PhoneNumbers = System.String[] }
    Console.WriteLine(person1 == person2); // output: False

    person2 = person1 with { };
    Console.WriteLine(person1 == person2); // output: True
}
```

## 模式匹配
“模式匹配”是一种测试表达式是否具有特定特征的方法。 C# 模式匹配提供更简洁的语法，用于测试表达式并在表达式匹配时采取措施。 “is 表达式”目前支持通过模式匹配测试表达式并有条件地声明该表达式结果。 “switch 表达式”允许你根据表达式的首次匹配模式执行操作。 这两个表达式支持丰富的模式词汇。


### Null 检查
模式匹配最常见的方案之一是确保值不是 null。 使用以下示例进行 null 测试时，可以测试可为 null 的值类型并将其转换为其基础类型：

```C#
int? maybe = 12;

if (maybe is int number)
{
    Console.WriteLine($"The nullable int 'maybe' has the value {number}");
}
else
{
    Console.WriteLine("The nullable int 'maybe' doesn't hold a value");
}
```
上述代码是声明模式，用于测试变量类型并将其分配给新变量。 语言规则使此方法比其他方法更安全。 变量 number 仅在 if 子句的 true 部分可供访问和分配。 如果尝试在 else 子句或 if 程序块后等其他位置访问，编译器将出错。 其次，由于不使用 == 运算符，因此当类型重载 == 运算符时，此模式有效。 这使该方法成为检查空引用值的理想方法，可以添加 not 模式：

```C#
string? message = "This is not the null string";

if (message is not null)
{
    Console.WriteLine(message);
}
```
前面的示例使用常数模式将变量与 null 进行比较。 not 为一种逻辑模式，在否定模式不匹配时与该模式匹配。

### 类型测试
模式匹配的另一种常见用途是测试变量是否与给定类型匹配。 例如，以下代码测试变量是否为非 null 并实现 System.Collections.Generic.IList<T> 接口。 如果是，它将使用该列表中的 ICollection<T>.Count 属性来查找中间索引。 不管变量的编译时类型如何，声明模式均与 null 值不匹配。 除了防范未实现 IList 的类型之外，以下代码还可防范 null。

```C#
public static T MidPoint<T>(IEnumerable<T> sequence)
{
    if (sequence is IList<T> list)
    {
        return list[list.Count / 2];
    }
    else if (sequence is null)
    {
        throw new ArgumentNullException(nameof(sequence), "Sequence can't be null.");
    }
    else
    {
        int halfLength = sequence.Count() / 2 - 1;
        if (halfLength < 0) halfLength = 0;
        return sequence.Skip(halfLength).First();
    }
}
```
可在 switch 表达式中应用相同测试，用以测试多种不同类型的变量。 你可以根据特定运行时类型使用这些信息创建更好的算法。

### 比较离散值
你还可以通过测试变量找到特定值的匹配项。 在以下代码演示的示例中，你针对枚举中声明的所有可能值进行数值测试：

```C#
public State PerformOperation(Operation command) =>
   command switch
   {
       Operation.SystemTest => RunDiagnostics(),
       Operation.Start => StartSystem(),
       Operation.Stop => StopSystem(),
       Operation.Reset => ResetToReady(),
       _ => throw new ArgumentException("Invalid enum value for command", nameof(command)),
   };
```
前一个示例演示了基于枚举值的方法调度。 最终 _ 案例为与所有数值匹配的弃元模式。 它处理值与定义的 enum 值之一不匹配的任何错误条件。 如果省略开关臂，编译器会警告你尚未处理所有可能输入值。 在运行时，如果检查的对象与任何开关臂均不匹配，则 switch 表达式会引发异常。 可以使用数值常量代替枚举值集。 你还可以将这种类似的方法用于表示命令的常量字符串值：

```C#
public State PerformOperation(string command) =>
   command switch
   {
       "SystemTest" => RunDiagnostics(),
       "Start" => StartSystem(),
       "Stop" => StopSystem(),
       "Reset" => ResetToReady(),
       _ => throw new ArgumentException("Invalid string value for command", nameof(command)),
   };
```
前面的示例显示相同的算法，但使用字符串值代替枚举。 如果应用程序响应文本命令而不是常规数据格式，则可以使用此方案。 从 C# 11 开始，还可以使用 Span<char> 或 ReadOnlySpan<char> 来测试常量字符串值，如以下示例所示：

```C#
public State PerformOperation(ReadOnlySpan<char> command) =>
   command switch
   {
       "SystemTest" => RunDiagnostics(),
       "Start" => StartSystem(),
       "Stop" => StopSystem(),
       "Reset" => ResetToReady(),
       _ => throw new ArgumentException("Invalid string value for command", nameof(command)),
   };
```
在所有这些示例中，“弃元模式”可确保处理每个输入。 编译器可确保处理每个可能的输入值，为你提供帮助。

### 关系模式
你可以使用关系模式测试如何将数值与常量进行比较。 例如，以下代码基于华氏温度返回水源状态：

```C#
string WaterState(int tempInFahrenheit) =>
    tempInFahrenheit switch
    {
        (> 32) and (< 212) => "liquid",
        < 32 => "solid",
        > 212 => "gas",
        32 => "solid/liquid transition",
        212 => "liquid / gas transition",
    };
```
上述代码还演示了联合 and逻辑模式，用于检查两种关系模式是否匹配。 你还可以使用析取 or 模式检查模式匹配。 这两种关系模式括在括号中，可以在任何模式下用于清晰表述。 最后两个开关臂用于处理熔点和沸点的案例。 如果没有这两个开关臂，编译器将警告你的逻辑未涵盖每个可能的输入。

上述代码还说明了编译器为模式匹配表达式提供的另一项重要功能：如果没有处理每个输入值，编译器会发出警告。 如果交换机 arm 已由先前的交换机 arm 处理，则编译器还会发出警告。 这使你能够随意重构和重新排列 switch 表达式。 编写同一表达式的另一种方法是：

```C#
string WaterState2(int tempInFahrenheit) =>
    tempInFahrenheit switch
    {
        < 32 => "solid",
        32 => "solid/liquid transition",
        < 212 => "liquid",
        212 => "liquid / gas transition",
        _ => "gas",
};
```
关于这一点和任何其他重构或重新排列的关键注意事项是，编译器会验证你已涵盖所有输入。

### 多个输入
到目前为止，你所看到的所有模式都在检查一个输入。 可以写入检查一个对象的多个属性的模式。 请考虑以下 Order 记录：

```C#
public record Order(int Items, decimal Cost);
```
前面的位置记录类型在显式位置声明两个成员。 首先出现 Items，然后是订单的 Cost。 有关详细信息，请参阅记录。

以下代码检查项数和订单值以计算折扣价：

```C#
public decimal CalculateDiscount(Order order) =>
    order switch
    {
        { Items: > 10, Cost: > 1000.00m } => 0.10m,
        { Items: > 5, Cost: > 500.00m } => 0.05m,
        { Cost: > 250.00m } => 0.02m,
        null => throw new ArgumentNullException(nameof(order), "Can't calculate discount on null order"),
        var someObject => 0m,
    };
```
前两个开关臂检查 Order 的两个属性。 第三个仅检查成本。 下一个检查 null，最后一个与其他任何值匹配。 如果 Order 类型定义了适当的 Deconstruct 方法，则可以省略模式的属性名称，并使用析构检查属性：

```C#
public decimal CalculateDiscount(Order order) =>
    order switch
    {
        ( > 10,  > 1000.00m) => 0.10m,
        ( > 5, > 50.00m) => 0.05m,
        { Cost: > 250.00m } => 0.02m,
        null => throw new ArgumentNullException(nameof(order), "Can't calculate discount on null order"),
        var someObject => 0m,
    };
```
上述代码演示了位置模式，其中表达式的属性已析构。

### 列表模式
可以使用列表模式检查列表或数组中的元素。 列表模式提供了一种方法，将模式应用于序列的任何元素。 此外，还可以应用弃元模式 (_) 来匹配任何元素，或者应用切片模式来匹配零个或多个元素。

当数据不遵循常规结构时，列表模式是一个有价值的工具。 可以使用模式匹配来测试数据的形状和值，而不是将其转换为一组对象。

看看下面的内容，它摘录自一个包含银行交易信息的文本文件：
```
04-01-2020, DEPOSIT,    Initial deposit,            2250.00
04-15-2020, DEPOSIT,    Refund,                      125.65
04-18-2020, DEPOSIT,    Paycheck,                    825.65
04-22-2020, WITHDRAWAL, Debit,           Groceries,  255.73
05-01-2020, WITHDRAWAL, #1102,           Rent, apt, 2100.00
05-02-2020, INTEREST,                                  0.65
05-07-2020, WITHDRAWAL, Debit,           Movies,      12.57
04-15-2020, FEE,                                       5.55
```
它是 CSV 格式，但某些行的列数比其他行要多。 对处理来说更糟糕的是，WITHDRAWAL 类型中的一列具有用户生成的文本，并且可以在文本中包含逗号。 一个包含弃元模式、常量模式和 var 模式的列表模式用于捕获这种格式的值处理数据：

```C#
decimal balance = 0m;
foreach (string[] transaction in ReadRecords())
{
    balance += transaction switch
    {
        [_, "DEPOSIT", _, var amount]     => decimal.Parse(amount),
        [_, "WITHDRAWAL", .., var amount] => -decimal.Parse(amount),
        [_, "INTEREST", var amount]       => decimal.Parse(amount),
        [_, "FEE", var fee]               => -decimal.Parse(fee),
        _                                 => throw new InvalidOperationException($"Record {string.Join(", ", transaction)} is not in the expected format!"),
    };
    Console.WriteLine($"Record: {string.Join(", ", transaction)}, New balance: {balance:C}");
}
```
前面的示例采用了字符串数组，其中每个元素都是行中的一个字段。 第二个字段的 switch 表达式键，用于确定交易的类型和剩余列数。 每一行都确保数据的格式正确。 弃元模式 (_) 跳过第一个字段，以及交易的日期。 第二个字段与交易的类型匹配。 其余元素匹配跳过包含金额的字段。 最终匹配使用 var 模式来捕获金额的字符串表示形式。 表达式计算要从余额中加上或减去的金额。

列表模式可以在数据元素序列的形状上进行匹配。 使用弃元模式和切片模式来匹配元素的位置。 使用其他模式来匹配各个元素的特征。
