using System;
using TechTalk.SpecFlow;

namespace SpecFlowAssist
{
    public static class ObjectPersistenceExtensionMethods
    {
        public static T Get<T>(this ScenarioContext scenarioContext, string id) where T : class
        {
            return scenarioContext[id] as T;
        }

        public static T Get<T>(this ScenarioContext scenarioContext, Type type) where T : class
        {
            var id = type.ToString();
            return scenarioContext.Get<T>(id);
        }

        public static T Get<T>(this ScenarioContext scenarioContext) where T : class
        {
            var id = typeof (T).ToString();
            return scenarioContext.Get<T>(id);
        }

        public static void Set<T>(this ScenarioContext scenarioContext, T data, string id)
        {
            scenarioContext[id] = data;
        }

        public static void Set<T>(this ScenarioContext scenarioContext, T data, Type type)
        {
            var id = type.ToString();
            scenarioContext.Set(data, id);
        }

        public static void Set<T>(this ScenarioContext scenarioContext, T data)
        {
            var id = typeof(T).ToString();
            scenarioContext.Set(data, id);
        }
    }

}