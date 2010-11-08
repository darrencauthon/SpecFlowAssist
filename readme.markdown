SpecFlowAssist
========

SpecFlowAssist is a small library that I wrote to assist me when I use SpecFlow.  SpecFlow is great the way it is, but as I used it I noticed I was doing a few things over and over.  This library contains a few extension methods and helpers that save me time and cut down the amount of code behind my specs.

Here are some of the methods that this library provides:

CompareToInstance<T> extension methods off of SpecFlow.Table
---

This method makes it easy to compare the properties of an object against a table. For example, say you have a class like this:

  public class Person {
    public string FirstName { get; set;}  
  }

and you want to compare it to a table in a step like this:

  Then the person should have the following values
  | Field     | Value |
  | FirstName | John  |
  | LastName  | Galt  |
  
You can assert that the properties match with this simple step definition:

  [Then("the person should have the following values")]
  public void x(Table table){
    // you don't have to get person this way, this is just for demo
    var person = ScenarioContext.Current.Get<Person>(); 
    
    table.CompareToInstance<Person>(person);
  }

If FirstName does not match "John" or LastName does not match "Galt", a descriptive error showing what properties did not match will appear.  If they do match, no exception will be thrown and SpecFlow will continue to process your scenario.

One of the neat things about this method, in my opinion, is that when new properties are added to the class, I can change my specs to match without having to go back to my step definition.  

CompareToSet<T> extension methods off of SpecFlow.Table
---

During my use of SpecFlow, I found that I often needed to check that an expected set of objects were returned by an object.  It was a little awkward to do this through enumerating the rows in SpecFlow.Table manually each time, so I created CompareToSet<T> to do the monkey work for me.

Lets say you have a class like so:

  public class Account {
    public string Id { get; set;}
    public string FirstName { get; set;}
    public string LastName { get; set;}
    public string MiddleName { get; set;}
  }

And you want to test that your system returns a specific set of accounts, like so:

  Then I get back the following accounts
  | Id     | FirstName | LastName |
  | 1      | John      | Galt     |
  | 2      | Howard    | Roark    |

You can test you results with one call to CompareToSet<T>, like so:

  [Then("I get back the following accounts")]
  public void x(Table table){
    var accounts = ScenarioContext.Current.Get<IEnumerable<Account>>();
    
    accounts.CompareToSet<Account>(accounts)
  }

CompareToSet<T> will test that two accounts were returned, and it will test only the properties that you define in the table.  **It does not test the order of the objects, only that one was found that matches.**  If it cannot find a record that matches the properties in your table, the exception that is thrown will return the row number(s) that did not match.

Like CompareToInstance<T>, when new properties are added to your test you won't have to go back to your step definition code.  You can just add the new property to your step and it will be checked.

Get<T> and Set<T> extension methods off of ScenarioContext
---

** Moved from SpecFlowAssist to SpecFlow v.1.4.  If you are using an earlier version of SpecFlow, please download the pre-1.4 version of SpecFlowAssist. **

Check the Wiki for more information.

CreateInstance<T> extenion methods off of SpecFlow.Table
---

** Moved from SpecFlowAssist to SpecFlow v.1.4.  If you are using an earlier version of SpecFlow, please download the pre-1.4 version of SpecFlowAssist. **

Check the Wiki for more information.

CreateSet<T> extenion methods off of SpecFlow.Table
---

** Moved from SpecFlowAssist to SpecFlow v.1.4.  If you are using an earlier version of SpecFlow, please download the pre-1.4 version of SpecFlowAssist. **

Check the Wiki for more information.