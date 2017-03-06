using System;
using System.Collections.Generic;
using Option;

namespace Option
{
    public class None
    {
         internal static None Instance = new None();

        private None() { }
    }
}

public partial class Prelude
{
    /// <summary>
    /// Convert from a Nullable<T> to Option<T>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Option<T> Some<T>(T? value) where T : struct => value.HasValue ? Some(value.Value) : None;

    // TODO: Should we throw an exception when `null` is passed to Some() ?
    /// <summary>
    /// Create a Option<T> with some value.
    /// </summary>
    /// <typeparam name="T">The type of option</typeparam>
    /// <param name="value">The value - if value is null a `None` will be returned</param>
    /// <returns></returns>
    public static Option<T> Some<T>(T value) => value == null ? None : new Option<T>(value);

    /// <summary>
    /// A convenience property to return None without explicitly specifying tyhe type.
    /// </summary>
    /// <example>
    /// Option<int> Positive(int x) => 
    ///     x > 0 : Some(x) : None;
    /// </example>
    public static None None => None.Instance;

    public static Option<TValue> Get<TKey, TValue>(this IDictionary<TKey, TValue> map, TKey key) =>
        map.ContainsKey(key) ? Some(map[key]) : None;

    /// <summary>
    /// An Option is a monad that either has a value of T, or no value (None). Options compose better than null.
    /// </summary>
    public struct Option<T>
    {
        internal static readonly Option<T> none = new Option<T>();

        private readonly bool some;
        private readonly T value;

        internal Option(T value)
        {
            this.value = value;
            some = true;
        }

        public Option<TResult> Select<TResult>(Func<T, TResult> selector) =>
            some ? new Option<TResult>(selector(value)) : Option<TResult>.none;
        public Option<TResult> SelectMany<TResult>(Func<T, Option<TResult>> selector) =>
            some ? selector(value) : Option<TResult>.none;
        public Option<TResult> SelectMany<TCollection, TResult>(Func<T, Option<TCollection>> collectionSelector, Func<T, TCollection, TResult> resultSelector)
        {
            var v = value;
            return some ? collectionSelector(value).Select(x => resultSelector(v, x)) : Option<TResult>.none;
        }
        public Option<T> Where(Func<T, bool> predicate) => some && predicate(value) ? this : none;

        public Option<T> OrElse(Option<T> other) => some ? this : other;
        public T GetOrElse(Func<T> other) => some ? value : other();
        public T GetOrElse(T other) => some ? value : other;
        public T Get()
        {
            if (some) return value;
            throw new NotSupportedException();
        }
        public bool Any() => some;

        public static implicit operator Option<T>(T value) => new Option<T>(value);
        public static implicit operator Option<T>(None _) => none;

        public static Option<T> operator |(Option<T> lhs, Option<T> rhs) => lhs.OrElse(rhs);
        public static T operator |(Option<T> lhs, T rhs) => lhs.GetOrElse(rhs);

        public static bool operator ==(Option<T> lhs, None rhs) => !lhs.some;
        public static bool operator !=(Option<T> lhs, None rhs) => lhs.some;

        public override bool Equals(object obj)
        {
            if (obj is None) return !some;
            if (obj is Option<T>)
            {
                var option = (Option<T>)obj;
                return some == option.some && (!some || (some && value.Equals(option.value)));
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
