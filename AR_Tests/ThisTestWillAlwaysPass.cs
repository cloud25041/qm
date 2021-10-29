using NUnit.Framework;

namespace AR_Tests
{
    public class ThisTestWillAlwaysPass
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}