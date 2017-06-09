namespace System.Threading.Tasks
{
    public static class TaskMonad
    {
        public static async Task<TResult> Select<TResult>(this Task task, Func<TResult> selector) 
        {
            await task;
            return selector();
        }

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

        public static async Task<T> RecoverWith<T>(this Task<T> task, Func<Exception, T> recover) 
        {
            try 
            {
                return await task;
            }
            catch(Exception ex) 
            {
                return recover(ex);
            }
        }
    }

    class Completed<T>
    {
        public static Task<T> Instance { get; } = Task<T>.FromResult(default(T));

    }
}

public partial class Prelude
{
    public static System.Threading.Tasks.Task<T> Completed<T>() => System.Threading.Tasks.Completed<T>.Instance;
    public static System.Threading.Tasks.Task Completed() => System.Threading.Tasks.Completed<int>.Instance;
}
