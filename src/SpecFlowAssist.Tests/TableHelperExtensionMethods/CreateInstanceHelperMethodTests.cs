﻿using System;
using NUnit.Framework;
using Should;
using TechTalk.SpecFlow;

namespace SpecFlowAssist.Tests.TableHelperExtensionMethods
{
    [TestFixture]
    public class CreateInstanceHelperMethodTests
    {
        [Test]
        public void Create_instance_will_return_an_instance_of_T()
        {
            var table = new Table("Field", "Value");
            var person = table.CreateInstance<Person>();
            person.ShouldNotBeNull();
        }

        [Test]
        public void Sets_string_values()
        {
            var table = new Table("Field", "Value");
            table.AddRow("FirstName", "John");
            table.AddRow("LastName", "Galt");

            var person = table.CreateInstance<Person>();

            person.FirstName.ShouldEqual("John");
            person.LastName.ShouldEqual("Galt");
        }

        [Test]
        public void Sets_int_values()
        {
            var table = new Table("Field", "Value");
            table.AddRow("NumberOfIdeas", "3");

            var person = table.CreateInstance<Person>();

            person.NumberOfIdeas.ShouldEqual(3);
        }

        [Test]
        public void Sets_decimal_values()
        {
            var table = new Table("Field", "Value");
            table.AddRow("Salary", "9.78");

            var person = table.CreateInstance<Person>();

            person.Salary.ShouldEqual(9.78M);
        }

        [Test]
        public void Sets_bool_values()
        {
            var table = new Table("Field", "Value");
            table.AddRow("IsRational", "true");

            var person = table.CreateInstance<Person>();

            person.IsRational.ShouldBeTrue();
        }

        [Test]
        public void Sets_datetime_values()
        {
            var table = new Table("Field", "Value");
            table.AddRow("BirthDate", "12/31/2010");

            var person = table.CreateInstance<Person>();

            person.BirthDate.ShouldEqual(new DateTime(2010, 12, 31));
        }
    }
}