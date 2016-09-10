using System.Collections.Generic;
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
            new Dictionary<int, string>
            {
                {1, "1"}
            }.Optimize()[1].Should().Be("1");
        }
    }
}
