using System;

public struct Try<T> 
{
    private readonly bool failure; 
    private readonly T value;
    private readonly Exception ex;

    internal Try(T value) 
    {
        this.value = value;
        failure = false;
        ex = null;
    }
    internal Try(Exception ex) 
    {
        this.ex = ex;
        failure = true;
        value = default(T);
    }
    
    public bool IsSuccess => !failure;
    public bool IsFailure => failure;

    public T Get() 
    {
        if(failure) throw new InvalidOperationException();
        return value;
    }

    public Try<TResult> Select<TResult>(Func<T, TResult> selector) => 
        failure ? new Try<TResult>(ex) : selector(value);
    public Try<TResult> SelectMany<TCollection, TResult>(Func<T, Try<TCollection>> collectionSelector, Func<T, TCollection, TResult> resultSelector)
    {
        var x = collectionSelector(value);
        return failure || x.failure ? new Try<TResult>(ex) : resultSelector(value, x.Get());
    }


    public static implicit operator Try<T>(T value) => new Try<T>(value);
    public static implicit operator Try<T>(Exception ex) => new Try<T>(ex);
}

public static partial class Prelude 
{
    public static Try<T> Try<T>(Func<T> f) 
    {
        try { return f(); }
        catch(Exception ex) { return ex; }
    }
}