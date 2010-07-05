using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace SpecFlowAssist
{
    public static class TableHelperExtensionMethods
    {
        public static Dictionary<Type, Func<TableRow, string, object>> GetTypeHandlers()
        {
            return new Dictionary<Type, Func<TableRow, string, object>>
                       {
                           {typeof (string), (TableRow row, string id) => row.GetString("Value")},
                           {typeof (int), (TableRow row, string id) => row.GetInt("Value")},
                           {typeof (decimal), (TableRow row, string id) => row.GetDecimal("Value")},
                           {typeof (bool), (TableRow row, string id) => row.GetBool("Value")},
                           {typeof (DateTime), (TableRow row, string id) => row.GetDateTime("Value")}
                       };
        }

        public static T CreateInstance<T>(this Table table)
        {
            var instance = (T) Activator.CreateInstance(typeof (T));

            var handlers = GetTypeHandlers();

            (from property in typeof (T).GetProperties()
             join key in handlers.Keys on property.PropertyType equals key
             join row in table.Rows on property.Name equals row["Field"]
             select new {Row = row, property.Name, Handler = handlers[key]}).ToList()
                .ForEach(x => instance.SetPropertyValue(x.Name, x.Handler(x.Row, x.Row["Value"])));

            return instance;
        }

        public static IEnumerable<T> CreateSet<T>(this Table table)
        {
            var enumerable = table.Rows.Select(row =>
                                                   {
                                                       var instance = (T) Activator.CreateInstance(typeof (T));

                                                       SetStringValues(table, instance, row);
                                                       SetIntValues(table, instance, row);
                                                       SetsDateTimeValues(table, instance, row);
                                                       SetsDecimalValues(table, instance, row);
                                                       SetsBooleanValues(table, instance, row);

                                                       SetsNullableDateTimeValues(table, instance, row);
                                                       SetsNullableBooleanValues(table, instance, row);
                                                       SetsNullableIntValues(table, instance, row);
                                                       SetsNullableDecimalValues(table, instance, row);

                                                       return instance;
                                                   });

            return enumerable;
        }

        private static void SetsNullableDecimalValues<T>(Table table, T instance, TableRow row)
        {
            var propertiesToUpdate = GetPropertiesOfThisTypeToUpdate<T>(table, typeof (decimal?));
            foreach (var property in propertiesToUpdate)
                if (string.IsNullOrEmpty(row[property]))
                    instance.SetPropertyValue(property, null);
                else
                    instance.SetPropertyValue(property, row.GetDecimal(property));
        }

        private static void SetsNullableIntValues<T>(Table table, T instance, TableRow row)
        {
            var propertiesToUpdate = GetPropertiesOfThisTypeToUpdate<T>(table, typeof (int?));
            foreach (var property in propertiesToUpdate)
                if (string.IsNullOrEmpty(row[property]))
                    instance.SetPropertyValue(property, null);
                else
                    instance.SetPropertyValue(property, row.GetInt(property));
        }

        private static void SetsNullableBooleanValues<T>(Table table, T instance, TableRow row)
        {
            var propertiesToUpdate = GetPropertiesOfThisTypeToUpdate<T>(table, typeof (bool?));
            foreach (var property in propertiesToUpdate)
                if (string.IsNullOrEmpty(row[property]))
                    instance.SetPropertyValue(property, null);
                else
                    instance.SetPropertyValue(property, row.GetBool(property));
        }

        private static void SetsBooleanValues<T>(Table table, T instance, TableRow row)
        {
            var propertiesToUpdate = GetPropertiesOfThisTypeToUpdate<T>(table, typeof (bool));
            foreach (var property in propertiesToUpdate)
                instance.SetPropertyValue(property, row.GetBool(property));
        }

        private static void SetsDecimalValues<T>(Table table, T instance, TableRow row)
        {
            var propertiesToUpdate = GetPropertiesOfThisTypeToUpdate<T>(table, typeof (decimal));
            foreach (var property in propertiesToUpdate)
                instance.SetPropertyValue(property, row.GetDecimal(property));
        }

        private static void SetsDateTimeValues<T>(Table table, T instance, TableRow row)
        {
            var propertiesToUpdate = GetPropertiesOfThisTypeToUpdate<T>(table, typeof (DateTime));
            foreach (var property in propertiesToUpdate)
                instance.SetPropertyValue(property, row.GetDateTime(property));
        }

        private static void SetsNullableDateTimeValues<T>(Table table, T instance, TableRow row)
        {
            var propertiesToUpdate = GetPropertiesOfThisTypeToUpdate<T>(table, typeof (DateTime?));
            foreach (var property in propertiesToUpdate)
                if (string.IsNullOrEmpty(row[property]))
                    instance.SetPropertyValue(property, null);
                else
                    instance.SetPropertyValue(property, row.GetDateTime(property));
        }

        private static void SetIntValues<T>(Table table, T instance, TableRow row)
        {
            var propertiesToUpdate = GetPropertiesOfThisTypeToUpdate<T>(table, typeof (int));
            foreach (var property in propertiesToUpdate)
                instance.SetPropertyValue(property, row.GetInt(property));
        }

        private static void SetStringValues<T>(Table table, T instance, TableRow row)
        {
            var propertiesToUpdate = GetPropertiesOfThisTypeToUpdate<T>(table, typeof (string));
            foreach (var property in propertiesToUpdate)
                instance.SetPropertyValue(property, row.GetString(property));
        }

        private static IEnumerable<string> GetPropertiesOfThisTypeToUpdate<T>(Table table, Type type)
        {
            var propertyNames = GetPropertiesOfThisType<T>(type);

            return from name in propertyNames
                   join header in table.Header on name equals header
                   select header;
        }

        private static IEnumerable<string> GetPropertiesOfThisType<T>(Type type)
        {
            return typeof (T).GetProperties().ToList()
                .Where(x => x.PropertyType == type)
                .Select(x => x.Name);
        }
    }
}