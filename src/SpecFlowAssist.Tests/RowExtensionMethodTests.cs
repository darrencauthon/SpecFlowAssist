using System;
using System.Linq;
using NUnit.Framework;
using Should;
using TechTalk.SpecFlow;

namespace SpecFlowAssist.Tests
{
    [TestFixture]
    public class RowExtensionMethodTests
    {
        [Test]
        public void GetString_should_return_the_string_value_from_the_row()
        {
            var table = new Table("Name");
            table.AddRow("John Galt");
            table.Rows.First()
                .GetString("Name").ShouldEqual("John Galt");
        }

        [Test]
        public void GetString_should_return_null_if_the_value_is_not_defined()
        {
            var table = new Table("Name");
            table.AddRow("John Galt");
            table.Rows.First()
                .GetString("SomethingThatDoesNotExist").ShouldEqual(null);
        }

        [Test]
        public void GetInt_should_return_the_int_from_the_row()
        {
            var table = new Table("Count");
            table.AddRow("3");
            table.Rows.First()
                .GetInt("Count").ShouldEqual(3);
        }

        [Test]
        public void GetInt_should_return_MinValue_when_the_value_is_not_defined()
        {
            var table = new Table("Count");
            table.AddRow("4");
            table.Rows.First()
                .GetInt("SomethingThatDoesNotExist").ShouldEqual(int.MinValue);
        }

        [Test]
        public void GetDecimal_should_return_the_datetime_from_the_row()
        {
            var table = new Table("Amount");
            table.AddRow("4.01");
            table.Rows.First()
                .GetDecimal("Amount").ShouldEqual(4.01M);
        }

        [Test]
        public void GetDecimal_should_return_MinValue_when_the_value_is_not_defined()
        {
            var table = new Table("Amount");
            table.AddRow("4.01");
            table.Rows.First()
                .GetDecimal("SomethingThatDoesNotExist").ShouldEqual(decimal.MinValue);
        }

        [Test]
        public void GetDateTime_should_return_the_datetime_from_the_row()
        {
            var table = new Table("Birthdate");
            table.AddRow("4/28/2009 21:02:03");
            table.Rows.First()
                .GetDateTime("Birthdate").ShouldEqual(new DateTime(2009, 4, 28, 21, 2, 3));
        }

        [Test]
        public void GetDateTime_should_return_MinValue_when_the_value_is_not_defined()
        {
            var table = new Table("Birthdate");
            table.AddRow("4/28/2009 21:02:03");
            table.Rows.First()
                .GetDateTime("SomethingThatDoesNotExist").ShouldEqual(DateTime.MinValue);
        }

        [Test]
        public void GetBool_returns_true_when_the_value_is_true()
        {
            var table = new Table("IsNeat");
            table.AddRow("true");
            table.Rows.First()
                .GetBool("IsNeat").ShouldBeTrue();
        }

        [Test]
        public void GetBool_returns_false_when_the_value_is_false()
        {
            var table = new Table("IsNeat");
            table.AddRow("false");
            table.Rows.First()
                .GetBool("IsNeat").ShouldBeFalse();
        }

        [Test]
        public void GetBool_throws_an_exception_when_the_value_is_not_true_or_false()
        {
            var table = new Table("IsNeat");
            table.AddRow("is not true nor false");
            var exceptionThrown = false;
            try
            {
                table.Rows.First()
                    .GetBool("IsNeat");
            }
            catch (InvalidCastException exception)
            {
                if (exception.Message == "You must use 'true' or 'false' when setting bools for IsNeat")
                    exceptionThrown = true;
            }
            exceptionThrown.ShouldBeTrue();
        }

        [Test]
        public void GetBool_throws_an_exception_when_the_id_is_not_defined()
        {
            var table = new Table("IsNeat");
            table.AddRow("is not true nor false");
            var exceptionThrown = false;
            try
            {
                table.Rows.First()
                    .GetBool("SomethingThatDoesNotExist");
            }
            catch (InvalidOperationException exception)
            {
                if (exception.Message == "SomethingThatDoesNotExist could not be found in the row.")
                    exceptionThrown = true;
            }
            exceptionThrown.ShouldBeTrue();
        }
    }
}