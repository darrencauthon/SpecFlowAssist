using NUnit.Framework;
using TechTalk.SpecFlow;

namespace SpecFlowAssist.Tests
{
    [TestFixture]
    public class ObjectPersistenceExtensionMethodsTests
    {
        [Test]
        public void Can_Set_Object_According_To_Generic_Type()
        {
            var scenarioContext = CreateScenarioContext();
            var testClass = new ScenarioTestClass();

            scenarioContext.Set<IScenarioTestInterface>(testClass);
            Assert.AreSame(testClass, scenarioContext[typeof (IScenarioTestInterface).ToString()]);
        }

        [Test]
        public void Can_Set_Object_According_To_String()
        {
            var scenarioContext = CreateScenarioContext();
            var testClass = new ScenarioTestClass();

            scenarioContext.Set(testClass, "TEST");
            Assert.AreSame(testClass, scenarioContext["TEST"]);
        }

        [Test]
        public void Can_Set_Object_According_To_Parameter_Type()
        {
            var scenarioContext = CreateScenarioContext();
            var testClass = new ScenarioTestClass();

            scenarioContext.Set(testClass, typeof (int));
            Assert.AreSame(testClass, scenarioContext[typeof (int).ToString()]);
        }

        [Test]
        public void Can_Get_Object_According_To_Generic_Type()
        {
            var scenarioContext = CreateScenarioContext();
            var testClass = new ScenarioTestClass();

            scenarioContext[typeof (IScenarioTestInterface).ToString()] = testClass;
            Assert.AreSame(testClass, scenarioContext.Get<IScenarioTestInterface>());
        }

        [Test]
        public void Can_Get_Object_According_To_String()
        {
            var scenarioContext = CreateScenarioContext();
            var testClass = new ScenarioTestClass();

            scenarioContext["TEST"] = testClass;
            Assert.AreSame(testClass, scenarioContext.Get<object>("TEST"));
        }

        [Test]
        public void Can_Get_Object_According_To_Parameter_Type()
        {
            var scenarioContext = CreateScenarioContext();
            var testClass = new ScenarioTestClass();

            scenarioContext[typeof (string).ToString()] = testClass;
            Assert.AreSame(testClass, scenarioContext.Get<object>(typeof (string)));
        }

        private static ScenarioContext CreateScenarioContext()
        {
            return new ScenarioContext(new ScenarioInfo("Test", new string[]{}));
        }
    }

    public class ScenarioTestClass : IScenarioTestInterface
    {
    }

    public interface IScenarioTestInterface
    {
    }
}