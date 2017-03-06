using System.Collections.Generic;
using System.Linq;


public static partial class Prelude
{    
    public static List<T> List<T>(params T[] items) => 
        new List<T>(items);

    public static Dictionary<TKey, TValue> Map<TKey, TValue>(params KeyValuePair<TKey, TValue>[] items) => 
        items.ToDictionary(x => x.Key, x => x.Value);

    public static KeyValuePair<TKey, TValue> Pair<TKey, TValue>(TKey key, TValue value) => 
        new KeyValuePair<TKey, TValue>(key, value);


}
