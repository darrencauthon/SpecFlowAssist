﻿using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowAssist
{
    public static class SetComparisonExtensionMethods
    {
        public static void CompareToSet<T>(this Table table, IEnumerable<T> set)
        {
            var checker = new SetComparer<T>(table);
            checker.CompareToSet(set);
        }
    }

    public class SetComparer<T>
    {
        private const int MatchNotFound = -1;
        private readonly Table table;
        private readonly IEnumerable<string> propertiesToTest;
        private readonly List<T> expectedItems;

        public SetComparer(Table table)
        {
            this.table = table;
            propertiesToTest = GetAllPropertiesToTest(table);
            expectedItems = GetTheExpectedItems(table);
        }

        public void CompareToSet(IEnumerable<T> set)
        {
            AssertThatAllColumnsInTheTableMatchToPropertiesOnTheType();

            if (ThereAreNoResultsAndNoExpectedResults(set))
                return;

            if (ThereAreResultsWhenThereShouldBeNone(set))
                ThrowAnExpectedNoResultsError(set);

            AssertThatTheItemsMatchTheExpectedResults(set);

            AssertThatNoExtraRowsExist(set);
        }

        private void AssertThatNoExtraRowsExist(IEnumerable<T> set)
        {
            if (set.Count() != table.Rows.Count())
                throw new ComparisonException(string.Format(@"Expected {0} result{2}, but found {1}.",
                                                            table.Rows.Count(), set.Count(), table.Rows.Count() == 1 ? "" : "s"));
        }

        private static void ThrowAnExpectedNoResultsError(IEnumerable<T> set)
        {
            throw new ComparisonException(string.Format("There {0} {1} result{2} when expecting no results.",
                                                        set.Count() == 1 ? "was" : "were",
                                                        set.Count(),
                                                        set.Count() == 1 ? "" : "s"));
        }

        private bool ThereAreResultsWhenThereShouldBeNone(IEnumerable<T> set)
        {
            return set.Count() > 0 && table.Rows.Count() == 0;
        }

        private bool ThereAreNoResultsAndNoExpectedResults(IEnumerable<T> set)
        {
            return set.Count() == 0 && table.Rows.Count() == 0;
        }

        private void AssertThatTheItemsMatchTheExpectedResults(IEnumerable<T> set)
        {
            var listOfMissingItems = GetListOfExpectedItemsThatCouldNotBeFound(set);

            if (ExpectedItemsCouldNotBeFound(listOfMissingItems))
                ThrowAnErrorDetailingWhichItemsAreMissing(listOfMissingItems);
        }

        private IEnumerable<int> GetListOfExpectedItemsThatCouldNotBeFound(IEnumerable<T> set)
        {
            var actualItems = GetTheActualItems(set);

            var listOfMissingItems = new List<int>();

            for (var index = 0; index < expectedItems.Count(); index++)
            {
                var expectedItem = expectedItems[index];
                var matchIndex = GetTheIndexOfTheMatchingItem(expectedItem, actualItems);

                if (matchIndex == MatchNotFound)
                    listOfMissingItems.Add(index + 1);
                else
                    RemoveFromActualItemsSoItWillNotBeCheckedAgain(actualItems, matchIndex);
            }
            return listOfMissingItems;
        }

        private static void ThrowAnErrorDetailingWhichItemsAreMissing(IEnumerable<int> listOfMissingItems)
        {
            throw new ComparisonException(
                listOfMissingItems.Aggregate(
                    @"The expected items at the following line numbers could not be matched:",
                    (running, next) => running + "\r\n" + next));
        }

        private static bool ExpectedItemsCouldNotBeFound(IEnumerable<int> listOfMissingItems)
        {
            return listOfMissingItems.Any();
        }

        private static void RemoveFromActualItemsSoItWillNotBeCheckedAgain(List<T> actualItems, int matchIndex)
        {
            actualItems.RemoveAt(matchIndex);
        }

        private int GetTheIndexOfTheMatchingItem(T expectedItem,
                                                 IList<T> actualItems)
        {
            for (var actualItemIndex = 0; actualItemIndex < actualItems.Count(); actualItemIndex++)
            {
                var actualItem = actualItems[actualItemIndex];

                if (ThisItemIsAMatch(expectedItem, actualItem))
                    return actualItemIndex;
            }
            return MatchNotFound;
        }

        private bool ThisItemIsAMatch(T expectedItem, T actualItem)
        {
            foreach (var propertyName in propertiesToTest)
            {
                var expectedValue = expectedItem.GetPropertyValue(propertyName);
                var actualValue = actualItem.GetPropertyValue(propertyName);

                if (TheseValuesDoNotMatch(actualValue, expectedValue))
                    return false;
            }
            return true;
        }

        private static IEnumerable<string> GetAllPropertiesToTest(Table table)
        {
            return table.Header;
        }

        private static bool TheseValuesDoNotMatch(object actualValue, object expectedValue)
        {
            return (actualValue != null && expectedValue == null) ||
                   (actualValue == null && expectedValue != null) ||
                   ((actualValue != null) &&
                    (actualValue.ToString() != expectedValue.ToString()));
        }

        private static List<T> GetTheActualItems(IEnumerable<T> set)
        {
            return set.ToList();
        }

        private static List<T> GetTheExpectedItems(Table table)
        {
            return table.CreateSet<T>().ToList();
        }

        private void AssertThatAllColumnsInTheTableMatchToPropertiesOnTheType()
        {
            var propertiesThatDoNotExist = from property in table.Header
                                           where
                                               typeof (T).GetProperties().Select(x => x.Name).Contains(property)
                                               == false
                                           select property;

            if (propertiesThatDoNotExist.Any())
                throw new ComparisonException(
                    propertiesThatDoNotExist.Aggregate(@"The following fields do not exist:",
                                                       (running, next) => running + string.Format("\r\n{0}", next)));
        }
    }
}