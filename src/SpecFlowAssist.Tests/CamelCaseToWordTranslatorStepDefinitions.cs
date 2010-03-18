using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace SpecFlowAssist.Tests
{
    [Binding]
    public class CamelCaseToWordTranslatorStepDefinitions
    {

        [Given(@"the word is ""(.*)""")]
        public void GivenTheWordIs(string value)
        {
            Context.Set(value, "Word");
        }

        
        [When("I call the word to camel case translator")]
        public void WhenICallTheWordToCamelCaseTranslator()
        {
            var word = Context.Get<string>("Word");
            var translator = Mocker.Resolve<CamelCaseToWordTranslator>();
            var camelCase = translator.GetCamelCase(word);
            Context.Set(camelCase, "CamelCase");
        }

        [Then(@"the result should be ""(.*)""")]
        public void ThenTheResultShouldBe(string value)
        {
            var camelCase = Context.Get<string>("CamelCase");
            Assert.AreEqual(value, camelCase);
        }

        //[Given("I have entered (.*) into the calculator")]
        //public void GivenIHaveEnteredSomethingIntoTheCalculator(int number)
        //{
        //    //TODO: implement arrange (recondition) logic
        //    // For storing and retrieving scenario-specific data, 
        //    // the instance fields of the class or the
        //    //     ScenarioContext.Current
        //    // collection can be used.
        //    // To use the multiline text or the table argument of the scenario,
        //    // additional string/Table parameters can be defined on the step definition
        //    // method. 

        //    ScenarioContext.Current.Pending();
        //}

        //[When("I press add")]
        //public void WhenIPressAdd()
        //{
        //    //TODO: implement act (action) logic

        //    ScenarioContext.Current.Pending();
        //}

        //[Then("the result should be (.*) on the screen")]
        //public void ThenTheResultShouldBe(int result)
        //{
        //    //TODO: implement assert (verification) logic

        //    ScenarioContext.Current.Pending();
        //}
    }
}
