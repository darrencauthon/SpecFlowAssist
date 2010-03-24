using System.Collections.Generic;
using AutoMoq;
using Moq;
using TechTalk.SpecFlow;

namespace SpecFlowAssist
{
    public static class MockerExtensionMethods
    {
        private const string MockerId = "__Mocker__";

        public static Mock<T> GetMock<T>(this ScenarioContext scenarioContext) where T : class
        {
            SetupTheAutoMockerIfNecessary(scenarioContext);
            return GetTheAutoMocker(scenarioContext).GetMock<T>();
        }

        public static T Resolve<T>(this ScenarioContext scenarioContext)
        {
            SetupTheAutoMockerIfNecessary(scenarioContext);
            return GetTheAutoMocker(scenarioContext).Resolve<T>();
        }

        private static void SetupTheAutoMockerIfNecessary(ScenarioContext scenarioContext)
        {
            if (TheAutoMockerHasNotBeenSetup(scenarioContext))
                SetupTheAutoMocker(scenarioContext);
        }

        private static AutoMoqer GetTheAutoMocker(IDictionary<string, object> scenarioContext)
        {
            return (AutoMoqer)scenarioContext[MockerId];
        }

        private static object SetupTheAutoMocker(IDictionary<string, object> scenarioContext)
        {
            return scenarioContext[MockerId] = new AutoMoqer();
        }

        private static bool TheAutoMockerHasNotBeenSetup(IDictionary<string, object> scenarioContext)
        {
            return !scenarioContext.ContainsKey(MockerId);
        }
    }
}