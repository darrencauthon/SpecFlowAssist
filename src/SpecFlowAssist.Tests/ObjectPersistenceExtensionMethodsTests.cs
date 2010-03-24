using NUnit.Framework;
using TechTalk.SpecFlow;

namespace SpecFlowAssist.Tests
{
    [TestFixture]
    public class ObjectPersistenceExtensionMethodsTests
    {
        [Test]
        public void StartHere(string starthere)
        {
            Assert.Fail("start here");
        }
    }

}