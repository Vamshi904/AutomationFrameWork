@Regression
Feature: System Management - Currency Rates and Customers

Background:
	Given user is on login page
	When user logs in with valid credentials
	Then user should be logged in successfully
@Add
Scenario: Add a new currency rate with valid details
	When the user clicks on the Couriers tab
	And the user clicks on the Rate menu
	And the user clicks on the Add Rates button
	And the user enters valid details for Add New Currency Rate
	And saves the new rate
	Then the new currency rate should appear in the rate list

@Edit
Scenario: Edit currency rate with valid details
	When the user clicks on the Couriers tab
	And the user clicks on the Rate menu
	And the user clicks on a username button
	And the user edits the user details
	And the user clicks on the Update Information button
	Then the confirmation message should appear in the rate list


@Customer
Scenario: View active customers
	When the user clicks on the customer tab
	And selects the Active status from the dropdown
	Then print all the details in the table


