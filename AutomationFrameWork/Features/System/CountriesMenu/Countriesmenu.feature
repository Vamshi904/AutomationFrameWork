@Regression
Feature: Countriesmenu

Background:
	Given user is on login page
	When user logs in with valid credentials
	Then user should be logged in successfully
   
   @AddCountry @P1
Scenario: Add a new Countries with valid details
	When the user navigates to the System tab
	And Click on the Countries menu
	And the user clicks on the Add Country button
	And fills in the Country Form with valid details
	And Click on the Submit information button in Country Form
	Then Verify the confirmation message is displayed for new Country Form


@EditCountry @P2
Scenario: Editing existing Countries with valid details
	When the user navigates to the System tab
	And Click on the Countries menu
	And the user clicks on the Country Name link
	And Edit in the Country Form with valid details
	And Click on the Submit information button in Country Form
	Then Verify the confirmation message is displayed for Edit Country Form

	@VIewData
Scenario: View Country Form table data
	When the user navigates to the System tab
	And Click on the Countries menu
	Then print all the details in the  Countries table



