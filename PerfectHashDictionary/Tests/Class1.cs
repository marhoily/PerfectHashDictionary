using System;
using System.Linq;
using FluentAssertions;
using PerfectHashDictionary;
using Xunit;

namespace Tests
{
    public class Class1
    {
        [Fact]
        public void FactMethodName()
        {
            var source = Enumerable.Range(0, 30)
                .Select(_ => new object())
                .ToArray();
            var optimized = source
                .ToDictionary(x => x, x => Array.IndexOf(source, x))
                .Optimize();
            for (var i = 0; i < source.Length; i++)
                optimized[source[i]].Should().Be(i);
        }
    }
}
