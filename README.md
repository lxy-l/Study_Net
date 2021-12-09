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
