using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using PerfectHashDictionary;

namespace Benchmark
{
    class Program
    {
        static void Main()
        {
            BenchmarkRunner.Run<Items30>();

        }
    }

    public class Items30
    {
        private readonly object[] _source;
        private readonly OptimizedDictionary<object, int> _optimized;

        public Items30()
        {
            _source = Enumerable.Range(0, 30)
                .Select(_ => new object())
                .ToArray();
            _optimized = _source
                .ToDictionary(x => x, x => Array.IndexOf(_source, x))
                .Optimize();
        }

        [Benchmark]
        public void Lookup()
        {
            for (int i = 0; i < _source.Length; i++)
                if (_optimized[_source[i]] != i)
                    throw new Exception("Bug in dictionary");
        }
    }
}
