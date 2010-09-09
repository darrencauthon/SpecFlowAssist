SpecFlowAssist
========

SpecFlowAssist is a small library that I wrote to assist me when I use SpecFlow.  SpecFlow is great the way it is, but as I used it I noticed I was doing a few things over and over.  This library contains a few extension methods and helpers that save me time and cut down the amount of code behind my specs.

Here are some of the methods that this library provides:

** Get<T> and Set<T> extension methods off of ScenarioContext **

ScenarioContext.Current is a static instance that can be used to share objects across multiple step files.  You can save objects in it like so:

	ScenarioContext.Current["AccountRepository"] = new AccountRepository();

and then retrieve them in other step definition methods like so:

	var accountRepository = (IAccountRepository)ScenarioContext.Current["AccountRepository"];
	
I like to avoid using strings and casting whenever possible, so I made a **Get<T>** and **Set<T>** methods that can be used to get and set values in ScenarioContext.  With these methods, I can do what I do like this:

	ScenarioContext.Current.Set<IAccountRepository>(new AccountRepository());
	// ...
	var repository = ScenarioContext.Current.Get<IAccountRepository>();
	

** CreateInstance<T> extenion methods off of SpecFlow.Table **

After using SpecFlow for a while, I noticed that I was writing many steps that looked like this:

	Given I entered the following data into the new account form:
	| Field              | Value      |
	| Name               | John Galt  |
	| Birthdate          | 2/2/1902   |
	| HeightInInches     | 72         |
	| BankAccountBalance | 1234.56    |

And then in my step definition, I'd create a new Account and manually load its values from the SpecFlow.Table that is passed in to the method, like so:

	[Given(@"Given I entered the following data into the new account form:")]
	public void ThenIShouldHaveTheFollowingDataInMyRepository(Table table)
	{
		var account = new Account();
		account.Name = table.Rows.First(x => x["Field"] == "Name")["Value"];
		account.Birthdate = DateTime.Parse(table.Rows.First(x => x["Field"] == "Birthdate")["Value"]);
		account.HeightInInches = int.Parse(table.Rows.First(x => x["Field"] == "HeightInInches")["Value"]);
		account.BankAccountBalance = decimal.Parse(table.Rows.First(x => x["Field"] == "BankAccountBalance")["Value"]);
		// ...
	}

Obviously, this is pretty awkward.  So, I made a **CreateInstance<T>** method to turn the code above into:

	[Given(@"Given I entered the following data into the new account form:")]
	public void ThenIShouldHaveTheFollowingDataInMyRepository(Table table)
	{
		var account = table.CreateInstance<Account>();
		// ...
	}

The CreateInstance<T> method will create the account and fill the values according to any matching names it finds.  It also will use the appropriate casting or conversion to turn your string into the appropriate type.



