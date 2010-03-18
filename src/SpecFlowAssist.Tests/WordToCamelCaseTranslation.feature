Feature: Word to camel case translation
	In order to relate english words to properties
	As a programmer
	I want to translate words to camel case

Scenario: Empty string
	Given the word is ""
	When I call the word to camel case translator
	Then the result should be ""
	
