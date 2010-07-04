using System;
using System.Linq;
using NUnit.Framework;
using Should;
using TechTalk.SpecFlow;

namespace SpecFlowAssist.Tests.TableHelperExtensionMethods
{
    [TestFixture]
    public class CreateSetWithNullableValuesTests
    {
        [Test]
        public void Can_set_a_nullable_datetime()
        {
            var table = new Table("FirstName", "LastName", "BirthDate", "NumberOfIdeas", "Salary", "IsRational");
            table.AddRow("", "", "4/28/2009", "3", "", "");

            var people = table.CreateSet<NullablePerson>();

            people.First().BirthDate.ShouldEqual(new DateTime(2009, 4, 28));
        }

        [Test]
        public void Sets_a_nullable_datetime_to_null_when_the_value_is_empty()
        {
            var table = new Table("FirstName", "LastName", "BirthDate", "NumberOfIdeas", "Salary", "IsRational");
            table.AddRow("", "", "", "", "", "");

            var people = table.CreateSet<NullablePerson>();

            people.First().BirthDate.ShouldBeNull();
        }

        [Test]
        public void Can_set_a_nullable_boolean()
        {
            var table = new Table("FirstName", "LastName", "BirthDate", "NumberOfIdeas", "Salary", "IsRational");
            table.AddRow("", "", "", "3", "", "true");

            var people = table.CreateSet<NullablePerson>();

            people.First().IsRational.Value.ShouldBeTrue();
        }

        [Test]
        public void Sets_a_nullable_boolean_to_null_when_the_value_is_empty()
        {
            var table = new Table("FirstName", "LastName", "BirthDate", "NumberOfIdeas", "Salary", "IsRational");
            table.AddRow("", "", "", "", "", "");

            var people = table.CreateSet<NullablePerson>();

            people.First().IsRational.ShouldBeNull();
        }

        [Test]
        public void Can_set_a_nullable_int()
        {
            var table = new Table("FirstName", "LastName", "BirthDate", "NumberOfIdeas", "Salary", "IsRational");
            table.AddRow("", "", "", "3", "", "true");

            var people = table.CreateSet<NullablePerson>();

            people.First().NumberOfIdeas.ShouldEqual(3);
        }

        [Test]
        public void Sets_a_nullable_int_to_null_when_the_value_is_empty()
        {
            var table = new Table("FirstName", "LastName", "BirthDate", "NumberOfIdeas", "Salary", "IsRational");
            table.AddRow("", "", "", "", "", "");

            var people = table.CreateSet<NullablePerson>();

            people.First().NumberOfIdeas.ShouldBeNull();
        }

        [Test]
        public void Can_set_a_nullable_decimal()
        {
            var table = new Table("FirstName", "LastName", "BirthDate", "NumberOfIdeas", "Salary", "IsRational");
            table.AddRow("", "", "", "", "4.01", "");

            var people = table.CreateSet<NullablePerson>();

            people.First().Salary.Value.ShouldEqual(4.01M);
        }

        [Test]
        public void Sets_a_nullable_decimal_to_null_when_the_value_is_empty()
        {
            var table = new Table("FirstName", "LastName", "BirthDate", "NumberOfIdeas", "Salary", "IsRational");
            table.AddRow("", "", "", "", "", "");

            var people = table.CreateSet<NullablePerson>();

            people.First().Salary.ShouldBeNull();
        }
    }

    public class NullablePerson
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? NumberOfIdeas { get; set; }
        public decimal? Salary { get; set; }
        public bool? IsRational { get; set; }
    }
}