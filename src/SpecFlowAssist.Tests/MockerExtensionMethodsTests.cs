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
            scenarioContext.GetMock<IScenarioTestInterface>();
            Assert.IsTrue(scenarioContext.ContainsKey(MockerId));
        }

        [Test]
        public void GetMock_CalledTwice_UsesSameAutoMoqer()
        {
            var scenarioContext = CreateScenarioContext();

            scenarioContext.GetMock<IScenarioTestInterface>();
            var firstMocker = scenarioContext[MockerId];

            scenarioContext.GetMock<IScenarioTestInterface>();
            var secondMocker = scenarioContext[MockerId];

            Assert.AreSame(firstMocker, secondMocker);
        }

        [Test]
        public void GetMock_Called_ReturnsMock()
        {
            var scenarioContext = CreateScenarioContext();
            var mock = scenarioContext.GetMock<IScenarioTestInterface>().Object;
            Assert.IsNotNull(mock);
        }

        [Test]
        public void Resolve_Called_LoadsAutoMoqerIntoScenarioContext()
        {
            var scenarioContext = CreateScenarioContext();
            scenarioContext.Resolve<ScenarioTestClass>();
            Assert.IsTrue(scenarioContext.ContainsKey(MockerId));
        }

        [Test]
        public void Resolve_CalledTwice_UsesSameAutoMoqer()
        {
            var scenarioContext = CreateScenarioContext();

            scenarioContext.Resolve<ScenarioTestClass>();
            var firstMocker = scenarioContext[MockerId];

            scenarioContext.Resolve<ScenarioTestClass>();
            var secondMocker = scenarioContext[MockerId];

            Assert.AreSame(firstMocker, secondMocker);
        }

        [Test]
        public void Resolve_Called_ReturnsMock()
        {
            var scenarioContext = CreateScenarioContext();
            var testClass = scenarioContext.Resolve<ScenarioTestClass>();
            Assert.IsNotNull(testClass);
        }

        private static ScenarioContext CreateScenarioContext()
        {
            return new ScenarioContext(new ScenarioInfo("Test", new string[]{}));
        }
    }

    public interface IMockerTestInterface
    {
    }

    public class MockerTestClass
    {
    }
}