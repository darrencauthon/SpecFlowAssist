using System;
using TechTalk.SpecFlow;

namespace SpecFlowAssist
{
    public static class Context
    {
        public static T Get<T>(string id) where T : class
        {
            return ScenarioContext.Current[id] as T;
        }

        public static T Get<T>(Type type) where T : class
        {
            var id = type.ToString();
            return Get<T>(id);
        }

        public static T Get<T>() where T : class
        {
            var id = typeof (T).ToString();
            return Get<T>(id);
        }

        public static void Set<T>(T data, string id)
        {
            ScenarioContext.Current[id] = data;
        }

        public static void Set<T>(T data, Type type)
        {
            var id = type.ToString();
            Set(data, id);
        }

        public static void Set<T>(T data)
        {
            var id = data.GetType().ToString();
            Set(data, id);
        }
    }
}