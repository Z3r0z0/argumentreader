[![Version: 1.0 Release](https://img.shields.io/badge/Version-1.0%20Release-green.svg)](https://github.com/Z3r0z0) [![Build Status](https://travis-ci.org/Z3r0z0/argumentreader.svg?branch=master)](https://travis-ci.org/Z3r0z0/argumentreader)
 [![License: GPL v3](https://img.shields.io/badge/License-GPL%20v3-blue.svg)](https://www.gnu.org/licenses/gpl-3.0)

# Argument Reader
---

### Description:

Whit Argument reader command line arguments can be passed into a .net core
application. The standart project assists 4 types of arguments:

* Boolean
* String *(\*)*
* integer *(\#)*
* Doubles *(\##)*

Own argument types can be build with onw classes. They need to be inherit from the 
**ArgumentMarshalerLib**. Libraries are loading dynamically on startup. There is not necessary to recompile the complete solution.

---

## Structure

``` csharp
static void Main(stirng[] args)
{
    Argument arg = new Argument("Path to Marshalers Libraries", "Schema", "Argument Array");
}
```

### Available Marshalers

* BooleanmarshalerLib.dll
* StringMarshalerLib.dll
* IntegerMarshalerLib.dll
* DoubleMarshalerLib.dll

### Schema

1. Parameter name
1. Marshaler type

**Example**

``` csharp
static void Main(stirng[] args)
{
    Argument arg = new Argument("...", "enabled,text*,number#,decimal##", "...");
}
```

---

#### Boolean

``` bash
 Arguments.exe -a
```

``` csharp
static void Main(stirng[] args)
{
    Argument arg = new Argument(@".\Marshaler", "a,b", args);
    bool a = parameter.GetValue<bool>("a"); // True
    bool b = parameter.GetValue<bool>("b"); // False
    // ...
}
```

#### String

``` bash
 Arguments.exe -a "This is a Text"
```

``` csharp
static void Main(stirng[] args)
{
    Argument arg = new Argument(@".\Marshaler", "a*", args);
    string a = parameter.GetValue<string>("a"); // This is a Text
    // ...
}
```

#### Integer

``` bash
 Arguments.exe -a 1234
```

``` csharp
static void Main(stirng[] args)
{
    Argument arg = new Argument(@".\Marshaler", "a#", args);
    int a = parameter.GetValue<int>("a"); // 1234
    // ...
}
```

#### Double

``` bash
 Arguments.exe -a 1234,4321
```

``` csharp
static void Main(stirng[] args)
{
    Argument arg = new Argument(@".\Marshaler", "a##", args);
    double a = parameter.GetValue<double>("a"); // 1234,4321
    // ...
}
```

---

## Build your own Marshaler

1. Create o new VisualStudio .NET Standard Classlibrary (**??Marshaler??**)
2. Link the new prject reference to ArgumentMashalerLib.dll (in this repository)
3. Writer Marshaler (See example code)
4. Copy the TestmarshalerLib.dll to the Marshaler director in your project
5. Implement the *?* in your schema (e.g. "mymarshaler?")

``` csharp
using System;
using ArgumentMarshalerLib;

namespace TestMarshalerLib
{
    public class TestmarshalerLib : ArgumentMarshalerLib.ArgumentMarshaler
    {
        // Only Schemas allowed witch are not uesd (string.Empt, *, #, ## are already used from the standard marshalers)
        public override string Schema => "?";

        public override void Set(Iterator<string> currentArgument)
        {
            try
            {
                // If implementation should be using an argument behind the command (e.g. -a "??")
                // it is necessary to move the iterator to the next position.
                Value = currentArgument.Next();
                // If no argument behinde the command is used just add your value
            }
            catch
            {
                throw new TestMarshalerException(ErrorCode.MISSING);
            }

            // if no argument behind the command is used just add your value
            Value = "This is my personal marshaler";
        }

        public class TestMarshalerException : ArgumentMarshalerLib.ArgumentException
        {
            public TestMarshalerException(ErrorCode errorCode) : base(errorCode)
            {

            }

            public override string ErrorMessage()
            {
                switch (ErrorCode)
                {
                    case ErrorCode.MISSING:
                        return $"Could not find test parameter for -{ErrorArgumentId}";
                    default:
                        return string.Empty;
                }
            }
        }
    }
}
```

---

## References

The orginal Argument Marshaler was written in Java by Robert C. Martin in his book Clean Code. This project adapt his implemenatatons and extends it dynamically.

