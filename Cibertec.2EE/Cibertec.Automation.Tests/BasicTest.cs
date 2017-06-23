using System;
using System.Linq;
using Xunit;

namespace Cibertec.Automation.Tests
{
    public class BasicTest
    {
        [Fact]
        public void Navigate_to_Google()
        {
            SimpleTest.Navigate();
        }
    }
}
