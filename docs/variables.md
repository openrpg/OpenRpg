# Variables

A large part of the system is based on the notion of `IVariable<T>` this acts as a sort of fancy dictionary which lets you store various data off keyed values.

## Why?
That is a good question, and the main reason is for extensibility, so for example if we used properties/fields for values then it would be hard to have a re-used and built upon in higher layers.

> For example with the notion of `Stats` we can agree in most games you would have a `HP` value or something, but do you have a `Strength` value or an `Attack` value, or even both? Once you go down that path of explicitly giving every value a property it becomes a lot harder to provide a basic framework that anyone can build on top of.

So using this `IVariables` notion allows us to have a data container for any possible value that could be needed, and instead of having the values on the containing class, we just combine enum/static lookup values with extension method.

For example if you wanted to add the notion of `HP` to a variable object you would do:
```csharp
// StatLookups.cs
public static int Health = 1;

// StatExtensions.cs
public static int Health(this IStats stats) => stats[StatLookups.Health];
public static int Health(this IStats stats, int value) => stats[StatLookups.Health] = value;
```

> `IStats` just implements `IVariables<float>` here, its always best to try and have an interface over the `IVariable<T>` you want so you can more easily target that with extension methods rather than having ALL `IVariable<T>` implementations from having your extension methods applied.

There is a bit more complexity around making it safer, and if you look in the source for some of the existing types you can see how you can use methods on the `IVariables` to guard for un-initialized values, but at the heart of it, this way you can make conventions which provide these extensions for downstream consumers, which is exactly what the `OpenRpg.Genres` repository does, providing out the box `Fantasy` and `SciFi` settings (as well as some other repos).

## Can I Use Properties Anyway?

Yeah sure, the only reason we do this in the library is for extensibility purposes, if you are making a game or app then you will probably not be exposing your logic for anyone to build on top of, so do what makes sense for you. However if you were to be making a re-usable convention/layer for others to use then using the variables approach listed above makes it easier for people to just bolt on your conventions on top of their existing objects (assuming lookup ids dont clash).

> Direct fields/properties will be quicker to get/set etc than using the lookup approach, so if you are doing LOTS of lookups and you wish to make it faster it may be worth creating a caching layer on your top level objects which just takes the important data you need to access frequently, and just recache it after any variable change events are fired that you care about.