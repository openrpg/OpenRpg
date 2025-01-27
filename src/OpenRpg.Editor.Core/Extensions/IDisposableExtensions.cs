using System;
using System.Collections.Generic;
using System.Reactive.Disposables;

namespace OpenRpg.Editor.Core.Extensions;

public static class IDisposableExtensions
{
    public static void AddTo(this IDisposable disposable, CompositeDisposable compositeDisposable)
    { compositeDisposable.Add(disposable); }
    
    public static void DisposeAll(this IEnumerable<IDisposable> disposables)
    {
        foreach(var disposable in disposables)
        { disposable.Dispose(); }
    }

    public static void DisposeAll<T>(this IDictionary<T, IDisposable> disposables)
    {
        foreach(var disposable in disposables.Values)
        { disposable.Dispose(); }
    }
    
    public static void DisposeAll<T>(this IDictionary<T, CompositeDisposable> disposables)
    {
        foreach(var disposable in disposables.Values)
        { disposable.Dispose(); }
    }
        
    public static IDisposable AddTo(this IDisposable currentDisposable, ICollection<IDisposable> disposables)
    {
        disposables.Add(currentDisposable);
        return currentDisposable;
    }
        
    public static IDisposable AddTo<T>(this IDisposable currentDisposable, IDictionary<T, IDisposable> disposables, T key)
    {
        disposables.Add(key, currentDisposable);
        return currentDisposable;
    }
}