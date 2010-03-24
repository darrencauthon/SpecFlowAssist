using NUnit.Framework;
using TechTalk.SpecFlow;

namespace SpecFlowAssist.Tests
{
    [TestFixture]
    public class MockerExtensionMethodsTests
    {
        private const string MockerId = "__Mocker__";

        [Test]
        public void GetMock_Called_LoadsAutoMoqerIntoScenarioContext()
        {
            var scenarioContext = CreateScenarioContext();
            scenarioContext.GetMock<ITestInterface>();
            Assert.IsTrue(scenarioContext.ContainsKey(MockerId));
        }

        [Test]
        public void GetMock_CalledTwice_UsesSameAutoMoqer()
        {
            var scenarioContext = CreateScenarioContext();

            scenarioContext.GetMock<ITestInterface>();
            var firstMocker = scenarioContext[MockerId];

            scenarioContext.GetMock<ITestInterface>();
            var secondMocker = scenarioContext[MockerId];

            Assert.AreSame(firstMocker, secondMocker);
        }

        [Test]
        public void GetMock_Called_ReturnsMock()
        {
            var scenarioContext = CreateScenarioContext();
            var mock = scenarioContext.GetMock<ITestInterface>().Object;
            Assert.IsNotNull(mock);
        }

        [Test]
        public void Resolve_Called_LoadsAutoMoqerIntoScenarioContext()
        {
            var scenarioContext = CreateScenarioContext();
            scenarioContext.Resolve<TestClass>();
            Assert.IsTrue(scenarioContext.ContainsKey(MockerId));
        }

        [Test]
        public void Resolve_CalledTwice_UsesSameAutoMoqer()
        {
            var scenarioContext = CreateScenarioContext();

            scenarioContext.Resolve<TestClass>();
            var firstMocker = scenarioContext[MockerId];

            scenarioContext.Resolve<TestClass>();
            var secondMocker = scenarioContext[MockerId];

            Assert.AreSame(firstMocker, secondMocker);
        }

        [Test]
        public void Resolve_Called_ReturnsMock()
        {
            var scenarioContext = CreateScenarioContext();
            var testClass = scenarioContext.Resolve<TestClass>();
            Assert.IsNotNull(testClass);
        }

        private static ScenarioContext CreateScenarioContext()
        {
            return new ScenarioContext(new ScenarioInfo("Test", new string[]{}));
        }
    }

    public interface ITestInterface
    {
    }

    public class TestClass
    {
    }
}