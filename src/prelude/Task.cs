namespace System.Threading.Tasks
{
    public static class TaskMonad
    {
        public static async Task<TResult> Select<TSource, TResult>(this Task<TSource> task, Func<TSource, TResult> selector)
        {
            var x = await task.ConfigureAwait(false);
            return selector(x);
        }

        public static async Task<TResult> SelectMany<TSource, TCollection, TResult>(this Task<TSource> task, Func<TSource, Task<TCollection>> selector, Func<TSource, TCollection, TResult> projection)
        {
            var x = await task.ConfigureAwait(false);
            var y = await selector(x).ConfigureAwait(false);
            return projection(x, y);
        }
    }

    public class Completed<T>
    {
        public static Task<T> Instance { get; } = Task<T>.FromResult(default(T));

    }
}

public partial class Prelude
{
    public static System.Threading.Tasks.Task<T> Completed<T>() => System.Threading.Tasks.Completed<T>.Instance;
}
