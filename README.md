# NET Reactor String Cleaner 6.7.X.X (Use Cflow remover first)  

<p align="center">
  <img src="Images/img.png" />
</p>

## What is .NET Reactor?

.NET Reactor is a powerful code protection and software licensing system for software written for the .NET Framework, and supports all languages that generate .NET assemblies. 

## What is String Encryption?

String encryption makes it difficult for a hacker to understand your code and to attempt a code patch of your assembly, as he will be unable to identify the text of messages or other useful strings, making it much more difficult to identify where to patch your code. This feature has a built-in protection against assembly manipulation.

- Control Flow Remover for .NET Reactor 6.7.0.0: [Click me!](https://github.com/Hussaryn/NET-Reactor-Cflow-Cleaner-6.7.0.0)

---

## Showcase

> Before 

```C# 
public static void Main(string[] args)
{
	Console.WriteLine(rxJ1soFU9jN03iRO2i.xQI1in9X2(0));
	Console.ReadKey();
}
```
> After
```C#
public static void Main(string[] args)
{
    Console.WriteLine("Hello, type anything.");
    Console.ReadKey();
}
```

---



## Credits

- GitHub - [HoLLy-HaCKeR](https://github.com/HoLLy-HaCKeR) for StackTrace Harmony Patcher

<br>

