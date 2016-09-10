using System.Collections.Generic;

namespace PerfectHashDictionary
{
    public sealed class OptimizedDictionary<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> _inner;

        public OptimizedDictionary(IDictionary<TKey, TValue> inner)
        {
            _inner = inner;
        }

        public TValue this[TKey key] => _inner[key];
    }
    public static class Ext
    {
        public static OptimizedDictionary<TKey, TValue> 
            Optimize<TKey, TValue>(this IDictionary<TKey, TValue> source)
        {
            return new OptimizedDictionary<TKey, TValue>(source);
        }
    }
}
