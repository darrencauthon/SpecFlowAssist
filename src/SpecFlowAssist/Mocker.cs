using AutoMoq;
using Moq;
using TechTalk.SpecFlow;

namespace SpecFlowAssist
{
    public static class Mocker
    {
        public static Mock<T> GetMock<T>() where T : class
        {
            return GetAutoMoqer().GetMock<T>();
        }

        public static T Resolve<T>()
        {
            return GetAutoMoqer().Resolve<T>();
        }

        public static AutoMoqer GetAutoMoqer()
        {
            if (!ScenarioContext.Current.ContainsKey("Mocker"))
                ScenarioContext.Current["Mocker"] = new AutoMoqer();

            return (AutoMoqer)ScenarioContext.Current["Mocker"];
        }
    }
}