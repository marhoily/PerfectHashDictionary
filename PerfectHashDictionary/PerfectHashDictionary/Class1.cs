using System;
using System.Collections.Generic;
using System.Linq;

namespace PerfectHashDictionary
{
    public sealed class OptimizedDictionary<TKey, TValue>
    {
        private readonly TValue[] _data;
        private readonly int _perfectMod;

        public OptimizedDictionary(IDictionary<TKey, TValue> inner)
        {
            var hashCodes = inner.Keys
                .Select(k => (uint)k.GetHashCode())
                .ToArray();
            _perfectMod = Enumerable
                .Range(hashCodes.Length, hashCodes.Length*10)
                .FirstOrDefault(i => Try(hashCodes, i, new bool[i]));
            if (_perfectMod == 0)
                throw new Exception("Perfect is not found");
            _data = new TValue[_perfectMod];
            foreach (var kv in inner)
            {
                var hashCode = (uint)kv.Key.GetHashCode();
                var index0 = hashCode%_perfectMod;
                _data[index0] = kv.Value;
            }
        }

        private static bool Try(uint[] hashCodes, int i, bool[] buf)
        {
            for (int j = 0; j < hashCodes.Length; j++)
            {
                var code = hashCodes[j] % i;
                if (buf[code])
                    return false;
                buf[code] = true;
            }
            return true;
        }

        public TValue this[TKey key]
        {
            get
            {
                var hashCode = (uint) key.GetHashCode();
                var index0 = hashCode%_perfectMod;
                return _data[index0];
            }
        }
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
