# OpenSignal is a C# Signal Library

OpenSignal is inspired by [Robert Penner's Signals for ActionScript3](https://github.com/robertpenner/as3-signals/ "as3 signals").
## Simple Example
```csharp
Signal<Action<string, int>> s1 = new Signal<Action<string, int>>();
Signal<Action> s2 = new Signal<Action>();

s1.AddAction(MyMethod1);
s1.AddOnceAction(MyMethod2);
Console.WriteLine("NumListeners s1 : " + s1.NumListeners); //NumListeners s1 : 2

s1.Dispatch("hello signal", 1);
Console.WriteLine("NumListeners s1 : " + s1.NumListeners); //NumListeners s1 : 1

s1.RemoveAction(MyMethod1);
Console.WriteLine("NumListeners s1 : " + s1.NumListeners); //NumListeners s1 : 0

s2.AddAction(MyMethod3);
s2.Dispatch();



```

```csharp
private void MyMethod1(string s, int i)
{
	Console.WriteLine("MyMethod1: {0}, {1}", s, i);	
}

private void MyMethod2(string s, int i)
{
	Console.WriteLine("MyMethod2: {0}, {1}", s, i);	
}

private void MyMethod3()
{
	Console.WriteLine("MyMethod3");	
} 
```