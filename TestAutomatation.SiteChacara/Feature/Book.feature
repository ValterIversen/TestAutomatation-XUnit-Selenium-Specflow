Feature: BookDates 
	As a user I need a way to select dates and book the days
	
Scenario: Generate random data and book
	Given that we are on the home page
	And the user is interested
	And that a random user is informed
	And that a random period of month 7 with an interval of 5 days is informed
	When proceeding with the booking
	Then the data must be sent via whatsapp